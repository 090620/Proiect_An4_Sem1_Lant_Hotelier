import { useState } from 'react';
import { Link } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import PageHeader from '../components/PageHeader.jsx';
import { EmptyState, Loading } from '../components/StateMessage.jsx';
import { useApi } from '../hooks/useApi.js';
import { formatPrice } from '../lib/format.js';

export default function RoomsPage() {
  const roomsApi = useApi('/rooms');
  const hotelsApi = useApi('/hotels');
  const [hotelId, setHotelId] = useState('');

  const loading = roomsApi.loading || hotelsApi.loading;
  const error = roomsApi.error || hotelsApi.error;
  const rooms = roomsApi.data?.rooms ?? [];
  const hotels = hotelsApi.data?.hotels ?? [];
  const hotelsById = Object.fromEntries(hotels.map((hotel) => [hotel.id, hotel]));
  const visibleRooms = hotelId ? rooms.filter((room) => room.hotelId === Number(hotelId)) : rooms;

  return (
    <section>
      <PageHeader title="Camere" subtitle="Toate tipurile de camere, ordonate după preț." />
      <div className="toolbar">
        <select
          className="field"
          aria-label="Filtrează după hotel"
          value={hotelId}
          onChange={(event) => setHotelId(event.target.value)}
        >
          <option value="">Toate hotelurile</option>
          {hotels.map((hotel) => (
            <option key={hotel.id} value={hotel.id}>
              {hotel.name}
            </option>
          ))}
        </select>
      </div>
      <FormError error={error} />
      {loading && !error && <Loading label="Se încarcă camerele..." />}
      {!loading && !error && visibleRooms.length === 0 && (
        <EmptyState>Nu s-a găsit nicio cameră.</EmptyState>
      )}
      {visibleRooms.length > 0 && (
        <div className="table-wrap">
          <table className="data-table">
            <thead>
              <tr>
                <th>Hotel</th>
                <th>Tip</th>
                <th>Descriere</th>
                <th className="num">Preț / noapte</th>
              </tr>
            </thead>
            <tbody>
              {visibleRooms.map((room) => {
                const hotel = hotelsById[room.hotelId];
                return (
                  <tr key={room.id}>
                    <td>{hotel ? <Link to={`/hotels/${hotel.id}`}>{hotel.name}</Link> : '–'}</td>
                    <td>{room.type}</td>
                    <td className="muted">{room.description ?? '–'}</td>
                    <td className="num">{formatPrice(room.price)}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      )}
    </section>
  );
}
