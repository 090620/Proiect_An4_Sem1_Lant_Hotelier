import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Cererile catre /api sunt trimise de serverul Vite la backend, deci browserul vede o singura
// origine (http://localhost:5173) si cookie-ul sesiunii merge fara configurare CORS.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/api': 'http://localhost:3000',
    },
  },
});
