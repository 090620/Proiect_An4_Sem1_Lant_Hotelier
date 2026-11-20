import { z } from 'zod';
import { priceSchema } from './money.js';

export const roomSchema = z.object({
  type: z.string().trim().min(2, 'Tipul este prea scurt').max(50, 'Tipul este prea lung'),
  price: priceSchema,
  description: z.string().trim().max(1000, 'Descrierea este prea lunga').nullish(),
  hotelId: z
    .number({
      required_error: 'hotelId este obligatoriu',
      invalid_type_error: 'hotelId trebuie sa fie un numar',
    })
    .int('hotelId trebuie sa fie un numar intreg')
    .positive('hotelId trebuie sa fie pozitiv'),
});

export const roomFilterSchema = z.object({
  hotelId: z.coerce
    .number({ invalid_type_error: 'hotelId invalid' })
    .int('hotelId invalid')
    .positive('hotelId invalid')
    .optional(),
});
