import styled from 'styled-components';

export const PageContainer = styled.div`
  min-height: 100vh;
  background-color: #f8fafc;
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
  background: white;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
`;

export const Title = styled.h1`
  color: #1f2937;
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
      case 1: return '#fef3c7'; 
      case 2: return '#d1fae5';
      case 3: return '#fecaca';
      default: return '#f3f4f6';
    }
  }};
  color: ${props => {
    switch (props.status) {
      case 1: return '#d97706'; 
      case 2: return '#059669'; 
      case 3: return '#dc2626'; 
      default: return '#6b7280';
    }
  }};
`;

export const Section = styled.div`
  background: white;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  overflow: hidden;
`;

export const SectionTitle = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px 24px;
  border-bottom: 1px solid #e5e7eb;
  font-size: 1.25rem;
  font-weight: 600;
  color: #1f2937;
  gap: 12px;
`;

export const Card = styled.div`
  background: white;
  border-radius: 12px;
  padding: 24px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
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
  color: #374151;
  font-size: 0.875rem;
`;

export const Input = styled.input`
  padding: 12px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 1rem;
  color: black;
  transition: all 0.2s;

  &:focus {
    outline: none;
    border-color: #3b82f6;
    box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
  }

  &:disabled {
    background-color: #f9fafb;
    cursor: not-allowed;
  }
`;

export const Select = styled.select`
  padding: 12px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 1rem;
  background-color: white;
  transition: all 0.2s;
  color: black;

  &:focus {
    outline: none;
    border-color: #3b82f6;
    box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
  }
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  padding: 12px 24px;
  background-color: #3b82f6;
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  white-space: nowrap;

  &:hover {
    background-color: #2563eb;
  }

  &:disabled {
    background-color: #9ca3af;
    cursor: not-allowed;
  }
`;

export const SecondaryButton = styled(Button)`
  background-color: #6b7280;
  
  &:hover {
    background-color: #4b5563;
  }
`;

export const DangerButton = styled(Button)`
  background-color: #dc2626;
  
  &:hover {
    background-color: #b91c1c;
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
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  margin-bottom: 12px;
  
  &:last-child {
    margin-bottom: 0;
  }
`;

export const ItemInfo = styled.div`
  h4 {
    margin: 0 0 8px 0;
    color: #1f2937;
    font-size: 1.125rem;
  }
  
  p {
    margin: 4px 0;
    color: #6b7280;
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
  color: #059669;
`;

export const TotalSection = styled.div`
  background: white;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  text-align: right;
  
  div {
    display: flex;
    flex-direction: column;
    gap: 8px;
    align-items: flex-end;
  }
  
  strong {
    font-size: 1.25rem;
    color: #374151;
  }
  
  p {
    color: #6b7280;
    margin: 0;
  }
  
  h3 {
    font-size: 1.875rem;
    color: #059669;
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
  
  background-color: ${props => props.type === 'success' ? '#d1fae5' : '#fecaca'};
  color: ${props => props.type === 'success' ? '#059669' : '#dc2626'};
  border: 1px solid ${props => props.type === 'success' ? '#10b981' : '#f87171'};
`;