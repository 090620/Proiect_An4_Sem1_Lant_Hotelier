import { Router } from 'express';
import { requireAuth, requireRole } from '../middleware/auth.js';
import { chainSchema } from '../validation/chain.js';
import { idSchema } from '../validation/common.js';
import * as chainService from '../services/chainService.js';

const router = Router();

// Toate rutele cer autentificare; scrierea cere in plus rolul ADMIN.
router.use(requireAuth);

router.get('/', async (req, res) => {
  const chains = await chainService.listChains();
  res.json({ chains });
});

router.get('/:id', async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const chain = await chainService.getChain(id);
  res.json({ chain });
});

router.post('/', requireRole('ADMIN'), async (req, res) => {
  const data = chainSchema.parse(req.body);
  const chain = await chainService.createChain(data);
  res.status(201).json({ chain });
});

router.put('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const data = chainSchema.parse(req.body);
  const chain = await chainService.updateChain(id, data);
  res.json({ chain });
});

router.delete('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  await chainService.deleteChain(id);
  res.status(204).end();
});

export default router;
