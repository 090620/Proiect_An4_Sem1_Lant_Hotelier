import express from 'express';
import cors from 'cors';
import { config } from './config.js';
import healthRouter from './routes/health.js';

const app = express();

app.use(cors({ origin: config.frontendOrigin, credentials: true }));
app.use(express.json());
app.use('/api/health', healthRouter);

export default app;
