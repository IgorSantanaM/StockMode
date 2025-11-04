import styled from 'styled-components';

export const PageContainer = styled.div`
  min-height: 100vh;
  background-color: ${props => props.theme.colors.background};
  padding: 20px;
  transition: background-color 0.3s ease;
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
  background: ${props => props.theme.colors.backgroundSecondary};
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  transition: all 0.3s ease;

  div:first-child {
    display: flex;
    align-items: center;
    gap: 16px;
  }
`;

export const Title = styled.h1`
  color: ${props => props.theme.colors.text};
  font-size: 2rem;
  font-weight: 700;
  margin: 0;
  transition: color 0.3s ease;
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
  background: ${props => props.theme.colors.backgroundSecondary};
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  flex-wrap: wrap;
  transition: all 0.3s ease;
  
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
    color: ${props => props.theme.colors.text};
    transition: color 0.3s ease;
  }
`;

export const Input = styled.input`
  padding: 10px 12px;
  border: 1px solid ${props => props.theme.colors.border};
  background-color: ${props => props.theme.colors.backgroundSecondary};
  color: ${props => props.theme.colors.text};
  border-radius: 8px;
  font-size: 0.875rem;
  transition: all 0.3s ease;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }

  &::placeholder {
    color: ${props => props.theme.colors.textTertiary};
  }
`;

export const Select = styled.select`
  padding: 10px 12px;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 8px;
  font-size: 0.875rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  color: ${props => props.theme.colors.text};
  transition: all 0.3s ease;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 16px;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 8px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
  white-space: nowrap;
  height: fit-content;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }

  &:disabled {
    background-color: ${props => props.theme.colors.textTertiary};
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
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 12px;
  padding: 20px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  transition: all 0.3s ease;

  &:hover {
    box-shadow: 0 4px 12px ${props => props.theme.colors.shadowMedium};
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
      color: ${props => props.theme.colors.text};
      font-size: 1.25rem;
      font-weight: 600;
      transition: color 0.3s ease;
    }
  }

  .sale-details {
    display: flex;
    flex-direction: column;
    gap: 8px;

    p {
      margin: 0;
      color: ${props => props.theme.colors.textSecondary};
      font-size: 0.875rem;
      transition: color 0.3s ease;

      strong {
        color: ${props => props.theme.colors.text};
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
  transition: all 0.3s ease;
  background-color: ${props => {
    switch (props.status) {
      case 0: return props.theme.colors.warningLight;
      case 1: return props.theme.colors.successLight;
      case 2: return props.theme.colors.dangerLight;
      default: return props.theme.colors.borderLight;
    }
  }};
  color: ${props => {
    switch (props.status) {
      case 0: return props.theme.colors.warning;
      case 1: return props.theme.colors.success;
      case 2: return props.theme.colors.danger;
      default: return props.theme.colors.textSecondary;
    }
  }};
`;

export const Pagination = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 16px;
  padding: 20px;
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  transition: all 0.3s ease;

  span {
    color: ${props => props.theme.colors.text};
    font-weight: 500;
    transition: color 0.3s ease;
  }
`;

export const PaginationButton = styled.button`
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 8px 16px;
  background-color: ${props => props.disabled ? props.theme.colors.backgroundTertiary : props.theme.colors.primary};
  color: ${props => props.disabled ? props.theme.colors.textTertiary : 'white'};
  border: 1px solid ${props => props.disabled ? props.theme.colors.border : props.theme.colors.primary};
  border-radius: 8px;
  font-weight: 500;
  cursor: ${props => props.disabled ? 'not-allowed' : 'pointer'};
  transition: all 0.3s ease;

  &:hover:not(:disabled) {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;

export const EmptyState = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 60px 20px;
  background: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 12px;
  box-shadow: 0 1px 3px ${props => props.theme.colors.shadow};
  text-align: center;
  transition: all 0.3s ease;

  h3 {
    margin: 16px 0 8px;
    color: ${props => props.theme.colors.text};
    font-size: 1.5rem;
    transition: color 0.3s ease;
  }

  p {
    color: ${props => props.theme.colors.textSecondary};
    margin-bottom: 24px;
    max-width: 400px;
    transition: color 0.3s ease;
  }
`;