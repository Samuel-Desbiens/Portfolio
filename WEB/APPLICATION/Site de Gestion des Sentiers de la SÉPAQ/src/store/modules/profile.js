import { userService } from '@/services/userService'

const state = {
  email: '',
  name: '',
  onError: false
}

const getters = {}

const mutations = {
  initializeProfile (state, profile) {
    state.email = profile.email
    state.name = profile.name
    state.onError = false
  },
  setOnError (state) {
    state.onError = true
  }
}

const actions = {
  async getProfile ({ commit, rootGetters }) {
    try {
      const userId = rootGetters['authentication/getTokenUserId']
      const profile = await userService.getUserById(userId)
      commit('initializeProfile', profile)
    } catch (error) {
      commit('setOnError')
    }
  },
  async hasLiked ({ rootGetters }, likes) {
    const userId = rootGetters['authentication/getTokenUserId']
    const isFound = await likes.some(like => {
      if (like.userId === userId) {
        return true
      }
      return false
    })
    return isFound
  },
  async likeTrail ({ rootGetters }, trailId) {
    const userId = rootGetters['authentication/getTokenUserId']
    const reponse = await userService.likeTrail(userId, trailId)
    return reponse
  },
  async dislikeTrail ({ rootGetters }, likes) {
    const userId = rootGetters['authentication/getTokenUserId']
    const like = await likes.find(like => {
      if (like.userId === userId) {
        return true
      }
      return false
    })
    const reponse = await userService.dislikeTrail(like.id)
    return reponse
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
}
