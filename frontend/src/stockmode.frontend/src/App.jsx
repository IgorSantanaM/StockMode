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

  // Debug logging
  React.useEffect(() => {
    console.log('Auth state:', {
      isLoading: auth.isLoading,
      isAuthenticated: auth.isAuthenticated,
      activeNavigator: auth.activeNavigator,
      user: auth.user ? 'present' : 'null',
      error: auth.error?.message
    });
  }, [auth.isLoading, auth.isAuthenticated, auth.activeNavigator, auth.user, auth.error]);

  // Don't show error on callback page
  if (auth.error && location.pathname !== '/signin-oidc') {
    return (
      <div style={{ padding: '40px', textAlign: 'center', color: 'red' }}>
        <h2>Authentication Error</h2>
        <p>{auth.error.message}</p>
      </div>
    );
  }

  // Show loading during authentication check (except on callback page which handles its own loading)
  if (auth.isLoading && location.pathname !== '/signin-oidc') {
    return (
      <LoadingContainer>
        <div style={{ textAlign: 'center', padding: '40px' }}>
          <h2>Loading...</h2>
          <p>Checking authentication...</p>
        </div>
      </LoadingContainer>
    );
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
