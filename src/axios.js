import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7211/api/',
  headers: {
    'content-type': 'application/json',
  },
});

axiosInstance.interceptors.response.use(
  (response) => response.data,
  (error) => Promise.reject((error.response && error.response.data) || 'Something went wrong!')
);

export default axiosInstance;
