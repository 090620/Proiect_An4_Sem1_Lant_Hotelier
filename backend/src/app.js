import express from 'express';
import cors from 'cors';
import { config } from './config.js';
import { sessionMiddleware } from './session.js';
import healthRouter from './routes/health.js';
import authRouter from './routes/auth.js';
import chainsRouter from './routes/chains.js';
import hotelsRouter from './routes/hotels.js';
import roomsRouter from './routes/rooms.js';
import servicesRouter from './routes/services.js';
import { notFound, errorHandler } from './middleware/errorHandler.js';

const app = express();

app.use(cors({ origin: config.frontendOrigin, credentials: true }));
app.use(express.json());
app.use(sessionMiddleware);

app.use('/api/health', healthRouter);
app.use('/api/auth', authRouter);
app.use('/api/chains', chainsRouter);
app.use('/api/hotels', hotelsRouter);
app.use('/api/rooms', roomsRouter);
app.use('/api/services', servicesRouter);

app.use(notFound);
app.use(errorHandler);

export default app;
