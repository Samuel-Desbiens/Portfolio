<template>
  <div>
    <p id="userName" v-if="isLoggedIn && onError === false">Connecter en tant que {{ this.name }}</p>
    <select id="parksSelect" class="custom-select" :disabled="hasServiceError" v-model="selectedPark">
      <option v-for="park in this.parks" :value="park" :key="park.id">{{ park.name }}</option>
    </select>
    <select
      id="trailsSelect"
      class="custom-select"
      :disabled="hasServiceError || selectedPark === null"
      v-model="selectedTrail"
      style="margin-top: 15px"
    >
      <option v-for="trail in trails" :value="trail" :key="trail.id">{{ trail.name }}</option>
    </select>
    <b-container id="trailInfo" fluid v-if="selectedTrail !== null">
      <b-row style="margin-top: 15px">
        <b-col>
          <img
            v-if="likeLoading"
            src="@/assets/loadingWaiting.gif"
            style="height: 50px"
          />
          <img
            id="liked"
            v-if="isLiked === true && likeLoading === false"
            src="@/assets/liked.png"
            alt="liked"
            style="height: 50px"
            @click="unlike()"
          />
          <img
            id="disliked"
            v-if="isLiked === false && likeLoading === false"
            src="@/assets/notLiked.png"
            alt="notLiked"
            style="height: 50px"
            @click="like()"
          />
        </b-col>
        <b-col>
          <p id="trailName">{{ this.selectedTrail.name }}</p>
        </b-col>
      </b-row>
      <b-row style="margin-top: 15px">
        <b-col>
          <p id="nbLikes">{{ this.likes.length }}</p>
        </b-col>
        <b-col>
          <p id="parkName">Parc: {{ this.selectedTrail.park }}</p>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import { ui } from '../externalization/uiTextPlugin.js'
export default {
  data () {
    return {
      selectedPark: null,
      selectedTrail: null,
      hasServiceError: false,
      isLiked: false,
      likeLoading: false
    }
  },
  async created () {
    try {
      await this.$store.dispatch('map/loadParks')
    } catch (error) {
      this.hasServiceError = true
      this.makeToast(ui.Selection.IMPOSSIBlE_LOADING('les parks'), ui.SERVER_ERROR_TITLE)
    }
    if (this.isLoggedIn) {
      await this.$store.dispatch('profile/getProfile')
      if (this.onError) {
        this.makeToast(ui.Selection.CANT_LOAD_USER, ui.SERVER_ERROR_TITLE)
      }
    }
  },
  watch: {
    selectedPark: async function () {
      await this.loadTrails()
    },
    selectedTrail: async function () {
      if (this.selectedTrail !== null) {
        this.likeLoading = true
        await this.$store.dispatch('map/updateSelectedTrail', this.selectedTrail)
        await this.getLike()
        this.likeLoading = false
      }
    }
  },
  computed: {
    isLoggedIn () {
      return this.$store.getters['authentication/isLoggedIn']
    },
    onError () {
      return this.$store.state.profile.onError
    },
    name () {
      return this.$store.state.profile.name
    },
    parks () {
      return this.$store.state.map.parks
    },
    trails () {
      return this.$store.state.map.trails
    },
    likes () {
      return this.$store.state.map.likes
    }
  },
  methods: {
    async loadTrails () {
      try {
        await this.$store.dispatch('map/loadTrails', this.selectedPark.id)
      } catch (error) {
        this.hasServiceError = true
        this.makeToast(ui.Selection.IMPOSSIBlE_LOADING('les sentiers pour le park choisi'), ui.SERVER_ERROR_TITLE)
      }
    },
    async getLike () {
      await this.updateLikes()
      if (this.isLoggedIn && !this.onError) {
        this.isLiked = await this.$store.dispatch('profile/hasLiked', this.likes)
      }
    },
    async like () {
      this.likeLoading = true
      if (this.isLoggedIn) {
        try {
          await this.$store.dispatch('profile/likeTrail', this.selectedTrail.id)
          this.isLiked = true
        } catch (e) {
          this.makeToast(ui.Selection.IMPOSSIBLE_ACTION('like'), ui.SERVER_ERROR_TITLE)
        }
        this.updateLikes()
      } else {
        this.makeToast(ui.NEEDED_CONNECTION_ACTION, ui.IMPOSSIBLE_ACTION_ERROR_TITLE)
      }
      this.likeLoading = false
    },
    async unlike () {
      this.likeLoading = true
      if (this.isLoggedIn) {
        try {
          await this.$store.dispatch('profile/dislikeTrail', this.likes)
          this.isLiked = false
        } catch (e) {
          this.makeToast(ui.Selection.IMPOSSIBLE_ACTION('retirer un like'), ui.SERVER_ERROR_TITLE)
        }
        await this.updateLikes()
      } else {
        this.makeToast(ui.NEEDED_CONNECTION_ACTION, ui.IMPOSSIBLE_ACTION_ERROR_TITLE)
      }
      this.likeLoading = false
    },
    async updateLikes () {
      try {
        await this.$store.dispatch('map/updateLikes', this.selectedTrail)
      } catch (error) {
        this.isLiked = false
        this.makeToast(ui.Selection.IMPOSSIBlE_LOADING('le nombre de like du sentier'), ui.SERVER_ERROR_TITLE)
      }
    },
    makeToast (msg, title) {
      this.$bvToast.toast(msg, {
        title: title,
        autoHideDelay: 5000,
        appendToast: true
      })
    }
  }
}
</script>
