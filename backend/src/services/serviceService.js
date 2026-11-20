import { prisma } from '../db.js';
import { AppError } from '../errors.js';
import { toNumber } from '../utils/decimal.js';
import { rethrowPrismaError } from '../utils/prismaErrors.js';

// "Service" este serviciul oferit de hoteluri (Wi-Fi, mic dejun, spa), nu un strat al aplicatiei.
const NOT_FOUND = 'Serviciul nu exista';

const include = { hotels: { select: { id: true, name: true }, orderBy: { name: 'asc' } } };

const present = (service) => ({ ...service, price: toNumber(service.price) });

async function assertChainExists(chainId) {
  const chain = await prisma.chain.findUnique({ where: { id: chainId }, select: { id: true } });
  if (!chain) {
    throw new AppError(400, 'Lantul hotelier ales nu exista');
  }
}

// Hotelurile alese trebuie sa existe si sa apartina lantului serviciului.
async function connectHotels(hotelIds, chainId) {
  const ids = [...new Set(hotelIds)];
  const hotels = await prisma.hotel.findMany({
    where: { id: { in: ids } },
    select: { id: true, chainId: true },
  });
  if (hotels.length !== ids.length || hotels.some((hotel) => hotel.chainId !== chainId)) {
    throw new AppError(400, 'Hotelurile alese trebuie sa existe si sa apartina lantului');
  }
  return ids.map((id) => ({ id }));
}

export async function listServices({ chainId, hotelId } = {}) {
  const where = {};
  if (chainId) {
    where.chainId = chainId;
  }
  if (hotelId) {
    where.hotels = { some: { id: hotelId } };
  }
  const services = await prisma.service.findMany({ where, orderBy: { name: 'asc' }, include });
  return services.map(present);
}

export async function getService(id) {
  const service = await prisma.service.findUnique({ where: { id }, include });
  if (!service) {
    throw new AppError(404, NOT_FOUND);
  }
  return present(service);
}

export async function createService({ hotelIds, ...data }) {
  await assertChainExists(data.chainId);
  const connect = await connectHotels(hotelIds, data.chainId);
  const service = await prisma.service.create({
    data: { ...data, hotels: { connect } },
    include,
  });
  return present(service);
}

// PUT inlocuieste serviciul, inclusiv lista de hoteluri (hotelIds lipsa inseamna nicio legatura).
export async function updateService(id, { hotelIds, ...data }) {
  await assertChainExists(data.chainId);
  const set = await connectHotels(hotelIds, data.chainId);
  try {
    const service = await prisma.service.update({
      where: { id },
      data: { ...data, hotels: { set } },
      include,
    });
    return present(service);
  } catch (error) {
    rethrowPrismaError(error, { notFound: NOT_FOUND });
  }
}

export async function deleteService(id) {
  try {
    await prisma.service.delete({ where: { id } });
  } catch (error) {
    rethrowPrismaError(error, { notFound: NOT_FOUND });
  }
}
