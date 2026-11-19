import express from 'express';
import cors from 'cors';
import { config } from './config.js';
import { sessionMiddleware } from './session.js';
import healthRouter from './routes/health.js';
import authRouter from './routes/auth.js';
import { notFound, errorHandler } from './middleware/errorHandler.js';

const app = express();

app.use(cors({ origin: config.frontendOrigin, credentials: true }));
app.use(express.json());
app.use(sessionMiddleware);

app.use('/api/health', healthRouter);
app.use('/api/auth', authRouter);

app.use(notFound);
app.use(errorHandler);

export default app;
