import router from "@/router";
import axios from "axios";

const axiosIns = axios.create({
  baseURL: "https://localhost:7130/api",
  timeout: 1000,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
  },
});

axiosIns.interceptors.request.use((config) => {
  const token =
    sessionStorage.getItem('accessToken') ||
    localStorage.getItem('accessToken');

  if (token) {
    config.headers = config.headers || {};

    config.headers.Authorization = token ? `Bearer ${JSON.parse(token)}` : "";
  }

  return config;
});

// ℹ️ Add response interceptor to handle 401 response
axiosIns.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    // Handle error
    if (error.response.status === 401) {
      // ℹ️ Logout user and redirect to login page
      // Remove "userData" from localStorage
      localStorage.removeItem("userInfo");

      // Remove "accessToken" from localStorage
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");

      // If 401 response returned from api
      router.push("/login");
    } else {
      return Promise.reject(error);
    }
  }
);
export default axiosIns;
