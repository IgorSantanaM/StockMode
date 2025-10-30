import styled from 'styled-components';

export const DashboardGrid = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
`;

export const StatsGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
  gap: 1.5rem;
`;

export const MainLayoutGrid = styled.div`
  display: flex;
  flex-direction: column; 
  gap: 1.5rem;

  @media (min-width: 1280px) {
    flex-direction: row;
    align-items: flex-start;
  }
`;

export const Card = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 3px 0 ${props => props.theme.colors.shadow};
  display: flex;
  flex-direction: column;
  height: 100%;
  transition: background-color 0.3s ease, box-shadow 0.3s ease;
  border: 1px solid ${props => props.theme.colors.border};
`;

export const ChartCard = styled(Card)`
  @media (min-width: 1280px) {
    flex: 2;
    min-width: 0;
  }
`;

export const ActionsAndSalesContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1.5rem;

  @media (min-width: 768px) and (max-width: 1279px) {
     flex-direction: row;
  }

  @media (min-width: 1280px) {
    flex: 1;
    min-width: 0;
  }
`;

export const ActionCard = styled(Card)`
    @media (min-width: 768px) and (max-width: 1279px) {
        flex: 1;
    }
`;

export const SalesCard = styled(Card)`
    flex-grow: 1;
    @media (min-width: 768px) and (max-width: 1279px) {
        flex: 1;
    }
`;

export const PrimaryButton = styled.button`
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 0.9rem;
  font-weight: 600;
  padding: 0.75rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border-radius: 0.5rem;
  border: none;
  cursor: pointer;
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadowMedium};
  transition: all 0.2s ease-in-out;
  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;

export const SecondaryButton = styled(PrimaryButton)`
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  box-shadow: none;

  &:hover {
    background-color: ${props => props.theme.colors.border};
  }
`;

export const StatusBadge = styled.span`
  font-size: 0.75rem;
  font-weight: 600;
  padding: 0.25rem 0.6rem;
  border-radius: 9999px;
  background-color: ${props => props.status === 'Concluída' ? props.theme.colors.successLight : props.theme.colors.warningLight};
  color: ${props => props.status === 'Concluída' ? props.theme.colors.success : props.theme.colors.warning};
  transition: background-color 0.3s ease, color 0.3s ease;
`;

export const StatCardContainer = styled(Card)`
    padding: 1rem;
    gap: 0.5rem;
`;

export const StatCardHeader = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    color: ${props => props.theme.colors.textSecondary};
    font-weight: 500;
    font-size: 0.875rem;
`;

export const StatCardValue = styled.p`
    font-size: 1.875rem;
    font-weight: bold;
    color: ${props => props.theme.colors.text};
    margin: 0;
`;

export const StatCardFooter = styled.div`
    display: flex;
    align-items: center;
    font-size: 0.875rem;
    color: ${props => props.isPositive ? props.theme.colors.success : props.theme.colors.danger};
`;

export const ChangeIndicator = styled.span`
    display: flex;
    align-items: center;
    margin-right: 0.25rem;
`;