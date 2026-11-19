import { Router } from 'express';
import { registerSchema } from '../validation/auth.js';
import * as authService from '../services/authService.js';

const router = Router();

router.post('/register', async (req, res) => {
  const data = registerSchema.parse(req.body);
  const user = await authService.register(data);
  res.status(201).json({ user });
});

export default router;
