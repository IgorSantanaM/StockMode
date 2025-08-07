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
  /* A more robust responsive grid that wraps items automatically */
  grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
  gap: 1.5rem;
`;

export const MainLayoutGrid = styled.div`
  display: flex;
  flex-direction: column; /* Stacks chart and actions on mobile */
  gap: 1.5rem;

  /* On large desktops, show chart and actions side-by-side */
  @media (min-width: 1280px) {
    flex-direction: row;
    align-items: flex-start;
  }
`;

export const Card = styled.div`
  background-color: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1);
  display: flex;
  flex-direction: column;
  height: 100%; /* Makes cards in a row have equal height */
`;

export const ChartCard = styled(Card)`
  /* On large desktops, the chart takes up 2/3 of the space */
  @media (min-width: 1280px) {
    flex: 2;
    min-width: 0; /* Prevents flex items from overflowing their container */
  }
`;

export const ActionsAndSalesContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1.5rem;

  /* On tablets, show action cards side-by-side */
  @media (min-width: 768px) and (max-width: 1279px) {
     flex-direction: row;
  }

  /* On large desktops, this container takes up 1/3 of the space */
  @media (min-width: 1280px) {
    flex: 1;
    min-width: 0;
  }
`;

export const ActionCard = styled(Card)`
    /* On tablets, each card takes half the space */
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
  background-color: #4f46e5;
  color: white;
  border-radius: 0.5rem;
  border: none;
  cursor: pointer;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  transition: all 0.2s ease-in-out;

  &:hover {
    background-color: #4338ca;
  }
`;

export const SecondaryButton = styled(PrimaryButton)`
  background-color: #e5e7eb;
  color: #1f2937;
  box-shadow: none;

  &:hover {
    background-color: #d1d5db;
  }
`;

export const StatusBadge = styled.span`
  font-size: 0.75rem;
  font-weight: 600;
  padding: 0.25rem 0.6rem;
  border-radius: 9999px;
  background-color: ${props => props.status === 'Concluída' ? '#d1fae5' : '#fef3c7'};
  color: ${props => props.status === 'Concluída' ? '#065f46' : '#92400e'};
`;

// Re-styled StatCard for a cleaner look
export const StatCardContainer = styled(Card)`
    padding: 1rem;
    gap: 0.5rem;
`;

export const StatCardHeader = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    color: #6b7280;
    font-weight: 500;
    font-size: 0.875rem;
`;

export const StatCardValue = styled.p`
    font-size: 1.875rem;
    font-weight: bold;
    color: #111827;
    margin: 0;
`;

export const StatCardFooter = styled.div`
    display: flex;
    align-items: center;
    font-size: 0.875rem;
    color: ${props => props.isPositive ? '#10b981' : '#ef4444'};
`;

export const ChangeIndicator = styled.span`
    display: flex;
    align-items: center;
    margin-right: 0.25rem;
`;