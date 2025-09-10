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

api.interceptors.request.use(
    (config) => {
        const idpAuthority = isDevelopment ? "https://localhost:5001" : "https://stockmode.idp";
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