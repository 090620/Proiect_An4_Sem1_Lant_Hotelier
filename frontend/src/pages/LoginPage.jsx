import { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import { useAuth } from '../hooks/useAuth.js';

export default function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const [submitting, setSubmitting] = useState(false);

  async function handleSubmit(event) {
    event.preventDefault();
    setError(null);
    setSubmitting(true);
    try {
      await login(email, password);
      navigate(location.state?.from ?? '/', { replace: true });
    } catch (err) {
      setError(err);
      setSubmitting(false);
    }
  }

  return (
    <section className="card auth-card">
      <h1>Bine ai revenit</h1>
      <p className="auth-subtitle">Autentifică-te ca să vezi hotelurile.</p>
      <form onSubmit={handleSubmit}>
        <label>
          Email
          <input
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            autoComplete="email"
            required
          />
        </label>
        <label>
          Parola
          <input
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            autoComplete="current-password"
            required
          />
        </label>
        <FormError error={error} />
        <button type="submit" className="button" disabled={submitting}>
          {submitting ? 'Se autentifică...' : 'Intră în cont'}
        </button>
      </form>
      <p className="auth-switch">
        Nu ai cont? <Link to="/register">Creează unul</Link>
      </p>
    </section>
  );
}
