import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';
import { LoadingContainer } from '../util/LoadingContainer';

export function ProtectedRoute({ element }) {
  const auth = useAuth();
  const location = useLocation();

  if (auth.isLoading) {
    return (
      <LoadingContainer>
        <div style={{ textAlign: 'center', padding: '40px' }}>
          <h2>Loading...</h2>
          <p>Checking authentication status...</p>
        </div>
      </LoadingContainer>
    );
  }

  if (!auth.isAuthenticated) {
    console.log('ProtectedRoute: User not authenticated, redirecting to login');
    return <Navigate to="/login" replace state={{ from: location.pathname + location.search }} />;
  }

  console.log('ProtectedRoute: User authenticated, rendering protected content');
  return element;
}

export default ProtectedRoute;