import { StrictMode } from 'react'
import { AuthProvider, useAuth } from 'react-oidc-context'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'

const oidcConfig ={
  authority: "https://localhost:5001", 
  client_id: "stockmodeclient",
  redirect_uri: "http://localhost:5173/signin-oidc",
  scope: "openid profile email",
  post_logout_redirect_uri: "http://localhost:5173/",
};

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
