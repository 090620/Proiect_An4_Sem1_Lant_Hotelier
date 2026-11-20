import { z } from 'zod';

// Suma in RON, cu cel mult 2 zecimale (coloana Decimal(10, 2) din baza de date).
export const priceSchema = z
  .number({
    required_error: 'Pretul este obligatoriu',
    invalid_type_error: 'Pretul trebuie sa fie un numar',
  })
  .positive('Pretul trebuie sa fie pozitiv')
  .max(99999999.99, 'Pretul este prea mare')
  .multipleOf(0.01, 'Pretul poate avea cel mult 2 zecimale');
