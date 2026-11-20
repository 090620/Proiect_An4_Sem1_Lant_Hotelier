import { z } from 'zod';
import { priceSchema } from './money.js';

const positiveInt = (name) =>
  z
    .number({ invalid_type_error: `${name} trebuie sa fie un numar` })
    .int(`${name} trebuie sa fie un numar intreg`)
    .positive(`${name} trebuie sa fie pozitiv`);

export const serviceSchema = z.object({
  name: z.string().trim().min(2, 'Numele este prea scurt').max(100, 'Numele este prea lung'),
  description: z.string().trim().max(1000, 'Descrierea este prea lunga').nullish(),
  price: priceSchema.nullish(),
  icon: z.string().trim().max(50, 'Iconita este prea lunga').nullish(),
  chainId: positiveInt('chainId'),
  // Hotelurile care ofera serviciul; trebuie sa apartina aceluiasi lant.
  hotelIds: z.array(positiveInt('hotelIds')).max(200, 'Prea multe hoteluri').default([]),
});

const filterId = (name) =>
  z.coerce
    .number({ invalid_type_error: `${name} invalid` })
    .int(`${name} invalid`)
    .positive(`${name} invalid`)
    .optional();

export const serviceFilterSchema = z.object({
  chainId: filterId('chainId'),
  hotelId: filterId('hotelId'),
});
