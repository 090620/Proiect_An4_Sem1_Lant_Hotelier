import { ZodError } from 'zod';
import { AppError } from '../errors.js';

export function notFound(req, res) {
  res.status(404).json({ error: 'Ruta nu exista' });
}

// Express recunoaste handlerul de erori dupa cele 4 parametri, chiar daca `next` nu e folosit.
// eslint-disable-next-line no-unused-vars
export function errorHandler(err, req, res, next) {
  if (err instanceof ZodError) {
    const details = err.issues.map((issue) => ({
      field: issue.path.join('.'),
      message: issue.message,
    }));
    return res.status(400).json({ error: 'Date invalide', details });
  }

  if (err instanceof AppError) {
    return res.status(err.status).json({ error: err.message, details: err.details });
  }

  if (err.type === 'entity.parse.failed') {
    return res.status(400).json({ error: 'JSON invalid' });
  }

  console.error(err);
  return res.status(500).json({ error: 'Eroare interna a serverului' });
}
