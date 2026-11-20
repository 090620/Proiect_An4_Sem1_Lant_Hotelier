import { prisma } from '../db.js';
import { AppError } from '../errors.js';
import { toNumber } from '../utils/decimal.js';
import { rethrowPrismaError } from '../utils/prismaErrors.js';

const NOT_FOUND = 'Camera nu exista';

const withNumberPrice = (room) => ({ ...room, price: toNumber(room.price) });

async function assertHotelExists(hotelId) {
  const hotel = await prisma.hotel.findUnique({ where: { id: hotelId }, select: { id: true } });
  if (!hotel) {
    throw new AppError(400, 'Hotelul ales nu exista');
  }
}

export async function listRooms({ hotelId } = {}) {
  const rooms = await prisma.room.findMany({
    where: hotelId ? { hotelId } : {},
    orderBy: { price: 'asc' },
  });
  return rooms.map(withNumberPrice);
}

export async function getRoom(id) {
  const room = await prisma.room.findUnique({
    where: { id },
    include: { hotel: { select: { id: true, name: true, chainId: true } } },
  });
  if (!room) {
    throw new AppError(404, NOT_FOUND);
  }
  return withNumberPrice(room);
}

export async function createRoom(data) {
  await assertHotelExists(data.hotelId);
  const room = await prisma.room.create({ data });
  return withNumberPrice(room);
}

export async function updateRoom(id, data) {
  await assertHotelExists(data.hotelId);
  try {
    const room = await prisma.room.update({ where: { id }, data });
    return withNumberPrice(room);
  } catch (error) {
    rethrowPrismaError(error, { notFound: NOT_FOUND });
  }
}

export async function deleteRoom(id) {
  try {
    await prisma.room.delete({ where: { id } });
  } catch (error) {
    rethrowPrismaError(error, {
      notFound: NOT_FOUND,
      referenced: 'Camera nu poate fi stearsa: are rezervari',
    });
  }
}
