import React, {useEffect } from 'react';
import { useAuth } from 'react-oidc-context';
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const auth = useAuth();
  const navigate = useNavigate();

  console.log("Auth object:", auth); 

  useEffect(() => {
    const processCallback = async () => {
      try {
        if (auth.isLoading) return;

        if (auth.user) {
          console.log("User already authenticated:", auth.user);
          navigate('/');
          return;
        }

        // If there's a code in the URL, process the callback
        const urlParams = new URLSearchParams(window.location.search);
        if (urlParams.has('code')) {
          await auth.signinRedirectCallback();
          navigate('/');
        } else {
          // Start the authentication process
          await auth.signinRedirect();
        }
      } catch (error) {
        console.error("Authentication error:", error);
      }
    };

    processCallback();
  }, [auth, navigate]);

  if (auth.isLoading) {
    return <div>Loading authentication...</div>;

    return <div>Processing authentication...</div>;
  }
}