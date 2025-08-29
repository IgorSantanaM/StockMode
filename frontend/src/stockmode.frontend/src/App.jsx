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

  if (auth.isLoading) {
    return <LoadingContainer>Carregando autenticação...</LoadingContainer>;
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
