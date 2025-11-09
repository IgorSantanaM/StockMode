import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import { useNavigate, useLocation } from 'react-router-dom';
import {
  LoginContainer,
  LoginCard,
  Logo,
  LogoText,
  LogoSubtext,
  WelcomeSection,
  WelcomeTitle,
  WelcomeText,
  LoginButton,
  LoadingSpinner,
  InfoSection,
  InfoText,
  FeatureList,
  FeatureItem,
  ErrorMessage
} from './styles';

export default function Login() {
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const [isProcessing, setIsProcessing] = useState(false);
  const [error, setError] = useState(null);

  // Redirect if already authenticated
  useEffect(() => {
    if (auth.isAuthenticated) {
      const returnPath = location.state?.from || '/home';
      navigate(returnPath, { replace: true });
    }
  }, [auth.isAuthenticated, navigate, location.state]);

  const handleLogin = async () => {
    try {
      setIsProcessing(true);
      setError(null);
      const from = location.state?.from || '/home';
      console.log('Login: Initiating signin redirect, return path:', from);
      await auth.signinRedirect({ state: { returnPath: from } });
    } catch (e) {
      console.error('Login: Error starting signin:', e);
      setError(e.message || 'Failed to start login process');
      setIsProcessing(false);
    }
  };

  if (auth.isLoading || isProcessing) {
    return (
      <LoginContainer>
        <WelcomeSection>
          <Logo>
            <LogoText>StockMode</LogoText>
            <LogoSubtext>Inventory Management System</LogoSubtext>
          </Logo>
          <div>
            <WelcomeTitle>Starting Login...</WelcomeTitle>
            <WelcomeText>Please wait while we redirect you</WelcomeText>
          </div>
        </WelcomeSection>

        <InfoSection>
          <LoginButton disabled>
            <LoadingSpinner />
            Redirecting...
          </LoginButton>
        </InfoSection>
      </LoginContainer>
    );
  }

  if (auth.isAuthenticated) {
    return null;
  }

  return (
    <LoginContainer>
      <WelcomeSection>
        <Logo>
          <LogoText>StockMode</LogoText>
          <LogoSubtext>Inventory Management System</LogoSubtext>
        </Logo>
        
        <div>
          <WelcomeTitle>Welcome to StockMode</WelcomeTitle>
          <WelcomeText>
            Your complete inventory management solution. 
            Track stock, manage sales, and grow your business with ease.
          </WelcomeText>
        </div>
      </WelcomeSection>

      <InfoSection>
        {error && <ErrorMessage>{error}</ErrorMessage>}

        <LoginButton onClick={handleLogin}>
          Access StockMode
        </LoginButton>

        <FeatureList>
          <FeatureItem index={0}>Real-time inventory tracking</FeatureItem>
          <FeatureItem index={1}>Financial management</FeatureItem>
          <FeatureItem index={2}>Sales and customer management</FeatureItem>
          <FeatureItem index={3}>Supplier relationship management</FeatureItem>
        </FeatureList>

        <InfoText>
          No registration required - just click to get started!
        </InfoText>
      </InfoSection>
    </LoginContainer>
  );
}