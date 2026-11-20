import { useEffect, useState } from 'react';
import { api } from '../api/client.js';
import FormError from '../components/FormError.jsx';

export default function HotelsPage() {
  const [hotels, setHotels] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [search, setSearch] = useState('');

  useEffect(() => {
    let active = true;
    api('/hotels')
      .then((data) => active && setHotels(data.hotels))
      .catch((err) => active && setError(err))
      .finally(() => active && setLoading(false));
    return () => {
      active = false;
    };
  }, []);

  const term = search.trim().toLowerCase();
  const visibleHotels = term
    ? hotels.filter((hotel) => `${hotel.name} ${hotel.city}`.toLowerCase().includes(term))
    : hotels;

  return (
    <section>
      <h1>Hoteluri</h1>
      <input
        type="search"
        className="search"
        placeholder="Cauta dupa nume sau oras"
        value={search}
        onChange={(event) => setSearch(event.target.value)}
      />
      <FormError error={error} />
      {loading && <p className="muted">Se incarca hotelurile...</p>}
      {!loading && !error && visibleHotels.length === 0 && (
        <p className="muted">Nu s-a gasit niciun hotel.</p>
      )}
      <div className="hotel-grid">
        {visibleHotels.map((hotel) => (
          <article key={hotel.id} className="card hotel-card">
            <h2>{hotel.name}</h2>
            <p className="muted">
              {hotel.city}, {hotel.country}
            </p>
            <p>{hotel.address}</p>
            {hotel.description && <p className="muted">{hotel.description}</p>}
          </article>
        ))}
      </div>
    </section>
  );
}
