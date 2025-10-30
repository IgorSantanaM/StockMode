import styled from "styled-components";

export const AppContainer = styled.div`
  display: flex;
  height: 100vh;
  width: 100vw;
  background-color: ${props => props.theme.colors.background};
  overflow: hidden;
  transition: background-color 0.3s ease;
`;

export const MainContent = styled.main`
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  height: 100vh;
  overflow-x: hidden;
`;

export const PageWrapper = styled.div`
  flex-grow: 1;
  overflow-y: auto; 
  padding: 1.5rem;
  @media (max-width: 768px) {
    padding: 1rem;
  }
`;
