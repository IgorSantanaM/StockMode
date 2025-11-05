import React from 'react'
import { AuthProvider } from 'react-oidc-context'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'

const hostname = window.location.hostname;
const protocol = window.location.protocol;
const port = window.location.port;

const apiUrl = import.meta.env.VITE_API_URL;
const idpUrl = import.meta.env.VITE_IDP_URL;

const isDevelopment = hostname === 'localhost' || hostname === '127.0.0.1';
const isPortForward = isDevelopment && port && port !== '80' && port !== '443' && port !== '5173';
const isKubernetes = !isDevelopment;

let idpAuthority;
let frontendUrl;

if (idpUrl) {
  idpAuthority = idpUrl;
  frontendUrl = apiUrl ? apiUrl.replace('/api', '') : `${protocol}//${hostname}${port ? ':' + port : ''}`;
} else if (isPortForward) {
  idpAuthority = "http://localhost:5001";
  frontendUrl = `${protocol}//${hostname}${port ? ':' + port : ''}`;
} else if (isDevelopment) {
  idpAuthority = "https://localhost:5001";
  frontendUrl = `http://localhost${port && port !== '80' ? ':' + port : ''}`;
} else if (isKubernetes) {
  idpAuthority = `${protocol}//${hostname}/idp`;
  frontendUrl = `${protocol}//${hostname}`;
}

console.log('OIDC Configuration:', { 
  isDevelopment, 
  isPortForward, 
  isKubernetes, 
  idpAuthority, 
  frontendUrl,
  port,
  envVars: { apiUrl, idpUrl }
});

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
  
  onSigninCallback: (_user) => {
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
