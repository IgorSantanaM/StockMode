import React from 'react';
import { useState } from 'react';
import { BrowserRouter, useLocation } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';

import { GlobalStyle } from './styles/global';
import Sidebar from './components/Sidebar'; 
import Header from './components/Header';
import AppRoutes from './routes';
import { MainContent, AppContainer, PageWrapper } from './Appstyles';
import { LoadingContainer } from './util/LoadingContainer';

function App() {
  const auth = useAuth();
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const location = useLocation();

  if (auth.error) {
    return <div>Erro na autenticação: {auth.error.message}</div>;
  }

  // Allow access to login/signin-oidc routes even when not authenticated
  if (!auth.isAuthenticated && !['/login', '/signin-oidc'].includes(location.pathname)) {
    return (
      <div style={{ 
        display: 'flex', 
        flexDirection: 'column', 
        alignItems: 'center', 
        justifyContent: 'center', 
        height: '100vh',
        gap: '20px',
        background: 'linear-gradient(135deg, #4f46e5 0%, #3730a3 100%)',
        color: 'white'
      }}>
        <h1 style={{ fontSize: '3rem', margin: 0, fontWeight: 'bold' }}>StockMode</h1>
        <p style={{ fontSize: '1.25rem', margin: 0, opacity: 0.9 }}>Inventory Management System</p>
        <p style={{ fontSize: '1rem', margin: '20px 0', opacity: 0.8, textAlign: 'center', maxWidth: '400px' }}>
          Please authenticate to access your inventory management dashboard
        </p>
        <button 
          onClick={() => auth.signinRedirect()}
          style={{
            padding: '12px 24px',
            fontSize: '16px',
            fontWeight: '600',
            background: 'rgba(255, 255, 255, 0.2)',
            color: 'white',
            border: '2px solid rgba(255, 255, 255, 0.3)',
            borderRadius: '8px',
            cursor: 'pointer',
            transition: 'all 0.2s ease',
            backdropFilter: 'blur(10px)'
          }}
          onMouseOver={(e) => {
            e.target.style.background = 'rgba(255, 255, 255, 0.3)';
            e.target.style.transform = 'translateY(-2px)';
          }}
          onMouseOut={(e) => {
            e.target.style.background = 'rgba(255, 255, 255, 0.2)';
            e.target.style.transform = 'translateY(0)';
          }}
        >
          Access StockMode
        </button>
      </div>
    );
  }

  // If authenticated, show the full app
  if (auth.isAuthenticated) {
    return (
      <>
        <GlobalStyle />
        <AppContainer>
          <Sidebar 
            isMobileOpen={isMobileMenuOpen} 
            onClose={() => setIsMobileMenuOpen(false)} 
          />
          <MainContent>
            <Header onMenuClick={() => setIsMobileMenuOpen(true)} />
              <PageWrapper>
                <AppRoutes />
              </PageWrapper>
          </MainContent>
        </AppContainer>
      </>
    );
  }

  // For login routes, just show the routes without authentication check
  return (
    <>
      <GlobalStyle />
      <AppRoutes />
    </>
  );
}

export default App;
