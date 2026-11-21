import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth.js';

const SHORTCUTS = [
  { to: '/hotels', title: 'Hoteluri', text: 'Toate hotelurile, cu căutare și filtru pe lanț.' },
  { to: '/chains', title: 'Lanțuri', text: 'Lanțurile hoteliere și hotelurile fiecăruia.' },
  { to: '/rooms', title: 'Camere', text: 'Tipurile de camere și prețurile pe noapte.' },
  { to: '/services', title: 'Servicii', text: 'Serviciile oferite și hotelurile unde le găsești.' },
];

export default function HomePage() {
  const { user, loading } = useAuth();

  if (loading) {
    return <p className="state muted">Se încarcă...</p>;
  }

  const firstName = user?.fullName.split(' ')[0];

  return (
    <>
      <section className="hero">
        <p className="hero-kicker">HotelExplorer</p>
        <h1>{user ? `Bine ai venit, ${firstName}!` : 'Descoperă hotelurile noastre'}</h1>
        <p className="hero-text">
          Răsfoiește hoteluri, camere și servicii din lanțurile hoteliere, într-un singur loc.
        </p>
        <div className="hero-actions">
          {user ? (
            <Link to="/hotels" className="button button-accent">
              Vezi hotelurile
            </Link>
          ) : (
            <>
              <Link to="/login" className="button button-accent">
                Autentificare
              </Link>
              <Link to="/register" className="button button-light">
                Creează un cont
              </Link>
            </>
          )}
        </div>
      </section>
      {user && (
        <section className="shortcut-grid">
          {SHORTCUTS.map((shortcut) => (
            <Link key={shortcut.to} to={shortcut.to} className="card shortcut">
              <h2>{shortcut.title}</h2>
              <p>{shortcut.text}</p>
            </Link>
          ))}
        </section>
      )}
    </>
  );
}
