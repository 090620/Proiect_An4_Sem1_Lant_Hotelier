import { AppError } from '../errors.js';

// Transforma erorile cunoscute ale Prisma in raspunsuri HTTP potrivite.
// P2025 = inregistrarea cautata nu exista; P2003 = inregistrarea este folosita de alte tabele.
export function rethrowPrismaError(
  error,
  { notFound = 'Resursa nu exista', referenced = 'Resursa este folosita in alta parte' } = {},
) {
  if (error.code === 'P2025') {
    throw new AppError(404, notFound);
  }
  if (error.code === 'P2003') {
    throw new AppError(409, referenced);
  }
  throw error;
}
