import { Router } from 'express';
import { requireAuth, requireRole } from '../middleware/auth.js';
import { serviceSchema, serviceFilterSchema } from '../validation/service.js';
import { idSchema } from '../validation/common.js';
import * as serviceService from '../services/serviceService.js';

const router = Router();

// Toate rutele cer autentificare; scrierea cere in plus rolul ADMIN.
router.use(requireAuth);

router.get('/', async (req, res) => {
  const filter = serviceFilterSchema.parse(req.query);
  const services = await serviceService.listServices(filter);
  res.json({ services });
});

router.get('/:id', async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const service = await serviceService.getService(id);
  res.json({ service });
});

router.post('/', requireRole('ADMIN'), async (req, res) => {
  const data = serviceSchema.parse(req.body);
  const service = await serviceService.createService(data);
  res.status(201).json({ service });
});

router.put('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  const data = serviceSchema.parse(req.body);
  const service = await serviceService.updateService(id, data);
  res.json({ service });
});

router.delete('/:id', requireRole('ADMIN'), async (req, res) => {
  const id = idSchema.parse(req.params.id);
  await serviceService.deleteService(id);
  res.status(204).end();
});

export default router;
