import axios from "axios";

const instance = axios.create({
  baseURL: import.meta.env.VITE_APP_API_URL,
});

instance.interceptors.request.use((config: any) => {
  config.headers.Authorization = window.localStorage.getItem("token");

  return config;
});

export default instance;