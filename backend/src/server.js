import app from './app.js';
import { config } from './config.js';

app.listen(config.port, () => {
  console.log(`API pornit pe http://localhost:${config.port}`);
});
