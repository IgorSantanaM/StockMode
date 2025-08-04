import React from 'react';
import {BrowserRouter} from 'react-router-dom'
import Routes from './routes.jsx';
import { useAuth } from 'react-oidc-context';
import {GlobalStyle} from './styles/Global.jsx';
import { LoadingContainer } from './util/LoadingContainer.js';
function App() {
  const auth = useAuth();

  if(auth.isLoading){
    return <LoadingContainer>Carregando autenticação...</LoadingContainer>
  }
  return (
    <BrowserRouter>
      <GlobalStyle />
      <Routes />
    </BrowserRouter>
  );
}

export default App
