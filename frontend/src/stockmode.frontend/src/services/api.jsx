import axios from 'axios';

const isDevelopment = window.location.hostname === 'localhost';
const apiBaseUrl = isDevelopment ? 'http://localhost:8080/api/' : 'http://stockmode.webapi/api/';

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
  const authorities = isDevelopment
    ? ['http://localhost:5001', 'https://localhost:5001']
    : ['http://stockmode.idp', 'https://stockmode.idp'];

  for (const authority of authorities) {
    const key = `oidc.user:${authority}:${clientId}`;
    const raw = sessionStorage.getItem(key) || localStorage.getItem(key);
    if (raw) {
      try { return JSON.parse(raw); } catch { /* ignore */ }
    }
  }
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