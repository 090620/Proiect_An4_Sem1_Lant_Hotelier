import { z } from 'zod';

const email = z.string().trim().toLowerCase().email('Email invalid');

export const registerSchema = z.object({
  email,
  password: z
    .string()
    .min(8, 'Parola trebuie sa aiba cel putin 8 caractere')
    .max(72, 'Parola poate avea cel mult 72 de caractere'),
  fullName: z.string().trim().min(2, 'Numele este prea scurt').max(100, 'Numele este prea lung'),
});

export const loginSchema = z.object({
  email,
  password: z.string().min(1, 'Parola este obligatorie').max(72, 'Parola este prea lunga'),
});
