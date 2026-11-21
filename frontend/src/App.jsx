import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AuthProvider from './context/AuthProvider.jsx';
import Layout from './components/Layout.jsx';
import ProtectedRoute from './components/ProtectedRoute.jsx';
import HotelsPage from './pages/HotelsPage.jsx';
import HotelDetailPage from './pages/HotelDetailPage.jsx';
import ChainsPage from './pages/ChainsPage.jsx';
import RoomsPage from './pages/RoomsPage.jsx';
import ServicesPage from './pages/ServicesPage.jsx';
import HomePage from './pages/HomePage.jsx';
import LoginPage from './pages/LoginPage.jsx';
import RegisterPage from './pages/RegisterPage.jsx';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route element={<Layout />}>
            <Route path="/" element={<HomePage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route element={<ProtectedRoute />}>
              <Route path="/hotels" element={<HotelsPage />} />
              <Route path="/hotels/:id" element={<HotelDetailPage />} />
              <Route path="/chains" element={<ChainsPage />} />
              <Route path="/rooms" element={<RoomsPage />} />
              <Route path="/services" element={<ServicesPage />} />
            </Route>
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}
