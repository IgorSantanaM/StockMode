import React from 'react';
import {BrowserRouter} from 'react-router-dom'
import Routes from './routes.jsx';
import {GlobalStyle} from './styles/Global.jsx';

function App() {
  return (
    <BrowserRouter>
    <GlobalStyle />
      <Routes />
    </BrowserRouter>
  );
}

export default App
