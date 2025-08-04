import styled from "styled-components";

export const Card = styled.div`
  background-color: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 2px 0 rgb(0 0 0 / 0.05);
`;

export const ChartCard = styled(Card)`
  grid-column: span 1 / span 1;
  @media (min-width: 1280px) { grid-column: span 2 / span 2; }
`;