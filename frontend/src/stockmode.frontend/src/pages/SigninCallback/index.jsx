import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import { useNavigate } from 'react-router-dom';
import { LoadingContainer } from '../../util/LoadingContainer';

export default function SigninCallback() {
  const auth = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState(null);

  useEffect(() => {
    console.log('SigninCallback mounted');
    console.log('Auth state:', {
      isLoading: auth.isLoading,
      isAuthenticated: auth.isAuthenticated,
      error: auth.error,
      user: auth.user
    });

    // If already authenticated, redirect immediately
    if (!auth.isLoading && auth.isAuthenticated) {
      console.log('SigninCallback: Already authenticated, redirecting to home');
      const returnPath = auth.user?.state?.returnPath || '/home';
      navigate(returnPath, { replace: true });
      return;
    }

    // If there's an error, show it
    if (auth.error) {
      console.error('SigninCallback: Authentication error', auth.error);
      setError(auth.error.message);
      setTimeout(() => {
        navigate('/login', { replace: true });
      }, 3000);
    }
  }, [auth.isLoading, auth.isAuthenticated, auth.error, auth.user, navigate]);

  if (error) {
    return (
      <LoadingContainer>
        <div style={{ textAlign: 'center', padding: '40px', color: 'red' }}>
          <h2>Authentication Error</h2>
          <p>{error}</p>
          <p>Redirecting to login...</p>
        </div>
      </LoadingContainer>
    );
  }

  return (
    <LoadingContainer>
      <div style={{ textAlign: 'center', padding: '40px' }}>
        <h2>Completing sign in...</h2>
        <p>Please wait while we authenticate you.</p>
        <p style={{ fontSize: '12px', color: '#666', marginTop: '20px' }}>
          Processing authorization code...
        </p>
      </div>
    </LoadingContainer>
  );
}
