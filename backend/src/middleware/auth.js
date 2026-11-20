import { prisma } from '../db.js';
import { AppError } from '../errors.js';
import { publicUserFields } from '../services/authService.js';

// Cere o sesiune valida. Utilizatorul se citeste din baza de date la fiecare cerere,
// deci un cont sters sau o schimbare de rol are efect imediat.
export async function requireAuth(req, res, next) {
  const userId = req.session.userId;
  if (!userId) {
    throw new AppError(401, 'Trebuie sa fii autentificat');
  }

  const user = await prisma.user.findUnique({
    where: { id: userId },
    select: publicUserFields,
  });
  if (!user) {
    delete req.session.userId;
    throw new AppError(401, 'Trebuie sa fii autentificat');
  }

  req.user = user;
  next();
}

// Se foloseste dupa requireAuth: router.post('/', requireAuth, requireRole('ADMIN'), handler)
export function requireRole(...roles) {
  return (req, res, next) => {
    if (!req.user) {
      throw new AppError(401, 'Trebuie sa fii autentificat');
    }
    if (!roles.includes(req.user.role)) {
      throw new AppError(403, 'Nu ai permisiunea necesara');
    }
    next();
  };
}
