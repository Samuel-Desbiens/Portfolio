<template>
  <div class="register">
    <div class="col d-flex justify-content-center">
      <div class="card w-75 mt-3">
        <div class="card-header bg-dark text-light"> Créer un compte </div>
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
              <div class="input-group-prepend mt-3">
                <div class = "row w-100">
                  <div class="input-group-text col">Nom Compte:</div>
                  <input type="text" class="form-control w-100 col-8" placeholder="Nom du Compte" aria-label="Name" aria-describedby="basic-addon1" v-model="name">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="registerButton mt-3">
      <button id="register" class="btn btn-primary" @click="register()">Créer le compte</button>
      <div id="errorMsg" v-if="authServiceError">{{ authServiceError }}</div>
    </div>
  </div>
</template>

<script>

export default {
  name: 'Register',
  data () {
    return {
      email: '',
      password: '',
      name: ''
    }
  },
  methods: {
    async register () {
      await this.$store.dispatch('authentication/register', {
        email: this.email,
        password: this.password,
        name: this.name
      })
      if (!this.authServiceError) {
        this.$router.push({
          name: 'Login'
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
