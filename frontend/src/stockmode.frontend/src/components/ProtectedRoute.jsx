import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';

export function ProtectedRoute({ element }) {
  const auth = useAuth();
  const location = useLocation();

  if (auth.isLoading) return null; 
  if (!auth.isAuthenticated) {
    return <Navigate to="/login" replace state={{ from: location.pathname + location.search }} />;
  }
  return element;
}

export default ProtectedRoute;