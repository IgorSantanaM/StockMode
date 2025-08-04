import { AuthProvider } from 'react-oidc-context'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'

const oidcConfig ={
  authority: "https://localhost:5001", 
  client_id: "stockmodeclient",
  redirect_uri: "https://localhost:5173/signin-oidc",
  scope: "openid profile email",
  post_logout_redirect_uri: "https://localhost:5173/",

  automaticSilentRenew: false,
  
  monitorSession: false,
  
  response_mode: 'query', 
};

const onSigninCallback = (user) => {
  console.log("AuthProvider's onSigninCallback triggered!", user);
  window.location.replace("/");
};
createRoot(document.getElementById('root')).render(
    <AuthProvider {...oidcConfig} onSigninCallback={onSigninCallback}>
        <App />
    </AuthProvider>
)
