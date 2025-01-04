import { parksService } from '@/services/parksService.js'
import { trailsService } from '@/services/trailsService.js'

const state = {
  parks: [],
  trails: [],
  selectedTrail: null,
  likes: []
}

const mutations = {
  initializeParks (state, loadedParks) {
    state.parks = loadedParks
    state.trails = []
    state.likes = []
    state.selectedTrail = null
  },
  setTrails (state, trails) {
    state.trails = trails
  },
  setSelectedTrail (state, selectedTrail) {
    state.selectedTrail = selectedTrail
  },
  setLikes (state, likes) {
    state.likes = likes
  }
}

const actions = {
  async loadParks ({ commit }) {
    const loadedParks = await parksService.getParks()
    const sortedParks = loadedParks.sort((a, b) =>
      a.name > b.name ? 1 : b.name > a.name ? -1 : 0
    )
    commit('initializeParks', sortedParks)
  },
  async loadTrails ({ commit }, selectedParkId) {
    const loadedTrails = await parksService.getTrailsByParkId(
      selectedParkId
    )
    const sortedTrails = loadedTrails.sort((a, b) =>
      a.name > b.name ? 1 : b.name > a.name ? -1 : 0
    )
    commit('setTrails', sortedTrails)
  },
  async updateSelectedTrail ({ commit, dispatch }, selectedTrail) {
    commit('setSelectedTrail', selectedTrail)
    if (selectedTrail != null) {
      dispatch('updateLikes', selectedTrail)
    }
  },
  async updateLikes ({ commit }, selectedTrail) {
    try {
      const loadedLikes = await trailsService.getNbLikesByTrailId(
        selectedTrail.id
      )
      commit('setLikes', loadedLikes)
    } catch (error) {
      commit('setLikes', [])
      throw new Error('Impossible de load le nombre de like')
    }
  }
}

export default {
  namespaced: true,
  state,
  actions,
  mutations
}
