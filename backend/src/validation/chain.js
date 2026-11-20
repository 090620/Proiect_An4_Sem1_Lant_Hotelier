import { z } from 'zod';

export const chainSchema = z.object({
  name: z.string().trim().min(2, 'Numele este prea scurt').max(100, 'Numele este prea lung'),
  description: z.string().trim().max(1000, 'Descrierea este prea lunga').nullish(),
});
