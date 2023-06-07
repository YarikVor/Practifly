import axios from 'axios';
import { getTokenFromLocalStorage } from '../handlers/handlers';

const instance = axios.create({
  baseURL: import.meta.env.VITE_APP_API_URL,
  timeout: 10000, 
});

instance.interceptors.request.use(
  (config) => {
    const token = getTokenFromLocalStorage("token");
    if(!token) {
      return config;
    }
    config.headers.Authorization = `Bearer ${token}`;
    config.headers['Content-Type'] = "application/json";
    return config;
  }, 
  (error) => {
    return Promise.reject(error);
  }
);

instance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default instance;