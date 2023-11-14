import axios from "axios";

const axiosInstance = axios.create();

axiosInstance.interceptors.request.use(
  (config) => {
    // Retrieve the token from wherever you store it (e.g., localStorage)
    const token = localStorage.getItem("accessToken");

    // Add the Authorization header if a token exists
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    // Handle request error
    return Promise.reject(error);
  }
);

export default axiosInstance;
