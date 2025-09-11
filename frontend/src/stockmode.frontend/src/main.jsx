import React from 'react'
import { AuthProvider } from 'react-oidc-context'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'

const isDevelopment = window.location.hostname === 'localhost';
const idpAuthority = isDevelopment ? "https://localhost:5001" : "https://stockmode.idp";
const frontendUrl = isDevelopment ? "http://localhost" : "http://localhost";

const oidcConfig ={
  authority: idpAuthority,
  client_id: "stockmodeclient",
  redirect_uri: `${frontendUrl}/signin-oidc`,
  scope: "openid profile email stockmodeapi",
  post_logout_redirect_uri: `${frontendUrl}/`,


  automaticSilentRenew: true,
  
  monitorSession: false,
  
  response_mode: 'query', 
  loadUserInfo: true,
};

const onSigninCallback = (user) => {
  console.log("AuthProvider's onSigninCallback triggered!", user);
  window.location.replace("/");
};
createRoot(document.getElementById('root')).render(
    <BrowserRouter>
        <AuthProvider {...oidcConfig} onSigninCallback={onSigninCallback}>
            <App />
        </AuthProvider>
    </BrowserRouter>
)
