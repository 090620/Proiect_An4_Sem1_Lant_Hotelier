import { prisma } from '../db.js';
import { AppError } from '../errors.js';
import { rethrowPrismaError } from '../utils/prismaErrors.js';

const NOT_FOUND = 'Lantul hotelier nu exista';

export function listChains() {
  return prisma.chain.findMany({ orderBy: { name: 'asc' } });
}

export async function getChain(id) {
  const chain = await prisma.chain.findUnique({
    where: { id },
    include: { hotels: { orderBy: { name: 'asc' } } },
  });
  if (!chain) {
    throw new AppError(404, NOT_FOUND);
  }
  return chain;
}

export function createChain(data) {
  return prisma.chain.create({ data });
}

export async function updateChain(id, data) {
  try {
    return await prisma.chain.update({ where: { id }, data });
  } catch (error) {
    rethrowPrismaError(error, { notFound: NOT_FOUND });
  }
}

export async function deleteChain(id) {
  try {
    await prisma.chain.delete({ where: { id } });
  } catch (error) {
    rethrowPrismaError(error, {
      notFound: NOT_FOUND,
      referenced: 'Lantul nu poate fi sters: unele hoteluri au rezervari',
    });
  }
}
