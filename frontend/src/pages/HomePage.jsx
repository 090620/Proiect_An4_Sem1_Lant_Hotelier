import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth.js';

export default function HomePage() {
  const { user, loading } = useAuth();

  if (loading) {
    return <p className="muted">Se incarca...</p>;
  }

  return (
    <section className="card">
      <h1>HotelExplorer</h1>
      {user ? (
        <p>
          Esti autentificat ca <strong>{user.fullName}</strong> ({user.role}).{' '}
          <Link to="/hotels">Vezi hotelurile</Link>
        </p>
      ) : (
        <p>
          <Link to="/login">Autentifica-te</Link> sau <Link to="/register">creeaza un cont</Link> ca
          sa continui.
        </p>
      )}
    </section>
  );
}
