import bcrypt from 'bcryptjs';
import { prisma } from '../db.js';
import { AppError } from '../errors.js';

const BCRYPT_ROUNDS = 10;

const publicUserFields = {
  id: true,
  email: true,
  fullName: true,
  role: true,
  profilePicture: true,
  createdAt: true,
};

export async function register({ email, password, fullName }) {
  const existing = await prisma.user.findUnique({ where: { email }, select: { id: true } });
  if (existing) {
    throw new AppError(409, 'Exista deja un cont cu acest email');
  }

  const passwordHash = await bcrypt.hash(password, BCRYPT_ROUNDS);

  try {
    // Conturile create prin inregistrare au intotdeauna rolul USER.
    return await prisma.user.create({
      data: { email, passwordHash, fullName, role: 'USER' },
      select: publicUserFields,
    });
  } catch (error) {
    // P2002 = incalcarea unei constrangeri unice (doua inregistrari simultane cu acelasi email)
    if (error.code === 'P2002') {
      throw new AppError(409, 'Exista deja un cont cu acest email');
    }
    throw error;
  }
}
