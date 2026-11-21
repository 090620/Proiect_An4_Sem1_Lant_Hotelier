import { useEffect, useState } from 'react';
import { api } from '../api/client.js';

// Incarca datele de la API. Cat timp rezultatul nu corespunde caii cerute, `loading` este true.
export function useApi(path) {
  const [result, setResult] = useState({ path: null, data: null, error: null });

  useEffect(() => {
    let active = true;
    api(path)
      .then((data) => active && setResult({ path, data, error: null }))
      .catch((error) => active && setResult({ path, data: null, error }));
    return () => {
      active = false;
    };
  }, [path]);

  const current = result.path === path;
  return {
    data: current ? result.data : null,
    error: current ? result.error : null,
    loading: !current,
  };
}
