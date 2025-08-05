import React from 'react';
import {BrowserRouter} from 'react-router-dom'
import Routes from './routes.jsx';
import { useAuth } from 'react-oidc-context';
import {GlobalStyle} from './styles/Global.jsx';
import { LoadingContainer } from './util/LoadingContainer.js';
import SideBar from './components/Sidebar/index.jsx';
import { AppContainer, MainContent } from './pages/home/styles.js';
import Header from './components/Header/index.jsx';
function App() {
  const auth = useAuth();

  if(auth.isLoading){
    return <LoadingContainer>Carregando autenticação...</LoadingContainer>
  }
  return (
    <BrowserRouter>
      <GlobalStyle />
      <AppContainer>
        <SideBar />
        <MainContent>
        <Header />
          <Routes />
        </MainContent>
      </AppContainer>
    </BrowserRouter>
  );
}

export default App
