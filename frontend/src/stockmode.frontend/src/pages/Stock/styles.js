import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: ${props => props.theme.colors.background};
  transition: background-color 0.3s ease;
`;

export const PageHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
`;

export const TitleContainer = styled.div`
  display: flex;
  align-items: center;
  gap: 0.75rem;
`;

export const Title = styled.h2`
  font-size: 1.75rem;
  font-weight: bold;
  color: ${props => props.theme.colors.text};
  margin: 0;
  transition: color 0.3s ease;
`;

export const PrimaryButton = styled.button`
  display: flex;
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
  transition: all 0.3s ease;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;

export const Card = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadow};
  overflow: hidden;
  transition: all 0.3s ease;
`;

export const FilterContainer = styled.div`
  padding: 1.5rem;
  display: flex;
  gap: 1rem;
  background-color: ${props => props.theme.colors.backgroundTertiary};
  border-bottom: 1px solid ${props => props.theme.colors.border};
  transition: all 0.3s ease;
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  color: ${props => props.theme.colors.text};
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
  padding: 0.75rem 1rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  color: ${props => props.theme.colors.text};
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

export const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
`;

export const Th = styled.th`
  padding: 1rem 1.5rem;
  text-align: left;
  font-size: 0.75rem;
  font-weight: 600;
  color: ${props => props.theme.colors.textSecondary};
  text-transform: uppercase;
  letter-spacing: 0.05em;
  border-bottom: 1px solid ${props => props.theme.colors.border};
  transition: color 0.3s ease, border-color 0.3s ease;
`;

export const Td = styled.td`
  padding: 1rem 1.5rem;
  font-size: 0.875rem;
  color: ${props => props.theme.colors.text};
  border-bottom: 1px solid ${props => props.theme.colors.border};
  vertical-align: middle;
  transition: color 0.3s ease, border-color 0.3s ease;
`;

export const Tr = styled.tr`
  &:last-child ${Td} {
    border-bottom: none;
  }
  &:hover {
    background-color: ${props => props.theme.colors.backgroundTertiary};
  }
  transition: background-color 0.3s ease;
`;

export const StockStatusBadge = styled.span`
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  font-weight: 500;
  padding: 0.25rem 0.75rem;
  border-radius: 9999px;
  transition: all 0.3s ease;
  
  ${({ stock, theme }) => {
    if (stock === 0) return `background-color: ${theme.colors.dangerLight}; color: ${theme.colors.danger};`;
    if (stock <= 10) return `background-color: ${theme.colors.warningLight}; color: ${theme.colors.warning};`;
    return `background-color: ${theme.colors.successLight}; color: ${theme.colors.success};`;
  }}
`;

export const ActionButton = styled.button`
  background: none;
  border: none;
  padding: 0.25rem;
  color: ${props => props.theme.colors.textSecondary};
  cursor: pointer;
  border-radius: 999px;
  transition: all 0.3s ease;
  
  &:hover {
    background-color: ${props => props.theme.colors.borderLight};
    color: ${props => props.theme.colors.text};
  }
`;
