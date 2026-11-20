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

// Dupa login se creeaza o sesiune noua (impotriva session fixation).
export function regenerateSession(req) {
  return new Promise((resolve, reject) => {
    req.session.regenerate((err) => (err ? reject(err) : resolve()));
  });
}

export function destroySession(req) {
  return new Promise((resolve, reject) => {
    req.session.destroy((err) => (err ? reject(err) : resolve()));
  });
}
