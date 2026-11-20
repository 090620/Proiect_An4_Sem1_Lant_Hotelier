import { Router } from 'express';
import { loginSchema, registerSchema } from '../validation/auth.js';
import * as authService from '../services/authService.js';
import { requireAuth } from '../middleware/auth.js';
import {
  SESSION_COOKIE_NAME,
  destroySession,
  regenerateSession,
  sessionCookieOptions,
} from '../session.js';

const router = Router();

router.post('/register', async (req, res) => {
  const data = registerSchema.parse(req.body);
  const user = await authService.register(data);
  res.status(201).json({ user });
});

router.post('/login', async (req, res) => {
  const data = loginSchema.parse(req.body);
  const user = await authService.login(data);
  await regenerateSession(req);
  req.session.userId = user.id;
  res.json({ user });
});

router.post('/logout', async (req, res) => {
  await destroySession(req);
  res.clearCookie(SESSION_COOKIE_NAME, {
    httpOnly: sessionCookieOptions.httpOnly,
    sameSite: sessionCookieOptions.sameSite,
    secure: sessionCookieOptions.secure,
  });
  res.status(204).end();
});

router.get('/me', requireAuth, (req, res) => {
  res.json({ user: req.user });
});

export default router;
