import React from 'react'
import { AuthProvider } from 'react-oidc-context'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'

const isDevelopment = window.location.hostname === 'localhost';
const idpAuthority = isDevelopment ? "https://localhost:5001" : "https://stockmode.idp";
const frontendUrl = isDevelopment ? "http://localhost" : "https://localhost";

const oidcConfig = {
  authority: idpAuthority,
  client_id: "stockmodeclient",
  redirect_uri: `${frontendUrl}/signin-oidc`,
  scope: "openid profile email stockmodeapi",
  post_logout_redirect_uri: `${frontendUrl}/`,
  automaticSilentRenew: true,
  monitorSession: false,
  response_type: 'code',
  response_mode: 'query',
  loadUserInfo: true,
  
  // Critical: This tells the library to automatically process the callback
  onSigninCallback: (_user) => {
    // Remove the query string from the URL after successful signin
    window.history.replaceState({}, document.title, window.location.pathname);
  }
};

createRoot(document.getElementById('root')).render(
    <BrowserRouter>
        <AuthProvider {...oidcConfig}>
            <App />
        </AuthProvider>
    </BrowserRouter>
)
