import axios from 'axios';

// Determine environment
const hostname = window.location.hostname;
const protocol = window.location.protocol;
const port = window.location.port;

const isDevelopment = hostname === 'localhost' || hostname === '127.0.0.1';
const isPortForward = isDevelopment && port && port !== '80' && port !== '443' && port !== '5173';
const isKubernetes = !isDevelopment;

let apiBaseUrl;
let idpAuthority;

const envApiUrl = import.meta.env.VITE_API_URL;
const envIdpUrl = import.meta.env.VITE_IDP_URL;

if (envApiUrl) {
  apiBaseUrl = envApiUrl;
  idpAuthority = envIdpUrl || `${protocol}//${hostname}/idp`;
} else if (isPortForward) {
  apiBaseUrl = 'http://localhost:8081/api';
  idpAuthority = 'https://localhost:5001';
} else if (isDevelopment) {
  apiBaseUrl = 'http://localhost:8080/api'; 
  idpAuthority = 'https://localhost:5001'; 
} else if (isKubernetes) {
  apiBaseUrl = `${protocol}//${hostname}/api`;
  idpAuthority = `${protocol}//${hostname}/idp`;
}

console.log('API Configuration:', { isDevelopment, isPortForward, isKubernetes, apiBaseUrl, idpAuthority });

const api = axios.create({
  baseURL: apiBaseUrl
});

export const setAuthToken = (token) => {
  if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete api.defaults.headers.common['Authorization'];
  }
};

const getStoredOidcUser = () => {
  const clientId = 'stockmodeclient';
  
  const authorities = [
    idpAuthority,
    'https://localhost:5001',
    'http://localhost:5001',
    `${protocol}//${hostname}/idp`,
    'https://stockmode.idp',
  ];

  for (const authority of authorities) {
    const key = `oidc.user:${authority}:${clientId}`;
    const raw = sessionStorage.getItem(key) || localStorage.getItem(key);
    if (raw) {
      try { 
        const user = JSON.parse(raw);
        console.log('Found OIDC user in storage with authority:', authority);
        return user;
      } catch { /* ignore */ }
    }
  }
  
  console.warn('No OIDC user found in storage');
  return null;
};

api.interceptors.request.use(
  (config) => {
    const user = getStoredOidcUser();
    const token = user?.access_token;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      window.location.href = '/signin-oidc';
    }
    return Promise.reject(error);
  }
);

export default api;