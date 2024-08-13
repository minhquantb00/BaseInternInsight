import axios from 'axios'
import router from '@/router'
import LocalStorageKey from '@/constants/LocalStorageKey'

const axiosIns = axios.create({
baseURL: import.meta.env.VITE_BASE_API_URL,
timeout: 1000,
headers: {
  'Accept': 'application/json',
  'Content-Type': 'application/json',
  'Access-Control-Allow-Origin': '*',
},
})


axiosIns.interceptors.request.use(config => {
  const token = sessionStorage.getItem(LocalStorageKey.ACCESS_TOKEN) || localStorage.getItem(LocalStorageKey.ACCESS_TOKEN)

  if (token) {
    config.headers = config.headers || {}

    config.headers.Authorization = token ? `Bearer ${JSON.parse(token)}` : ''
  }

  return config
})

// ℹ️ Add response interceptor to handle 401 response
axiosIns.interceptors.response.use(response => {
  return response
}, error => {
  // Handle error
  if (error.response.status === 401) {
    // ℹ️ Logout user and redirect to login page
    // Remove "userData" from localStorage
    localStorage.removeItem('userData')

    // Remove "accessToken" from localStorage
    localStorage.removeItem('accessToken')
    localStorage.removeItem('userAbilities')

    // If 401 response returned from api
    router.push('/login')
  }
  else {
    return Promise.reject(error)
  }
})
export default axiosIns
