import styled, { keyframes } from 'styled-components';

const fadeIn = keyframes`
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
`;

const slideUp = keyframes`
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
`;

const spin = keyframes`
  to {
    transform: rotate(360deg);
  }
`;

export const LoginContainer = styled.div`
  min-height: 100vh;
  display: grid;
  grid-template-columns: 1fr 1fr;
  background: ${props => props.theme.colors.background};
  animation: ${fadeIn} 0.5s ease;

  @media (max-width: 968px) {
    grid-template-columns: 1fr;
  }
`;

export const LoginCard = styled.div`
  /* Not used - using split screen layout instead */
`;

export const WelcomeSection = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 3rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  position: relative;
  overflow: hidden;

  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: 
      radial-gradient(circle at 20% 80%, rgba(255, 255, 255, 0.1) 0%, transparent 50%),
      radial-gradient(circle at 80% 20%, rgba(255, 255, 255, 0.08) 0%, transparent 50%);
    pointer-events: none;
  }

  @media (max-width: 968px) {
    padding: 2.5rem 2rem;
    min-height: auto;
  }
`;

export const Logo = styled.div`
  position: relative;
  z-index: 1;
  animation: ${slideUp} 0.6s ease 0.1s both;
`;

export const LogoText = styled.h1`
  font-size: 2.5rem;
  font-weight: 700;
  color: #ffffff;
  margin: 0 0 0.5rem 0;
  letter-spacing: -0.02em;

  @media (max-width: 480px) {
    font-size: 2rem;
  }
`;

export const LogoSubtext = styled.p`
  color: rgba(255, 255, 255, 0.85);
  font-size: 1rem;
  margin: 0;
  font-weight: 400;

  @media (max-width: 480px) {
    font-size: 0.9rem;
  }
`;

export const WelcomeTitle = styled.h2`
  font-size: 2.5rem;
  font-weight: 600;
  color: #ffffff;
  margin: 0 0 1.25rem 0;
  line-height: 1.2;
  position: relative;
  z-index: 1;
  animation: ${slideUp} 0.6s ease 0.2s both;

  @media (max-width: 480px) {
    font-size: 1.75rem;
  }
`;

export const WelcomeText = styled.p`
  color: rgba(255, 255, 255, 0.9);
  font-size: 1.125rem;
  margin: 0;
  line-height: 1.7;
  position: relative;
  z-index: 1;
  animation: ${slideUp} 0.6s ease 0.3s both;

  @media (max-width: 480px) {
    font-size: 1rem;
  }
`;

export const InfoSection = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 3rem;
  background: ${props => props.theme.colors.background};
  gap: 2rem;

  @media (max-width: 968px) {
    padding: 2.5rem 2rem;
  }
`;

export const LoginButton = styled.button`
  width: 100%;
  padding: 1rem 2rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  box-shadow: 0 4px 12px rgba(102, 126, 234, 0.3);
  animation: ${slideUp} 0.6s ease 0.4s both;
  
  &:hover:not(:disabled) {
    transform: translateY(-2px);
    box-shadow: 0 6px 20px rgba(102, 126, 234, 0.4);
  }
  
  &:active:not(:disabled) {
    transform: translateY(0);
  }
  
  &:disabled {
    background: ${props => props.theme.colors.textTertiary};
    cursor: not-allowed;
    opacity: 0.7;
  }
`;

export const LoadingSpinner = styled.div`
  display: inline-block;
  width: 18px;
  height: 18px;
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-radius: 50%;
  border-top-color: white;
  animation: ${spin} 0.8s linear infinite;
`;

export const InfoText = styled.p`
  color: ${props => props.theme.colors.textSecondary};
  font-size: 0.875rem;
  margin: 1.5rem 0 0 0;
  line-height: 1.5;
  text-align: center;
  animation: ${slideUp} 0.6s ease 0.5s both;
`;

export const FeatureList = styled.ul`
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
`;

export const FeatureItem = styled.li`
  display: flex;
  align-items: center;
  color: ${props => props.theme.colors.text};
  font-size: 0.9rem;
  padding: 0;
  animation: ${slideUp} 0.6s ease calc(0.5s + ${props => props.index || 0} * 0.1s) both;

  &::before {
    content: "✓";
    color: #667eea;
    font-weight: bold;
    font-size: 1rem;
    margin-right: 0.75rem;
    display: flex;
    align-items: center;
    justify-content: center;
    min-width: 20px;
    height: 20px;
    background: rgba(102, 126, 234, 0.1);
    border-radius: 50%;
  }
`;

export const ErrorMessage = styled.div`
  background: ${props => props.theme.name === 'dark' ? 'rgba(239, 68, 68, 0.1)' : '#fef2f2'};
  border: 1px solid ${props => props.theme.name === 'dark' ? 'rgba(239, 68, 68, 0.3)' : '#fecaca'};
  color: ${props => props.theme.name === 'dark' ? '#fca5a5' : '#dc2626'};
  padding: 0.875rem 1rem;
  border-radius: 8px;
  margin-bottom: 1rem;
  font-size: 0.875rem;
  line-height: 1.5;
  animation: ${slideUp} 0.3s ease;
`;