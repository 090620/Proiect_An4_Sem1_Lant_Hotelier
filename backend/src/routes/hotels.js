import { Router } from 'express';
import { requireAuth, requireRole } from '../middleware/auth.js';
import { hotelSchema, hotelFilterSchema } from '../validation/hotel.js';
import { idSchema } from '../validation/common.js';
import * as hotelService from '../services/hotelService.js';

const router = Router();

// Toate rutele cer autentificare; scrierea cere in plus rolul ADMIN.
router.use(requireAuth);

router.get('/', async (req, res) => {
  const filter = hotelFilterSchema.parse(req.query);
  const hotels = await hotelService.listHotels(filter);
  res.json({ hotels });
});

router.get('/:id', async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const hotel = await hotelService.getHotel(id);
  res.json({ hotel });
});

router.post('/', requireRole('ADMIN'), async (req, res) => {
  const data = hotelSchema.parse(req.body);
  const hotel = await hotelService.createHotel(data);
  res.status(201).json({ hotel });
});

router.put('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const data = hotelSchema.parse(req.body);
  const hotel = await hotelService.updateHotel(id, data);
  res.json({ hotel });
});

router.delete('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  await hotelService.deleteHotel(id);
  res.status(204).end();
});

export default router;
