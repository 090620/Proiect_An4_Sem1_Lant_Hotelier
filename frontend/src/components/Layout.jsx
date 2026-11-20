import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth.js';

export default function Layout() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  async function handleLogout() {
    await logout();
    navigate('/login');
  }

  return (
    <>
      <header className="header">
        <div className="header-inner">
          <Link to="/" className="brand">
            HotelExplorer
          </Link>
          <nav className="nav">
            {user ? (
              <>
                <Link to="/hotels">Hoteluri</Link>
                <span className="nav-user">{user.fullName}</span>
                <button type="button" className="button button-secondary" onClick={handleLogout}>
                  Iesi din cont
                </button>
              </>
            ) : (
              <>
                <Link to="/login">Autentificare</Link>
                <Link to="/register">Cont nou</Link>
              </>
            )}
          </nav>
        </div>
      </header>
      <main className="page">
        <Outlet />
      </main>
    </>
  );
}
