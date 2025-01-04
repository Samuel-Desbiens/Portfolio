import axios from 'axios'
import store from '../store/index'

const requestInterceptor = axios.create({})

requestInterceptor.interceptors.request.use(req => {
  if (store.getters['authentication/isLoggedIn']) {
    req.headers.authorization = `Bearer ${store.state.authentication.token}`
  }
  return req
})

export default requestInterceptor
