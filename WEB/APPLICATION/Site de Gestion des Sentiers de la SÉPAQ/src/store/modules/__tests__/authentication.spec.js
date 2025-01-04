import authStore from '@/store/modules/authentication.js'
import { authService } from '@/services/authService'
import tokenHelper from '@/shared/tokenHelper'

const EMPTY_STATE = {
    token: '',
    authServiceError: ''
}

const FULLED_STATE = {
    token: '111111111',
    authServiceError: 'aaaaaaaaaa'
}

jest.mock('@/services/authService')
jest.mock('@/shared/tokenHelper')

describe('authentication store module', () => {
  describe('getters', () => {
    test('getTokenUserId retourne le id', async () => {
      const state = FULLED_STATE
      tokenHelper.getUserId.mockResolvedValue('1')

      const result = await authStore.getters.getTokenUserId(state)

      expect(result).toStrictEqual('1')
    })
  })
  describe('mutations', () => {
    test('clearError vide le texte de l erreur', async () => {
      const state = FULLED_STATE
      await authStore.mutations.clearError(state)
  
      expect(state.token).toStrictEqual(FULLED_STATE.token)
      expect(state.authServiceError).toStrictEqual(EMPTY_STATE.authServiceError)
    })

    test('initializeAuthentication initialise l auth', async () => {
      const state = FULLED_STATE
      await authStore.mutations.initializeAuthentication(state, '1')
    
      expect(state.token).toStrictEqual('1')
      expect(state.authServiceError).toStrictEqual(EMPTY_STATE.authServiceError)
    })

    test('logout efface le token', async () => {
      const state = FULLED_STATE
      await authStore.mutations.logout(state)
      
      expect(state.token).toStrictEqual('')
      expect(state.authServiceError).toStrictEqual(FULLED_STATE.authServiceError)
    })

    test('setAuthServiceError change le texte de l erreur', async () => {
      const state = FULLED_STATE
      await authStore.mutations.setAuthServiceError(state, 'new error')
        
      expect(state.token).toStrictEqual(FULLED_STATE.token)
      expect(state.authServiceError).toStrictEqual('new error')
    })
  })
  describe('mutations', () => {
    test('login fait l initialisation si aucune erreur ne se produit', async () => {
      const commit = jest.fn()
      const credential = 'credential'
      authService.getToken.mockResolvedValue('mock get token')
      await authStore.actions.login({ commit }, credential)
      expect(commit).toBeCalledWith('initializeAuthentication', 'mock get token')
    })

    test('login fait ajoute le message d erreur si une erreur se produit', async () => {
      const commit = jest.fn()
      const credential = 'credential'
      authService.getToken.mockImplementation(() => {
        throw new Error('mock get token');
      })  
      await authStore.actions.login({ commit }, credential)
      expect(commit).toBeCalledWith('setAuthServiceError', 'mock get token')
    })

    test('logout fait le commit logout', async () => {
      const commit = jest.fn()
      await authStore.actions.logout({ commit })
      expect(commit).toBeCalledWith('logout')
    })

    test('register efface les erreurs si aucune erreur ne se produit', async () => {
      const commit = jest.fn()
      const credential = 'credential'
      authService.postUser.mockResolvedValue('mock get token')
      await authStore.actions.login({ commit }, credential)
      expect(commit).toBeCalledWith('setAuthServiceError', 'mock get token')
    })
  
    test('register ajoute le message d erreur si une erreur se produit', async () => {
      const commit = jest.fn()
      const credential = 'credential'
      authService.postUser.mockImplementation(() => {
        throw new Error('mock get token');
      })  
      await authStore.actions.login({ commit }, credential)
      expect(commit).toBeCalledWith('setAuthServiceError', 'mock get token')
    })
  })
})