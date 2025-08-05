import { createGlobalStyle, styled } from 'styled-components';

export const GlobalStyle =  createGlobalStyle`
  body {
    margin: 0;
    font-family: 'Roboto', -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Oxygen',
      'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',
      sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    background-color: #f3f5f7;
  }
`;

const AppContainer = styled.div`
  display: flex;
  height: 100vh;
`;

// Este contêiner ocupa o espaço restante e permite a rolagem do conteúdo.
const MainContent = styled.main`
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
`;

const LoadingContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  font-size: 1.5rem;
`;