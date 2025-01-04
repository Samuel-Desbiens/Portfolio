<template>
  <div class="login">
    <div class="col d-flex justify-content-center">
      <div class="card w-75 mt-3">
        <div class="card-header bg-dark text-light"> Login </div>
        <div class="card-body">
          <div class="input-group mb-3">
            <div class="container">
              <div class="input-group-prepend mt-3">
                <div class = "row w-100">
                  <div class="input-group-text col">Email:</div>
                  <input type="text" class="form-control w-100 col-8" placeholder="ABC@fournisseur.ca" aria-label="Email" aria-describedby="basic-addon1" v-model="email">
                </div>
              </div>
              <div class="input-group-prepend mt-3">
                <div class = "row w-100">
                  <div class="input-group-text col">Mot de Passe:</div>
                  <input type="password" class="form-control w-100 col-8" placeholder="Mots de Passe" aria-label="Password" aria-describedby="basic-addon1" v-model="password">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="loginButton mt-4 w-75">
      <button id="login" class="btn btn-primary" @click="login()">Se Connecter</button>
      <router-link id="toRegister" class="nav-link active" v-bind:to="{ name: 'Register'}">Créer un compte</router-link>
      <div id="errorMsg" v-if="authServiceError">{{ authServiceError }}</div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'Login',
  created () {
    this.$store.commit('authentication/clearError')
  },
  data () {
    return {
      email: '',
      password: ''
    }
  },
  methods: {
    async login () {
      await this.$store.dispatch('authentication/login', {
        email: this.email,
        password: this.password
      })
      if (!this.authServiceError) {
        this.$router.push({
          name: 'Home'
        })
      }
    }
  },
  computed: {
    authServiceError () {
      return this.$store.state.authentication.authServiceError
    }
  }
}
</script>
