import styled from 'styled-components';

export const PageContainer = styled.div`
  min-height: 100vh;
  background-color: ${props => props.theme.colors.background};
  padding: 20px;
`;

export const Container = styled.div`
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 24px;
`;

export const Header = styled.div`
  display: flex;
  align-items: center;
  gap: 16px;
  background: ${props => props.theme.colors.backgroundSecondary};
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
`;

export const Title = styled.h1`
  color: ${props => props.theme.colors.text};
  font-size: 2rem;
  font-weight: 700;
  margin: 0;
`;

export const StatusBadge = styled.span`
  padding: 4px 12px;
  border-radius: 16px;
  font-size: 0.875rem;
  font-weight: 500;
  background-color: ${props => {
    switch (props.status) {
      case 1: return props.theme.colors.warningLight; 
      case 2: return props.theme.colors.successLight;
      case 3: return props.theme.colors.dangerLight;
      default: return props.theme.colors.backgroundTertiary;
    }
  }};
  color: ${props => {
    switch (props.status) {
      case 1: return props.theme.colors.warning; 
      case 2: return props.theme.colors.success; 
      case 3: return props.theme.colors.danger; 
      default: return props.theme.colors.textSecondary;
    }
  }};
`;

export const Section = styled.div`
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  overflow: hidden;
`;

export const SectionTitle = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px 24px;
  border-bottom: 1px solid ${props => props.theme.colors.border};
  font-size: 1.25rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  gap: 12px;
`;

export const Card = styled.div`
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 12px;
  padding: 24px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
`;

export const Form = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

export const FormGroup = styled.div`
  display: flex;
  flex-direction: column;
  gap: 8px;
`;

export const Label = styled.label`
  font-weight: 500;
  color: ${props => props.theme.colors.textSecondary};
  font-size: 0.875rem;
`;

export const Input = styled.input`
  padding: 12px;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 8px;
  font-size: 1rem;
  color: ${props => props.theme.colors.text};
  background-color: ${props => props.theme.colors.backgroundTertiary};
  transition: all 0.2s;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }

  &:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const Select = styled.select`
  padding: 12px;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 8px;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  transition: all 0.2s;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  padding: 12px 24px;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  white-space: nowrap;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }

  &:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const SecondaryButton = styled(Button)`
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  
  &:hover {
    background-color: ${props => props.theme.colors.border};
  }
`;

export const DangerButton = styled(Button)`
  background-color: ${props => props.theme.colors.danger};
  
  &:hover {
    opacity: 0.9;
  }
`;

export const ItemsList = styled.div`
  padding: 0 24px 24px;
`;

export const ItemCard = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 8px;
  margin-bottom: 12px;
  
  &:last-child {
    margin-bottom: 0;
  }
`;

export const ItemInfo = styled.div`
  h4 {
    margin: 0 0 8px 0;
    color: ${props => props.theme.colors.text};
    font-size: 1.125rem;
  }
  
  p {
    margin: 4px 0;
    color: ${props => props.theme.colors.textSecondary};
    font-size: 0.875rem;
  }
`;

export const ItemActions = styled.div`
  display: flex;
  gap: 8px;
`;

export const PriceInfo = styled.div`
  font-size: 1.25rem;
  font-weight: 600;
  color: ${props => props.theme.colors.success};
`;

export const TotalSection = styled.div`
  background: ${props => props.theme.colors.backgroundSecondary};
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  text-align: right;
  
  div {
    display: flex;
    flex-direction: column;
    gap: 8px;
    align-items: flex-end;
  }
  
  strong {
    font-size: 1.25rem;
    color: ${props => props.theme.colors.text};
  }
  
  p {
    color: ${props => props.theme.colors.textSecondary};
    margin: 0;
  }
  
  h3 {
    font-size: 1.875rem;
    color: ${props => props.theme.colors.success};
    margin: 0;
  }
`;

export const ActionButtons = styled.div`
  display: flex;
  gap: 12px;
  justify-content: flex-end;
  flex-wrap: wrap;
  
  @media (max-width: 768px) {
    justify-content: stretch;
    
    button {
      flex: 1;
    }
  }
`;

export const Alert = styled.div`
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 16px;
  border-radius: 8px;
  font-weight: 500;
  
  background-color: ${props => props.type === 'success' 
    ? props.theme.colors.successLight 
    : props.theme.colors.dangerLight};
  color: ${props => props.type === 'success' 
    ? props.theme.name === 'dark' ? '#86efac' : props.theme.colors.success
    : props.theme.name === 'dark' ? '#fca5a5' : props.theme.colors.danger};
  border: 1px solid ${props => props.type === 'success' 
    ? props.theme.colors.success 
    : props.theme.colors.danger};
`;