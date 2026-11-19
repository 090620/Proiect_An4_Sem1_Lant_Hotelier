import 'dotenv/config';

const nodeEnv = process.env.NODE_ENV || 'development';
const isProduction = nodeEnv === 'production';

if (isProduction && !process.env.SESSION_SECRET) {
  throw new Error('SESSION_SECRET trebuie setat in productie');
}

export const config = {
  nodeEnv,
  isProduction,
  port: Number(process.env.PORT) || 3000,
  frontendOrigin: process.env.FRONTEND_ORIGIN || 'http://localhost:5173',
  sessionSecret: process.env.SESSION_SECRET || 'secret-doar-pentru-dezvoltare',
};
