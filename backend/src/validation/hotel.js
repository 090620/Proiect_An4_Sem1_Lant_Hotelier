import { z } from 'zod';

const requiredText = (max) =>
  z.string().trim().min(2, 'Valoarea este prea scurta').max(max, 'Valoarea este prea lunga');

export const hotelSchema = z.object({
  name: requiredText(100),
  city: requiredText(100),
  country: requiredText(100),
  address: requiredText(200),
  description: z.string().trim().max(1000, 'Descrierea este prea lunga').nullish(),
  image: z.string().trim().max(500, 'Calea imaginii este prea lunga').nullish(),
  chainId: z
    .number({
      required_error: 'chainId este obligatoriu',
      invalid_type_error: 'chainId trebuie sa fie un numar',
    })
    .int('chainId trebuie sa fie un numar intreg')
    .positive('chainId trebuie sa fie pozitiv'),
});

export const hotelFilterSchema = z.object({
  chainId: z.coerce
    .number({ invalid_type_error: 'chainId invalid' })
    .int('chainId invalid')
    .positive('chainId invalid')
    .optional(),
});
