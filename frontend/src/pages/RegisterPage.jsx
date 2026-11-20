import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import FormError from '../components/FormError.jsx';
import { useAuth } from '../hooks/useAuth.js';

export default function RegisterPage() {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const [submitting, setSubmitting] = useState(false);

  async function handleSubmit(event) {
    event.preventDefault();
    setError(null);
    setSubmitting(true);
    try {
      await register(fullName, email, password);
      navigate('/', { replace: true });
    } catch (err) {
      setError(err);
      setSubmitting(false);
    }
  }

  return (
    <section className="card auth-card">
      <h1>Cont nou</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Nume complet
          <input
            type="text"
            value={fullName}
            onChange={(event) => setFullName(event.target.value)}
            autoComplete="name"
            required
          />
        </label>
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
          Parola (minim 8 caractere)
          <input
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            autoComplete="new-password"
            minLength={8}
            required
          />
        </label>
        <FormError error={error} />
        <button type="submit" className="button" disabled={submitting}>
          {submitting ? 'Se creeaza contul...' : 'Creeaza cont'}
        </button>
      </form>
      <p className="muted">
        Ai deja cont? <Link to="/login">Autentifica-te</Link>
      </p>
    </section>
  );
}
