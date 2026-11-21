import { useState } from 'react';
import { Link } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import PageHeader from '../components/PageHeader.jsx';
import { EmptyState, Loading } from '../components/StateMessage.jsx';
import { useApi } from '../hooks/useApi.js';

export default function HotelsPage() {
  const hotelsApi = useApi('/hotels');
  const chainsApi = useApi('/chains');
  const [search, setSearch] = useState('');
  const [chainId, setChainId] = useState('');

  const loading = hotelsApi.loading || chainsApi.loading;
  const error = hotelsApi.error || chainsApi.error;
  const hotels = hotelsApi.data?.hotels ?? [];
  const chains = chainsApi.data?.chains ?? [];
  const chainNames = Object.fromEntries(chains.map((chain) => [chain.id, chain.name]));

  const term = search.trim().toLowerCase();
  const visibleHotels = hotels.filter(
    (hotel) =>
      (!chainId || hotel.chainId === Number(chainId)) &&
      (!term || `${hotel.name} ${hotel.city}`.toLowerCase().includes(term)),
  );

  return (
    <section>
      <PageHeader title="Hoteluri" subtitle="Alege un hotel ca să vezi camerele și serviciile." />
      <div className="toolbar">
        <input
          type="search"
          className="field"
          placeholder="Caută după nume sau oraș"
          aria-label="Caută hotel"
          value={search}
          onChange={(event) => setSearch(event.target.value)}
        />
        <select
          className="field"
          aria-label="Filtrează după lanț"
          value={chainId}
          onChange={(event) => setChainId(event.target.value)}
        >
          <option value="">Toate lanțurile</option>
          {chains.map((chain) => (
            <option key={chain.id} value={chain.id}>
              {chain.name}
            </option>
          ))}
        </select>
      </div>
      <FormError error={error} />
      {loading && !error && <Loading label="Se încarcă hotelurile..." />}
      {!loading && !error && visibleHotels.length === 0 && (
        <EmptyState>Nu s-a găsit niciun hotel.</EmptyState>
      )}
      <div className="hotel-grid">
        {visibleHotels.map((hotel) => (
          <article key={hotel.id} className="card hotel-card">
            <div className={`hotel-cover cover-${hotel.id % 4}`}>{hotel.city}</div>
            <div className="hotel-body">
              <h2>{hotel.name}</h2>
              <div>
                <span className="badge badge-accent">
                  {chainNames[hotel.chainId] ?? 'Lanț hotelier'}
                </span>
              </div>
              <p className="hotel-address">
                {hotel.address}, {hotel.city}, {hotel.country}
              </p>
              {hotel.description && <p className="hotel-desc">{hotel.description}</p>}
              <div className="hotel-footer">
                <Link to={`/hotels/${hotel.id}`} className="button button-outline">
                  Vezi detalii
                </Link>
              </div>
            </div>
          </article>
        ))}
      </div>
    </section>
  );
}
