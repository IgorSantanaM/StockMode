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

  useEffect(() => {
    const urlParams = new URLSearchParams(window.location.search);
    if (urlParams.has('code')) {
      (async () => {
        try {
          setIsProcessing(true);
          await auth.signinRedirectCallback();
          setIsProcessing(false);
          const returnPath = auth.user?.state?.returnPath || location.state?.from || '/';
          navigate(returnPath, { replace: true });
        } catch (e) {
          setError(e.message || 'Authentication failed');
          setIsProcessing(false);
        }
      })();
    }
  }, [auth, navigate, location.state]);

  const handleLogin = async () => {
    try {
      setIsProcessing(true);
      setError(null);
      const from = location.state?.from || '/';
      await auth.signinRedirect({ state: { returnPath: from } });
    } catch (e) {
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
            <WelcomeTitle>Processing Login...</WelcomeTitle>
            <WelcomeText>Please wait while we authenticate you</WelcomeText>
          </WelcomeSection>

          <LoginButton disabled>
            <LoadingSpinner />
            Authenticating...
          </LoginButton>
        </LoginCard>
      </LoginContainer>
    );
  }

    if (auth.isAuthenticated) {
      // Already logged in; maybe user navigated to /login manually
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