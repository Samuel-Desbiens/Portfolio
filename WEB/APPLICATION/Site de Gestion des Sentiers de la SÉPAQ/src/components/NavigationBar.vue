<template>
  <nav class="navbar navbar-expand navbar-dark bg-dark">
    <a class="navbar-brand">TP3</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse">
      <router-link class="nav-link active" v-bind:to="{ name: 'Home' }">Accueil</router-link>
      <router-link v-if="isLoggedIn === false" class="nav-link active" v-bind:to="{ name: 'Login' }">Connexion</router-link>
      <a v-if="isLoggedIn" class="nav-link active" id="logoutLink" @click="logout">Déconnexion</a>
    </div>
  </nav>
</template>

<script>
export default {
  computed: {
    isLoggedIn () {
      return this.$store.getters['authentication/isLoggedIn']
    }
  },
  methods: {
    logout () {
      this.$store.dispatch('authentication/logout')
      this.$router.push({
        name: 'Login'
      })
    }
  }
}
</script>
