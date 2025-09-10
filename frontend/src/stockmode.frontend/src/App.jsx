import React from 'react';
import { useState } from 'react';
import { BrowserRouter } from 'react-router-dom';
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

  if (auth.error) {
    return <div>Erro na autenticação: {auth.error.message}</div>;
  }

  if (!auth.isAuthenticated) {
    return (
      <div style={{ 
        display: 'flex', 
        flexDirection: 'column', 
        alignItems: 'center', 
        justifyContent: 'center', 
        height: '100vh',
        gap: '20px'
      }}>
        <h1>StockMode</h1>
        <p>Por favor, faça login para acessar o sistema.</p>
        <button 
          onClick={() => auth.signinRedirect()}
          style={{
            padding: '10px 20px',
            fontSize: '16px',
            backgroundColor: '#007bff',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor: 'pointer'
          }}
        >
          Fazer Login
        </button>
      </div>
    );
  }

  return (
      <BrowserRouter>
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
      </BrowserRouter>
  );
}

export default App;
