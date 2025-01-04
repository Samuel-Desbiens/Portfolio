import profileStore from '@/store/modules/profile.js'
import { userService } from '@/services/userService'
import { usersJsonFake } from '@/../tests/data/usersJsonFake'

jest.mock('@/services/userService')

const FILLED_STATE = {
  email: 'JSPPPPPPPPPPP',
  name: 'JSPPPPPPPPPPP',
  onError: true
}

const EMPTY_STATE = {
    email: '',
    name: '',
    onError: false
  }

let firstUser
let users

beforeEach(() => {
  users = [...usersJsonFake]
  firstUser = {...users[0] }
})


describe('Profile store module', () => {
  describe('mutations', () => {
    test('initializeProfile doit initialiser le profile', async () => {
      const state = FILLED_STATE
      profileStore.mutations.initializeProfile(state, firstUser)
  
      expect(state.email).toStrictEqual(firstUser.email)
      expect(state.name).toStrictEqual(firstUser.name)
      expect(state.onError).toStrictEqual(false)
    })

    test('setOnError mettre le state erreur a true', async () => {
      const state = EMPTY_STATE
      profileStore.mutations.setOnError(state)
    
      expect(state.email).toStrictEqual(EMPTY_STATE.email)
      expect(state.name).toStrictEqual(EMPTY_STATE.name)
      expect(state.onError).toStrictEqual(true)
    })
  })
  describe('actions', () => {
    test('getProfile mets a jour le profile si aucune erreur survient', async () => {
      const commit = jest.fn()
      const rootGetters = {
        authentication: {
            getTokenUserId: () => firstUser.userId
        }
      }
      userService.getUserById.mockResolvedValue(firstUser)

      await profileStore.actions.getProfile({commit, rootGetters})
      
      expect(commit).toHaveBeenCalledWith('initializeProfile', firstUser)
    })

    test('getProfile mets le state erreur si une erreur survient', async () => {
      const commit = jest.fn()
      const rootGetters = {
        authentication: {
          getTokenUserId: () => firstUser.userId
        }
      }
      userService.getUserById.mockImplementation(() => {
        throw new Error();
      })  
      await profileStore.actions.getProfile({commit, rootGetters})  
      expect(commit).toHaveBeenCalledWith('setOnError')
    })

    test('hasLiked retourne true si le userId est dans likes', async () => {
      const rootGetters = []
      rootGetters['authentication/getTokenUserId'] = 1
      const likes = [
        {
          userId: 1,
          trailId: 1,
          id: 1
        }
      ] 
      const result = await profileStore.actions.hasLiked({rootGetters}, likes)  
      expect(result).toBeTruthy()
    })

    test('hasLiked retourne false si le userId n est pas dans likes', async () => {
      const rootGetters = []
      rootGetters['authentication/getTokenUserId'] = 2
      const likes = [
        {
          userId: 1,
          trailId: 1,
          id: 1
        }
      ] 
      const result = await profileStore.actions.hasLiked({rootGetters}, likes)  
      expect(result).toBeFalsy()
    })

    test('likeTrail like trail with user id', async () => {
      const rootGetters = []
      rootGetters['authentication/getTokenUserId'] = 2
      const trailId = 2
      userService.likeTrail.mockResolvedValue('Worked')

      const result = await profileStore.actions.likeTrail({rootGetters}, trailId)  
      expect(result).toBe('Worked')
    })

    test('dislikeTrail dislike trail with user id', async () => {
      const rootGetters = []
      rootGetters['authentication/getTokenUserId'] = 1
      const likes = [
        {
          userId: 1,
          trailId: 1,
          id: 1
        }
      ] 
      userService.dislikeTrail.mockResolvedValue('Worked')
  
      const result = await profileStore.actions.dislikeTrail({rootGetters}, likes)  
      expect(result).toBe('Worked')
    })
  })
})