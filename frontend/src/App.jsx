import { useEffect, useState } from 'react';

export default function App() {
  const [apiStatus, setApiStatus] = useState('se verifica...');

  useEffect(() => {
    fetch('/api/health')
      .then((response) => response.json())
      .then((data) => setApiStatus(data.status))
      .catch(() => setApiStatus('indisponibil'));
  }, []);

  return (
    <main className="page">
      <h1>HotelExplorer</h1>
      <p>
        Starea API-ului: <strong>{apiStatus}</strong>
      </p>
    </main>
  );
}
