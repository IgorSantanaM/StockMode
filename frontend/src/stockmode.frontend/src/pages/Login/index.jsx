import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import { useNavigate } from 'react-router-dom';
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
  const [isProcessing, setIsProcessing] = useState(false);
  const [error, setError] = useState(null);

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
          setIsProcessing(true);
          await auth.signinRedirectCallback();
          navigate('/');
        }
      } catch (error) {
        console.error("Authentication error:", error);
        setError(error.message || "Authentication failed. Please try again.");
        setIsProcessing(false);
      }
    };

    processCallback();
  }, [auth, navigate]);

  const handleLogin = async () => {
    try {
      setIsProcessing(true);
      setError(null);
      await auth.signinRedirect();
    } catch (error) {
      console.error("Login initiation error:", error);
      setError(error.message || "Failed to start login process. Please try again.");
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