import Map from '@/components/Map.vue'
import { shallowMount } from '@vue/test-utils'
import flushPromises from 'flush-promises'
import { segmentsService } from '@/services/segmentsService.js'
import { trailsJsonFake } from '@/../tests/data/trailsJsonFake'
import { segmentsJsonFake } from '@/../tests/data/segmentsJsonFake'
import { ui } from '@/externalization/uiTextPlugin.js'

jest.mock('@/services/segmentsService')

let firstTrail
let segments
let firstSegment

beforeEach(() => {
  firstTrail = {...trailsJsonFake[0]}
  segments = [...segmentsJsonFake]
  firstSegment = {...segments[0]}
})

describe('Map.vue', () => {
  test('La map commencent avec un array vide et un zoom de 12', async () => {
    const mocks = {
      $store: createSimpleStore(null),
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Map, {
      mocks: mocks
    })
    expect(wrapper.vm.zoom).toBe(12)
    expect(wrapper.vm.polylines.length).toBe(0)
  })

  test('getColor retourne la valeur désiré selon la difficulté', async () => {
    const mocks = {
      $store: createSimpleStore(null),
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Map, {
      mocks: mocks
    })
    expect(wrapper.vm.getColor('Facile')).toBe('green')
    expect(wrapper.vm.getColor('Intermédiaire')).toBe('blue')
    expect(wrapper.vm.getColor('Difficile')).toBe('orange')
    expect(wrapper.vm.getColor('Très difficile')).toBe('red')
    expect(wrapper.vm.getColor('Inconnue')).toBe('black')
  })

  test('Suite à un changement de trail, si le trail est null, ne fait rien', async () => {
    const mocks = {
      $store: createSimpleStore(null),
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Map, {
      mocks: mocks
    })
    const spy = jest.spyOn(wrapper.vm, 'loadPolyline')
    wrapper.vm.$options.watch.trail.call(wrapper.vm)
    expect(spy).toBeCalledTimes(0)
  })

  test('Suite à un changement de trail, si le trail est non null, load les polylines', async () => {
    const mocks = {
      $store: createSimpleStore(firstTrail),
      $bvToast: {
        toast: () => jest.fn()
      }
    }

    const wrapper = shallowMount(Map, {
      mocks: mocks
    })

    wrapper.vm.setNewFocus = jest.fn()
    segmentsService.getSegmentById.mockResolvedValue(firstSegment)

    const spy = jest.spyOn( wrapper.vm, 'loadPolyline')
    await wrapper.vm.$options.watch.trail.call(wrapper.vm)
    expect(spy).toBeCalledTimes(1)
  })

  test('Suite à un changement de trail, si le trail provoque une erreur, affiche un msg', async () => {
    const toastSpy = jest.fn()
    const mocks = {
      $store: createSimpleStore(firstTrail),
      $bvToast: {
        toast: param => toastSpy(param)
      }
    }

    const wrapper = shallowMount(Map, {
      mocks: mocks
    })

    wrapper.vm.setNewFocus = jest.fn()
    segmentsService.getSegmentById.mockImplementation(() => {
      throw new Error()
    })
    
    const spy = jest.spyOn(wrapper.vm, 'loadPolyline')
    await wrapper.vm.$options.watch.trail.call(wrapper.vm)
    flushPromises()
    expect(spy).toBeCalledTimes(1)
    expect(toastSpy).toHaveBeenCalledWith(ui.Map.CANT_LOAD_TRAIL)
  })
})  

function createSimpleStore (trail) {
  return {
    state: {
      map: {
        selectedTrail: trail
      }
    }
  }
}
