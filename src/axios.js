import axios from 'axios';

const accessToken = window.localStorage.getItem('accessToken');

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7211/api/',
  headers: {
    'content-type': 'application/json',
    Authorization: 'Bearer ' + accessToken,
  },
});

axiosInstance.interceptors.response.use(
  (response) => response.data,
  (error) => Promise.reject((error.response && error.response.data) || 'Something went wrong!')
);

export const axiosFormData = axios.create({
  baseURL: 'https://localhost:7211/api/',
  headers: {
    'Content-Type': 'multipart/form-data',
    Authorization: 'Bearer ' + accessToken,
  },
});

axiosFormData.interceptors.response.use(
  (response) => response.data,
  (error) => Promise.reject((error.response && error.response.data) || 'Something went wrong!')
);

export default axiosInstance;
