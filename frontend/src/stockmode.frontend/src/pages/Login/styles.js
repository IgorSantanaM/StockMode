import styled from 'styled-components';

export const LoginContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  background: ${props => props.theme.name === 'dark' 
    ? 'linear-gradient(135deg, #1e293b 0%, #0f172a 100%)'
    : 'linear-gradient(135deg, #4f46e5 0%, #3730a3 100%)'};
  padding: 1rem;
  transition: background 0.3s ease;
`;

export const LoginCard = styled.div`
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 1rem;
  box-shadow: 0 20px 25px -5px ${props => props.theme.colors.shadowMedium};
  padding: 3rem;
  width: 100%;
  max-width: 400px;
  text-align: center;
  border: 1px solid ${props => props.theme.colors.border};
  transition: background-color 0.3s ease, box-shadow 0.3s ease;
`;

export const Logo = styled.div`
  margin-bottom: 2rem;
`;

export const LogoText = styled.h1`
  font-size: 2.5rem;
  font-weight: bold;
  color: ${props => props.theme.colors.primary};
  margin: 0;
  margin-bottom: 0.5rem;
`;

export const LogoSubtext = styled.p`
  color: ${props => props.theme.colors.textSecondary};
  font-size: 1rem;
  margin: 0;
`;

export const WelcomeSection = styled.div`
  margin-bottom: 2rem;
`;

export const WelcomeTitle = styled.h2`
  font-size: 1.5rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  margin: 0 0 0.5rem 0;
`;

export const WelcomeText = styled.p`
  color: ${props => props.theme.colors.textSecondary};
  font-size: 0.875rem;
  margin: 0;
`;

export const LoginButton = styled.button`
  width: 100%;
  padding: 0.875rem 1.5rem;
  background: linear-gradient(135deg, ${props => props.theme.colors.primary} 0%, ${props => props.theme.colors.primaryHover} 100%);
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  margin-bottom: 1rem;
  
  &:hover {
    background: linear-gradient(135deg, ${props => props.theme.colors.primaryHover} 0%, ${props => props.theme.colors.primary} 100%);
    transform: translateY(-1px);
    box-shadow: 0 10px 15px -3px ${props => props.theme.colors.shadowMedium};
  }
  
  &:active {
    transform: translateY(0);
  }
  
  &:disabled {
    background: ${props => props.theme.colors.textTertiary};
    cursor: not-allowed;
    transform: none;
    box-shadow: none;
  }
`;

export const LoadingSpinner = styled.div`
  display: inline-block;
  width: 20px;
  height: 20px;
  border: 2px solid #ffffff;
  border-radius: 50%;
  border-top-color: transparent;
  animation: spin 1s linear infinite;
  margin-right: 0.5rem;
  @keyframes spin {
    to {
      transform: rotate(360deg);
    }
  }
`;

export const InfoSection = styled.div`
  border-top: 1px solid #e5e7eb;
  padding-top: 1.5rem;
  margin-top: 1.5rem;
`;

export const InfoText = styled.p`
  color: #6b7280;
  font-size: 0.75rem;
  margin: 0;
  line-height: 1.4;
`;

export const FeatureList = styled.ul`
  list-style: none;
  padding: 0;
  margin: 1rem 0 0 0;
  text-align: left;
`;

export const FeatureItem = styled.li`
  display: flex;
  align-items: center;
  color: #6b7280;
  font-size: 0.875rem;
  margin-bottom: 0.5rem;
  &:last-child {
    margin-bottom: 0;
  }
  &:before {
    content: "✓";
    color: #4f46e5;
    font-weight: bold;
    margin-right: 0.5rem;
  }
`;

export const ErrorMessage = styled.div`
  background-color: #fef2f2;
  border: 1px solid #fecaca;
  color: #dc2626;
  padding: 0.75rem;
  border-radius: 0.5rem;
  margin-bottom: 1rem;
  font-size: 0.875rem;
`;