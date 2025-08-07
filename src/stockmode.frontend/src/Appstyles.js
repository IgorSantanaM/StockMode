import styled from "styled-components";

export const AppContainer = styled.div`
  display: flex;
  flex-direction: row;
  height: 100vh;
  width: 100%;
  background-color: #f3f5f7;
  overflow: hidden;

  @media (max-width: 768px) {
    flex-direction: column;
  }
`;

export const PageWrapper = styled.div`
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  padding: 1rem;
  box-sizing: border-box;
`;

export const MainContent = styled.main`
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
  width: 100%;
  height: 100%;
`;
