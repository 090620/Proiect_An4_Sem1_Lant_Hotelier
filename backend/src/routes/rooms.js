import { Router } from 'express';
import { requireAuth, requireRole } from '../middleware/auth.js';
import { roomSchema, roomFilterSchema } from '../validation/room.js';
import { idSchema } from '../validation/common.js';
import * as roomService from '../services/roomService.js';

const router = Router();

// Toate rutele cer autentificare; scrierea cere in plus rolul ADMIN.
router.use(requireAuth);

router.get('/', async (req, res) => {
  const filter = roomFilterSchema.parse(req.query);
  const rooms = await roomService.listRooms(filter);
  res.json({ rooms });
});

router.get('/:id', async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const room = await roomService.getRoom(id);
  res.json({ room });
});

router.post('/', requireRole('ADMIN'), async (req, res) => {
  const data = roomSchema.parse(req.body);
  const room = await roomService.createRoom(data);
  res.status(201).json({ room });
});

router.put('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const data = roomSchema.parse(req.body);
  const room = await roomService.updateRoom(id, data);
  res.json({ room });
});

router.delete('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  await roomService.deleteRoom(id);
  res.status(204).end();
});

export default router;
