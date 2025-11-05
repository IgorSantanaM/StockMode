import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const PageContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  height: 100%;
  min-height: 100vh;
  padding: 1rem;
  background-color: ${props => props.theme.colors.background};

  @media (min-width: 768px) {
    padding: 2rem;
  }
`;

export const ContentWrapper = styled.div`
  max-width: 500px;
`;

export const IconWrapper = styled.div`
  margin-bottom: 2rem;
  color: ${props => props.theme.colors.primary};
`;

export const Title = styled.h1`
  font-size: 2.5rem;
  font-weight: bold;
  color: ${props => props.theme.colors.text};
  margin: 0;

  @media (min-width: 768px) {
    font-size: 3rem;
  }
`;

export const Subtitle = styled.h2`
  font-size: 1.125rem;
  font-weight: 600;
  color: ${props => props.theme.colors.textSecondary};
  margin-top: 0.5rem;

  @media (min-width: 768px) {
    font-size: 1.25rem;
  }
`;

export const Message = styled.p`
  font-size: 1rem;
  color: ${props => props.theme.colors.textSecondary};
  margin-top: 1rem;
  margin-bottom: 2rem;
`;

export const HomeButton = styled(Link)`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  text-decoration: none;
  transition: all 0.2s ease;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;
