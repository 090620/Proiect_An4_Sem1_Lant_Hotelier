import { Link, NavLink, Outlet, useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth.js';

const NAV_LINKS = [
  { to: '/hotels', label: 'Hoteluri' },
  { to: '/chains', label: 'Lanțuri' },
];

export default function Layout() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  async function handleLogout() {
    await logout();
    navigate('/login');
  }

  return (
    <div className="app">
      <header className="header">
        <div className="header-inner">
          <Link to="/" className="brand">
            <span className="brand-mark" aria-hidden="true">
              H
            </span>
            HotelExplorer
          </Link>
          {user && (
            <nav className="main-nav" aria-label="Navigare principală">
              {NAV_LINKS.map((link) => (
                <NavLink
                  key={link.to}
                  to={link.to}
                  className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}
                >
                  {link.label}
                </NavLink>
              ))}
            </nav>
          )}
          <div className="header-actions">
            {user ? (
              <>
                <div className="user-chip">
                  <span className="user-avatar" aria-hidden="true">
                    {user.fullName.charAt(0).toUpperCase()}
                  </span>
                  <span className="user-name">{user.fullName}</span>
                  {user.role === 'ADMIN' && <span className="role-tag">Admin</span>}
                </div>
                <button type="button" className="button button-ghost" onClick={handleLogout}>
                  Ieșire
                </button>
              </>
            ) : (
              <>
                <Link to="/login" className="button button-ghost">
                  Autentificare
                </Link>
                <Link to="/register" className="button button-accent">
                  Cont nou
                </Link>
              </>
            )}
          </div>
        </div>
      </header>
      <main className="page">
        <Outlet />
      </main>
      <footer className="footer">HotelExplorer · proiect universitar</footer>
    </div>
  );
}
