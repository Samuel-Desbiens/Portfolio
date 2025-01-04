export const uiTextPlugin = {
  welcomeMessage: 'Allo tout le monde !'
}

export const ui = {
  SERVER_ERROR_TITLE: 'Une erreur serveur est survenue!',
  CLIENT_ERROR_TITLE: 'Une erreur côté client est survenue!',
  IMPOSSIBLE_ACTION_ERROR_TITLE: 'Action Impossible',
  NEEDED_CONNECTION_ACTION: 'Veuillez vous connecter faire cette action',
  Selection: {
    IMPOSSIBLE_ACTION (action) {
      return 'Impossible de ' + action + ' pour le moment...'
    },
    IMPOSSIBlE_LOADING (subject) {
      return 'Impossible de charger ' + subject
    },
    CANT_LOAD_USER: 'Erreur du chargement du profile'
  },
  Map: {
    CANT_LOAD_TRAIL: 'Impossible de charger le sentier choisi'
  }
}

// Plugin pour VueJs
// https://coderethinked.com/3-different-ways-to-access-constants-in-a-vue-template/
uiTextPlugin.install = function (Vue) {
  Vue.prototype.$getUiText = key => {
    return uiTextPlugin[key]
  }
}

export default uiTextPlugin
