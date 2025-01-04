import { parksService } from '@/services/parksService.js'
import { trailsService } from '@/services/trailsService.js'
import { parksJsonFake } from '@/../tests/data/parksJsonFake'
import { trailsJsonFake } from '@/../tests/data/trailsJsonFake'
import { likesJsonFake } from '@/../tests/data/likesJsonFake'
import mapStore from '@/store/modules/map.js'

jest.mock('@/services/parksService')
jest.mock('@/services/trailsService')

let likes
let parks
let firstParks
let trails
let firstTrail

const FILLED_STATE = { 
  parks: ['array', 'avec', 'du', 'stock'], 
  trails: ['array', 'avec', 'du', 'stock'],
  selectedTrail: {
    propriete: 'une valeur'
  },
  likes: ['array', 'avec', 'du', 'stock'] 
}

beforeEach(() => {
  parks = [...parksJsonFake]
  firstParks = { ...parks[0] }
  trails = [...trailsJsonFake]
  firstTrail = {...trails[0] }
  likes = [...likesJsonFake]
})

describe('Map store module', () => {
  describe('mutations', () => {
    test('initializeParks doit initialiser les parks et rénitialiser les autres states', async () => {
      const state = FILLED_STATE
      mapStore.mutations.initializeParks(state, parks)

      expect(state.parks).toStrictEqual(parks)
      expect(state.trails).toStrictEqual([])
      expect(state.selectedTrail).toStrictEqual(null)
      expect(state.likes).toStrictEqual([])
    })

    test('setTrails doit initialiser les trails et pas changer les autres states', async () => {
      const state = FILLED_STATE
      mapStore.mutations.initializeParks(state, parks)
      mapStore.mutations.setTrails(state, trails)

      expect(state.parks).toStrictEqual(parks)
      expect(state.trails).toStrictEqual(trails)
      expect(state.selectedTrail).toStrictEqual(null)
      expect(state.likes).toStrictEqual([])
    })

    test('setSelectedTrail doit initialiser le selected trail et pas changer les autres states', async () => {
      const state = FILLED_STATE
      mapStore.mutations.initializeParks(state, parks)
      mapStore.mutations.setTrails(state, trails)
      mapStore.mutations.setSelectedTrail(state, firstTrail)
      
      expect(state.parks).toStrictEqual(parks)
      expect(state.trails).toStrictEqual(trails)
      expect(state.selectedTrail).toStrictEqual(firstTrail)
      expect(state.likes).toStrictEqual([])
    })

    test('setLikes doit initialiser les trails et pas changer les autres states', async () => {
      const state = FILLED_STATE
      mapStore.mutations.initializeParks(state, parks)
      mapStore.mutations.setTrails(state, trails)
      mapStore.mutations.setSelectedTrail(state, firstTrail)
      
      expect(state.parks).toStrictEqual(parks)
      expect(state.trails).toStrictEqual(trails)
      expect(state.selectedTrail).toStrictEqual(firstTrail)
      expect(state.likes).toStrictEqual([])
    })
  })
  describe('actions', () => {
    test('loadParks doit faire la mutation suite au resultat de l API', async () => {
      const commit = jest.fn()
      parksService.getParks.mockResolvedValue(parks)
      
      await mapStore.actions.loadParks({ commit })

      expect(commit).toHaveBeenCalledWith('initializeParks', parks)
    })

    test('loadTrails doit faire la mutation suite au resultat de l API', async () => {
      const commit = jest.fn()
      parksService.getTrailsByParkId.mockResolvedValue(trails)
      
      await mapStore.actions.loadTrails({ commit }, firstParks.id)

      expect(commit).toHaveBeenCalledWith('setTrails', trails)
    })

    test('updateSelectedTrail doit faire la mutation du trail et update les likes si non null', async () => {
      const commit = jest.fn()
      const dispatch = jest.fn()
      
      await mapStore.actions.updateSelectedTrail({ commit, dispatch }, firstTrail)

      expect(commit).toHaveBeenCalledWith('setSelectedTrail', firstTrail)
      expect(dispatch).toHaveBeenCalledWith('updateLikes', firstTrail)
    })

    test('updateSelectedTrail doit faire la mutation du trail et update pas les likes si null', async () => {
      const commit = jest.fn()
      const dispatch = jest.fn()
      const trail = null
      
      await mapStore.actions.updateSelectedTrail({ commit, dispatch }, trail)

      expect(commit).toHaveBeenCalledWith('setSelectedTrail', trail)
      expect(dispatch).not.toHaveBeenCalled()
    })

    test('updateLikes doit faire la mutation des likes si aucune erreur survient', async () => {
      const commit = jest.fn()
      trailsService.getNbLikesByTrailId.mockResolvedValue(likes)

      await mapStore.actions.updateLikes({ commit }, firstTrail.id)

      expect(commit).toHaveBeenCalledWith('setLikes', likes)
    })

    test('updateLikes doit faire la mutation des likes et lancer une erreur si une erreur survient', async () => {
      const commit = jest.fn()
      trailsService.getNbLikesByTrailId.mockImplementation(() => {
        throw new Error()
      })

      try {
        await mapStore.actions.updateLikes({ commit }, firstTrail.id)
      } catch (error) {
        expect(error).toBeInstanceOf(Error)
        expect(String(error)).toBe('Error: Impossible de load le nombre de like')
      }

      expect(commit).toHaveBeenCalledWith('setLikes', [])
    })
  })
})
