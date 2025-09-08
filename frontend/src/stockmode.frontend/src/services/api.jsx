import axios from 'axios';

const isDevelopment = window.location.hostname === 'localhost';
const apiBaseUrl = isDevelopment ? 'http://localhost:8080/api/' : 'http://stockmode.webapi/api/';

const api = axios.create({
    baseURL: apiBaseUrl
});

// Function to set auth token
export const setAuthToken = (token) => {
    if (token) {
        api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
        delete api.defaults.headers.common['Authorization'];
    }
};

// Request interceptor to add token automatically
api.interceptors.request.use(
    (config) => {
        // Get token from session storage or local storage
        const idpAuthority = isDevelopment ? "http://localhost:5001" : "http://stockmode.idp";
        const user = JSON.parse(sessionStorage.getItem(`oidc.user:${idpAuthority}:stockmodeclient`) || '{}');
        const token = user.access_token;
        
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor to handle auth errors
api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            // Token expired or invalid, redirect to login
            window.location.href = '/signin-oidc';
        }
        return Promise.reject(error);
    }
);

export default api;