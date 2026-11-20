import { prisma } from '../db.js';
import { AppError } from '../errors.js';
import { rethrowPrismaError } from '../utils/prismaErrors.js';

const NOT_FOUND = 'Hotelul nu exista';

async function assertChainExists(chainId) {
  const chain = await prisma.chain.findUnique({ where: { id: chainId }, select: { id: true } });
  if (!chain) {
    throw new AppError(400, 'Lantul hotelier ales nu exista');
  }
}

export function listHotels({ chainId } = {}) {
  return prisma.hotel.findMany({
    where: chainId ? { chainId } : {},
    orderBy: { name: 'asc' },
  });
}

export async function getHotel(id) {
  const hotel = await prisma.hotel.findUnique({
    where: { id },
    include: { chain: { select: { id: true, name: true } } },
  });
  if (!hotel) {
    throw new AppError(404, NOT_FOUND);
  }
  return hotel;
}

export async function createHotel(data) {
  await assertChainExists(data.chainId);
  return prisma.hotel.create({ data });
}

export async function updateHotel(id, data) {
  await assertChainExists(data.chainId);
  try {
    return await prisma.hotel.update({ where: { id }, data });
  } catch (error) {
    rethrowPrismaError(error, { notFound: NOT_FOUND });
  }
}

export async function deleteHotel(id) {
  try {
    await prisma.hotel.delete({ where: { id } });
  } catch (error) {
    rethrowPrismaError(error, {
      notFound: NOT_FOUND,
      referenced: 'Hotelul nu poate fi sters: are rezervari',
    });
  }
}
