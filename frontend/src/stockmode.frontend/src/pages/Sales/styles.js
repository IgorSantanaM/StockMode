import styled from 'styled-components';

export const PageContainer = styled.div`
  min-height: 100vh;
  background-color: #f8fafc;
  padding: 20px;
`;

export const Container = styled.div`
  max-width: 1400px;
  margin: 0 auto;
`;

export const Header = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 32px;
  background: white;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);

  div:first-child {
    display: flex;
    align-items: center;
    gap: 16px;
  }
`;

export const Title = styled.h1`
  color: #1f2937;
  font-size: 2rem;
  font-weight: 700;
  margin: 0;
`;

export const HeaderActions = styled.div`
  display: flex;
  gap: 12px;
`;

export const FiltersSection = styled.div`
  display: flex;
  gap: 16px;
  align-items: end;
  margin-bottom: 24px;
  background: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  flex-wrap: wrap;
  
  @media (max-width: 768px) {
    flex-direction: column;
    align-items: stretch;
  }
`;

export const FilterGroup = styled.div`
  display: flex;
  flex-direction: column;
  gap: 8px;
  min-width: 150px;

  label {
    font-size: 0.875rem;
    font-weight: 500;
    color: #374151;
  }
`;

export const Input = styled.input`
  padding: 10px 12px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 0.875rem;
  transition: border-color 0.2s;

  &:focus {
    outline: none;
    border-color: #3b82f6;
    box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
  }
`;

export const Select = styled.select`
  padding: 10px 12px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 0.875rem;
  background-color: white;
  transition: border-color 0.2s;

  &:focus {
    outline: none;
    border-color: #3b82f6;
    box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
  }
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 16px;
  background-color: #3b82f6;
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s;
  white-space: nowrap;
  height: fit-content;

  &:hover {
    background-color: #2563eb;
  }

  &:disabled {
    background-color: #9ca3af;
    cursor: not-allowed;
  }
`;

export const SalesGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 20px;
  margin-bottom: 32px;

  @media (max-width: 768px) {
    grid-template-columns: 1fr;
  }
`;

export const SaleCard = styled.div`
  background: white;
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.2s;

  &:hover {
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  }
`;

export const SaleInfo = styled.div`
  margin-bottom: 16px;

  .sale-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px;

    h3 {
      margin: 0;
      color: #1f2937;
      font-size: 1.25rem;
      font-weight: 600;
    }
  }

  .sale-details {
    display: flex;
    flex-direction: column;
    gap: 8px;

    p {
      margin: 0;
      color: #6b7280;
      font-size: 0.875rem;

      strong {
        color: #374151;
      }
    }
  }
`;

export const SaleActions = styled.div`
  display: flex;
  gap: 8px;
  justify-content: flex-end;
`;

export const StatusBadge = styled.span`
  padding: 4px 12px;
  border-radius: 16px;
  font-size: 0.75rem;
  font-weight: 500;
  text-transform: uppercase;
  letter-spacing: 0.05em;
  background-color: ${props => {
    switch (props.status) {
      case 0: return '#fef3c7'; // Pending
      case 1: return '#d1fae5'; // Completed
      case 2: return '#fecaca'; // Cancelled
      default: return '#f3f4f6';
    }
  }};
  color: ${props => {
    switch (props.status) {
      case 0: return '#d97706'; // Pending
      case 1: return '#059669'; // Completed
      case 2: return '#dc2626'; // Cancelled
      default: return '#6b7280';
    }
  }};
`;

export const Pagination = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 16px;
  padding: 20px;
  background: white;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);

  span {
    color: #374151;
    font-weight: 500;
  }
`;

export const PaginationButton = styled.button`
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 8px 16px;
  background-color: ${props => props.disabled ? '#f9fafb' : '#3b82f6'};
  color: ${props => props.disabled ? '#9ca3af' : 'white'};
  border: 1px solid ${props => props.disabled ? '#e5e7eb' : '#3b82f6'};
  border-radius: 8px;
  font-weight: 500;
  cursor: ${props => props.disabled ? 'not-allowed' : 'pointer'};
  transition: all 0.2s;

  &:hover:not(:disabled) {
    background-color: #2563eb;
  }
`;

export const EmptyState = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 60px 20px;
  background: white;
  border-radius: 12px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  text-align: center;

  h3 {
    margin: 16px 0 8px;
    color: #374151;
    font-size: 1.5rem;
  }

  p {
    color: #6b7280;
    margin-bottom: 24px;
    max-width: 400px;
  }
`;