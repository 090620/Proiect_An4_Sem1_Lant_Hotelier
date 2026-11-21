import { useState } from 'react';
import { Link } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import PageHeader from '../components/PageHeader.jsx';
import { EmptyState, Loading } from '../components/StateMessage.jsx';
import { useApi } from '../hooks/useApi.js';
import { formatPrice } from '../lib/format.js';

export default function ServicesPage() {
  const servicesApi = useApi('/services');
  const chainsApi = useApi('/chains');
  const [chainId, setChainId] = useState('');

  const loading = servicesApi.loading || chainsApi.loading;
  const error = servicesApi.error || chainsApi.error;
  const services = servicesApi.data?.services ?? [];
  const chains = chainsApi.data?.chains ?? [];
  const chainNames = Object.fromEntries(chains.map((chain) => [chain.id, chain.name]));
  const visibleServices = chainId
    ? services.filter((service) => service.chainId === Number(chainId))
    : services;

  return (
    <section>
      <PageHeader title="Servicii" subtitle="Serviciile oferite și hotelurile unde le găsești." />
      <div className="toolbar">
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
      {loading && !error && <Loading label="Se încarcă serviciile..." />}
      {!loading && !error && visibleServices.length === 0 && (
        <EmptyState>Nu s-a găsit niciun serviciu.</EmptyState>
      )}
      <div className="service-grid">
        {visibleServices.map((service) => (
          <article key={service.id} className="card service-card">
            <div className="service-top">
              <span className="service-icon" aria-hidden="true">
                {service.name.charAt(0).toUpperCase()}
              </span>
              <div>
                <h2>{service.name}</h2>
                <span className="badge">{chainNames[service.chainId] ?? 'Lanț hotelier'}</span>
              </div>
            </div>
            {service.description && <p>{service.description}</p>}
            <div className="price">{service.price ? formatPrice(service.price) : 'Gratuit'}</div>
            {service.hotels?.length > 0 && (
              <div className="service-hotels">
                {service.hotels.map((hotel) => (
                  <Link key={hotel.id} to={`/hotels/${hotel.id}`}>
                    <span className="badge">{hotel.name}</span>
                  </Link>
                ))}
              </div>
            )}
          </article>
        ))}
      </div>
    </section>
  );
}
