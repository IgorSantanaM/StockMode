import React from 'react';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
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

  return (
    <>
      <GlobalStyle />
      {auth.isAuthenticated ? (
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
      ) : (
        <AppRoutes />
      )}
    </>
  );
}

export default App;
