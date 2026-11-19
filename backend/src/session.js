import session from 'express-session';
import { config } from './config.js';

export const SESSION_COOKIE_NAME = 'sid';

export const sessionCookieOptions = {
  httpOnly: true,
  sameSite: 'lax',
  secure: config.isProduction,
  maxAge: 1000 * 60 * 60 * 24 * 7,
};

// Sesiunile sunt tinute in memorie: se pierd la repornirea serverului.
// Pentru productie se trece pe un store persistent (PostgreSQL).
export const sessionMiddleware = session({
  name: SESSION_COOKIE_NAME,
  secret: config.sessionSecret,
  resave: false,
  saveUninitialized: false,
  cookie: sessionCookieOptions,
});
