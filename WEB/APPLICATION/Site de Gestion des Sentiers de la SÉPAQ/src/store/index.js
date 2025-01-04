import Vue from 'vue'
import Vuex from 'vuex'
import authentication from './modules/authentication'
import profile from './modules/profile'
import map from './modules/map'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    authentication,
    profile,
    map
  }
})
