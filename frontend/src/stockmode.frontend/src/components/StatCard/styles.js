import styled from "styled-components";

export const Card = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 2px 0 ${props => props.theme.colors.shadow};
  transition: background-color 0.3s ease, box-shadow 0.3s ease;
  border: 1px solid ${props => props.theme.colors.border};
`;

export const ChartCard = styled(Card)`
  grid-column: span 1 / span 1;
  @media (min-width: 1280px) { grid-column: span 2 / span 2; }
`;