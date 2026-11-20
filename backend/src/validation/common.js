import { z } from 'zod';

// Id din adresa (/api/chains/5): se converteste din text in numar intreg pozitiv.
export const idSchema = z.coerce
  .number({ invalid_type_error: 'Id invalid' })
  .int('Id invalid')
  .positive('Id invalid');
