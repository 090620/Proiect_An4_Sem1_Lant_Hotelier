import { Link } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import PageHeader from '../components/PageHeader.jsx';
import { EmptyState, Loading } from '../components/StateMessage.jsx';
import { useApi } from '../hooks/useApi.js';

export default function ChainsPage() {
  const chainsApi = useApi('/chains');
  const hotelsApi = useApi('/hotels');

  const loading = chainsApi.loading || hotelsApi.loading;
  const error = chainsApi.error || hotelsApi.error;
  const chains = chainsApi.data?.chains ?? [];
  const hotels = hotelsApi.data?.hotels ?? [];

  return (
    <section>
      <PageHeader title="Lanțuri hoteliere" subtitle="Fiecare lanț, cu hotelurile lui." />
      <FormError error={error} />
      {loading && !error && <Loading label="Se încarcă lanțurile..." />}
      {!loading && !error && chains.length === 0 && (
        <EmptyState>Nu există lanțuri hoteliere.</EmptyState>
      )}
      <div className="chain-grid">
        {chains.map((chain) => {
          const chainHotels = hotels.filter((hotel) => hotel.chainId === chain.id);
          return (
            <article key={chain.id} className="card chain-card">
              <h2>
                {chain.name}
                <span className="badge badge-accent">
                  {chainHotels.length} {chainHotels.length === 1 ? 'hotel' : 'hoteluri'}
                </span>
              </h2>
              {chain.description && <p>{chain.description}</p>}
              <ul className="link-list">
                {chainHotels.map((hotel) => (
                  <li key={hotel.id}>
                    <Link to={`/hotels/${hotel.id}`}>
                      {hotel.name}
                      <span>{hotel.city}</span>
                    </Link>
                  </li>
                ))}
              </ul>
            </article>
          );
        })}
      </div>
    </section>
  );
}
