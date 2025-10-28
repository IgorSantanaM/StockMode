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
        <LoginCard>
          <Logo>
            <LogoText>StockMode</LogoText>
            <LogoSubtext>Inventory Management System</LogoSubtext>
          </Logo>

          <WelcomeSection>
            <WelcomeTitle>Starting Login...</WelcomeTitle>
            <WelcomeText>Please wait</WelcomeText>
          </WelcomeSection>

          <LoginButton disabled>
            <LoadingSpinner />
            Redirecting...
          </LoginButton>
        </LoginCard>
      </LoginContainer>
    );
  }

  if (auth.isAuthenticated) {
    // Already logged in; redirect to home
    return null;
  }

  return (
    <LoginContainer>
      <LoginCard>
        <Logo>
          <LogoText>StockMode</LogoText>
          <LogoSubtext>Inventory Management System</LogoSubtext>
        </Logo>

        <WelcomeSection>
          <WelcomeTitle>Welcome to StockMode</WelcomeTitle>
          <WelcomeText>Manage your inventory with ease and efficiency</WelcomeText>
        </WelcomeSection>

        {error && (
          <ErrorMessage>
            {error}
          </ErrorMessage>
        )}

        <LoginButton onClick={handleLogin}>
          Access StockMode
        </LoginButton>

        <InfoSection>
          <InfoText>
            No registration required - just click to get started!
          </InfoText>

          <FeatureList>
            <FeatureItem>Real-time inventory tracking</FeatureItem>
            <FeatureItem>Financial management</FeatureItem>
            <FeatureItem>Sales and customer management</FeatureItem>
            <FeatureItem>Supplier relationship management</FeatureItem>
          </FeatureList>
        </InfoSection>
      </LoginCard>
    </LoginContainer>
  );
}