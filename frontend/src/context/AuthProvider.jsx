import { useEffect, useState } from 'react';
import { api } from '../api/client.js';
import { AuthContext } from './AuthContext.js';

export default function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  // La pornire se intreaba backend-ul daca exista deja o sesiune (cookie valid).
  useEffect(() => {
    let active = true;
    api('/auth/me')
      .then((data) => active && setUser(data.user))
      .catch(() => active && setUser(null))
      .finally(() => active && setLoading(false));
    return () => {
      active = false;
    };
  }, []);

  async function login(email, password) {
    const data = await api('/auth/login', { method: 'POST', body: { email, password } });
    setUser(data.user);
    return data.user;
  }

  // Inregistrarea nu porneste o sesiune pe backend, deci dupa ea se face login.
  async function register(fullName, email, password) {
    await api('/auth/register', { method: 'POST', body: { fullName, email, password } });
    return login(email, password);
  }

  async function logout() {
    await api('/auth/logout', { method: 'POST' });
    setUser(null);
  }

  return (
    <AuthContext.Provider value={{ user, loading, login, register, logout }}>
      {children}
    </AuthContext.Provider>
  );
}
