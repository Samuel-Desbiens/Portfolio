import { authService } from '@/services/authService'
import tokenHelper from '@/shared/tokenHelper'

const state = {
  token: '',
  authServiceError: ''
}

const getters = {
  isLoggedIn (state) {
    return !!state.token
  },
  getTokenUserId (state) {
    const userId = tokenHelper.getUserId(state.token)
    return userId
  }
}

const mutations = {
  clearError (state) {
    state.authServiceError = ''
  },
  initializeAuthentication (state, token) {
    state.token = token
    state.authServiceError = ''
  },
  logout (state) {
    state.token = ''
  },
  setAuthServiceError (state, message) {
    state.authServiceError = message
  }
}

const actions = {
  async login ({ commit }, credential) {
    try {
      const token = await authService.getToken(credential)
      commit('initializeAuthentication', token)
    } catch (error) {
      commit('setAuthServiceError', error.message)
    }
  },
  logout ({ commit }) {
    commit('logout')
  },
  async register ({ commit }, credential) {
    try {
      await authService.postUser(credential)
      commit('setAuthServiceError', '')
    } catch (error) {
      commit('setAuthServiceError', error.message)
    }
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
}
