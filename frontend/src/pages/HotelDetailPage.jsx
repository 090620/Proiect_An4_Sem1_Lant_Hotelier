import { Link, useParams } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import { EmptyState, Loading } from '../components/StateMessage.jsx';
import { useApi } from '../hooks/useApi.js';
import { formatPrice } from '../lib/format.js';

export default function HotelDetailPage() {
  const { id } = useParams();
  const hotelApi = useApi(`/hotels/${id}`);
  const roomsApi = useApi(`/rooms?hotelId=${id}`);
  const servicesApi = useApi(`/services?hotelId=${id}`);

  if (hotelApi.error) {
    return (
      <section>
        <Link to="/hotels" className="back-link">
          ← Toate hotelurile
        </Link>
        <FormError error={hotelApi.error} />
      </section>
    );
  }
  if (hotelApi.loading) {
    return <Loading label="Se încarcă hotelul..." />;
  }

  const hotel = hotelApi.data.hotel;
  const rooms = roomsApi.data?.rooms ?? [];
  const services = servicesApi.data?.services ?? [];

  return (
    <section>
      <Link to="/hotels" className="back-link">
        ← Toate hotelurile
      </Link>

      <div className="detail-hero">
        <div className="chip-row">
          {hotel.chain && <span className="badge">{hotel.chain.name}</span>}
          <span className="badge">
            {hotel.city}, {hotel.country}
          </span>
        </div>
        <h1>{hotel.name}</h1>
        <p>{hotel.address}</p>
        {hotel.description && <p>{hotel.description}</p>}
      </div>

      <div className="section">
        <h2>Camere</h2>
        <FormError error={roomsApi.error} />
        {roomsApi.loading && <Loading label="Se încarcă camerele..." />}
        {!roomsApi.loading && !roomsApi.error && rooms.length === 0 && (
          <EmptyState>Hotelul nu are camere adăugate.</EmptyState>
        )}
        <div className="room-grid">
          {rooms.map((room) => (
            <article key={room.id} className="card room-card">
              <h3>{room.type}</h3>
              {room.description && <p>{room.description}</p>}
              <div className="price">
                {formatPrice(room.price)} <small>/ noapte</small>
              </div>
            </article>
          ))}
        </div>
      </div>

      <div className="section">
        <h2>Servicii</h2>
        <FormError error={servicesApi.error} />
        {servicesApi.loading && <Loading label="Se încarcă serviciile..." />}
        {!servicesApi.loading && !servicesApi.error && services.length === 0 && (
          <EmptyState>Hotelul nu are servicii adăugate.</EmptyState>
        )}
        <div className="service-chips">
          {services.map((service) => (
            <div key={service.id} className="service-chip">
              {service.name}
              <span>{service.price ? formatPrice(service.price) : 'gratuit'}</span>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
