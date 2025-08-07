import styled from 'styled-components';

export const DashboardGrid = styled.div`
  flex-grow: 1;
  width: 100%;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  gap: 2rem;
  box-sizing: border-box;

  @media (min-width: 768px) {
    padding: 2rem;
  }
`;


export const StatsGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(1, 1fr);
  gap: 1rem;
  margin-bottom: 2rem;

  @media (min-width: 640px) {
    grid-template-columns: repeat(2, 1fr);
  }
  @media (min-width: 1280px) {
    grid-template-columns: repeat(4, 1fr);
    gap: 1.5rem;
  }
`;

export const MainLayoutGrid = styled.div`
  display: grid;
  grid-template-columns: 1fr; // Uma coluna por defeito para telemóveis
  gap: 2rem;

  @media (min-width: 1280px) {
    grid-template-columns: repeat(3, 1fr); // Layout de 3 colunas para desktops
  }
`;

export const Card = styled.div`
  background-color: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 2px 0 rgb(0 0 0 / 0.05);
`;

export const ChartCard = styled(Card)`
  @media (min-width: 1280px) {
    grid-column: span 2 / span 2; // Ocupa 2 colunas no layout de desktop
  }
`;

export const ActionsAndSalesContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

export const PrimaryButton = styled.button`
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1rem;
  font-weight: 600;
  padding: 0.75rem 0;
  background-color: #4f46e5;
  color: white;
  border-radius: 0.75rem;
  border: none;
  cursor: pointer;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  transition: all 0.2s ease-in-out;

  &:hover {
    background-color: #4338ca;
    transform: scale(1.02);
  }
`;

export const SecondaryButton = styled(PrimaryButton)`
  font-size: 0.875rem;
  background-color: #e5e7eb;
  padding: 0.75rem 0;
  color: #1f2937;
  box-shadow: none;

  &:hover {
    background-color: #d1d5db;
    transform: none;
  }
`;

export const StatusBadge = styled.span`
  font-size: 0.75rem;
  font-weight: 600;
  padding: 0.25rem 0.5rem;
  border-radius: 9999px;
  background-color: ${props => props.status === 'Concluída' ? '#d1fae5' : '#fef3c7'};
  color: ${props => props.status === 'Concluída' ? '#065f46' : '#92400e'};
`;
