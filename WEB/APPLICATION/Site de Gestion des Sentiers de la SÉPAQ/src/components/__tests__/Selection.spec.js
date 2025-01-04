import Selection from '@/components/Selection.vue'
import { shallowMount } from '@vue/test-utils'
import flushPromises from 'flush-promises'
import { parksJsonFake } from '@/../tests/data/parksJsonFake'
import { trailsJsonFake } from '@/../tests/data/trailsJsonFake'
import { ui } from '@/externalization/uiTextPlugin.js'
import { likesJsonFake } from '@/../tests/data/likesJsonFake'

let likes
let parks
let firstParks
let trails
let firstTrail

const ANY_NAME = 'bob'

beforeEach(() => {
  likes = [...likesJsonFake]
  parks = [...parksJsonFake]
  firstParks = { ...parks[0] }
  trails = [...trailsJsonFake]
  firstTrail = {...trails[0] }
})

describe('Selection.vue', () => {
  test('Doit faire toutes les actions desirer au created quand connecte.', async () => {
    const mocks = {
      $store: createSimpleStore(true),
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    // msg
    const name = wrapper.find('#userName')
    expect(name.text()).toContain(ANY_NAME)
    // select park
    const optionValue = wrapper
    .find('#parksSelect')
    .findAll('option')
    .wrappers.map(o => o.element.textContent)
    expect(optionValue).toContain(parks[0].name)
    expect(optionValue).toContain(parks[1].name)
    expect(optionValue).toContain(parks[2].name)
    // select trail
    expect(wrapper.find('#trailsSelect').attributes().disabled).toBe('disabled')
    // trail info
    const trailInfo = wrapper.find('#trailInfo')
    expect(trailInfo.exists()).toBeFalsy()
  })

  test('Doit faire toutes les actions desirer au created quand non connecte.', async () => {
    const mocks = {
      $store: createSimpleStore(false),
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    // msg
    const name = wrapper.find('#userName')
    expect(name.exists()).toBeFalsy()
  })

  test('Doit afficher un msg et bloque des selects quand une erreur survient au load des parks.', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
        onError: false,
        name: ANY_NAME
      },
      map: {
        parks: parks,
        trails: trails
      }
    },
    getters: {
      'authentication/isLoggedIn': true
    },
    dispatch(txt) { if (txt === 'map/loadParks') throw new Error()}
    }
    const toastSpy = jest.fn()
    const mocks = {
      $store: store,
      $bvToast: {
        toast: param => toastSpy(param)
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    expect(toastSpy).toBeCalledWith(ui.Selection.IMPOSSIBlE_LOADING('les parks'))
    expect(wrapper.find('#parksSelect').attributes().disabled).toBe('disabled') 
    expect(wrapper.find('#trailsSelect').attributes().disabled).toBe('disabled') 
    expect(wrapper.find('#trailInfo').exists()).toBeFalsy()
  })

  test('Doit afficher un msg quand est connecte mes une erreur arrive quand on veut les info.', async () => {
    let store = createSimpleStore(true)
    const errorGetProfileInfo = jest.fn().mockImplementation(() => {
      throw new Error()
    })
    store.state.profile.onError = true
    store.getters['authentication/isLoggedIn'] = errorGetProfileInfo
    const toastSpy = jest.fn()
    const mocks = {
      $store: store,
      $bvToast: {
        toast: param => toastSpy(param)
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    expect(toastSpy).toBeCalledWith(ui.Selection.CANT_LOAD_USER)
    const optionValue = wrapper
    .find('#parksSelect')
    .findAll('option')
    .wrappers.map(o => o.element.textContent)
    expect(optionValue).toContain(parks[0].name)
    expect(optionValue).toContain(parks[1].name)
    expect(optionValue).toContain(parks[2].name)
    expect(wrapper.find('#trailsSelect').attributes().disabled).toBe('disabled') 
    expect(wrapper.find('#trailInfo').exists()).toBeFalsy()
  })

  test('Doit loader les trails apres avoir select un park et affiche les infos.', async () => {
    const dispatch = jest.fn()
    let store = createListnerDispatchStore(true, dispatch)
    const mocks = {
      $store: store,
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    const oElement = wrapper.find('#parksSelect').findAll('option')

    oElement.at(0).setSelected()
    await flushPromises()

    expect(dispatch).toBeCalledWith('map/loadTrails', firstParks.id)
    const optionValue = wrapper
    .find('#trailsSelect')
    .findAll('option')
    .wrappers.map(o => o.element.textContent)
    expect(optionValue).toContain(trails[0].name)
    expect(optionValue).toContain(trails[1].name)
    expect(optionValue).toContain(trails[2].name)
  })

  test('Affiche un msg si erreur lors de loader les trails apres avoir select un park', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails
        }
      },
      getters: {
        'authentication/isLoggedIn': true
      },
      dispatch(txt) { if (txt === 'map/loadTrails') throw new Error()}
    }
    const toatSpy = jest.fn()
    const mocks = {
      $store: store,
      $bvToast: {
        toast: param => toatSpy(param)
      }
    }
    const wrapper = shallowMount(Selection, {
        mocks: mocks
    })
    await flushPromises()
    const oElement = wrapper.find('#parksSelect').findAll('option')

    oElement.at(0).setSelected()
    await flushPromises()

    expect(toatSpy).toBeCalledWith(ui.Selection.IMPOSSIBlE_LOADING('les sentiers pour le park choisi'))
  })

  test('Update les likes apres change de selectedTrail, et affiche les infos.', async () => {
    const store = createSimpleStore(true)
    const mocks = {
      $store: store,
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        stubs: { "b-col": true, "b-row": true, "b-container": true },
        mocks: mocks
    })
    const spyUpdate = jest.spyOn(wrapper.vm, 'updateLikes')
    await flushPromises()
    
    const parkElement = wrapper.find('#parksSelect').findAll('option')
    await parkElement.at(0).setSelected()
    await flushPromises()

    const trailElement = wrapper.find('#trailsSelect').findAll('option')
    await trailElement.at(0).setSelected()
    await flushPromises()

    expect(spyUpdate).toHaveBeenCalled()
    expect(wrapper.find('#trailName').element.textContent).toContain(firstTrail.name)
    expect(wrapper.find('#parkName').element.textContent).toContain(firstTrail.park)
    expect(wrapper.find('#nbLikes').element.textContent).toBe(likes.length + "")
  })

  test('Peut like le selectedTrail si pas like, et fait un update par la suite.', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails,
          likes: likes
        }
      },
      getters: {
        'authentication/isLoggedIn': true
      },
      dispatch(txt) { if (txt === 'profile/hasLiked') return false}
    }
    const mocks = {
      $store: store,
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        stubs: { "b-col": true, "b-row": true, "b-container": true },
        mocks: mocks
    })
    const spyUpdate = jest.spyOn(wrapper.vm, 'updateLikes')
    const spyLike = jest.spyOn(wrapper.vm, 'like')
    
    const parkElement = wrapper.find('#parksSelect').findAll('option')
    await parkElement.at(0).setSelected()
    await flushPromises()

    const trailElement = wrapper.find('#trailsSelect').findAll('option')
    await trailElement.at(0).setSelected()
    await flushPromises()

    expect(wrapper.find('#liked').exists()).toBeFalsy()
    expect(wrapper.find('#disliked').exists()).toBeTruthy()

    await wrapper.find('#disliked').trigger('click')
    await flushPromises()

    //2 fois: 1 au select, et l'autre au click
    expect(spyUpdate).toBeCalledTimes(2)
    expect(spyLike).toBeCalledTimes(1)
    expect(wrapper.find('#trailName').element.textContent).toContain(firstTrail.name)
    expect(wrapper.find('#parkName').element.textContent).toContain(firstTrail.park)
    //normalement change après le update
    expect(wrapper.find('#nbLikes').element.textContent).toBe(likes.length + "")
  })

  test('Peut dislike si like, et fait un update par la suite .', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails,
          likes: likes
        }
      },
      getters: {
        'authentication/isLoggedIn': true
      },
      dispatch(txt) { if (txt === 'profile/hasLiked') return true}
    }
    const mocks = {
      $store: store,
      $bvToast: {
        toast: () => jest.fn()
      }
    }
    const wrapper = shallowMount(Selection, {
        stubs: { "b-col": true, "b-row": true, "b-container": true },
        mocks: mocks
    })
    const spyUpdate = jest.spyOn(wrapper.vm, 'updateLikes')
    const spyDislike = jest.spyOn(wrapper.vm, 'unlike')
    
    const parkElement = wrapper.find('#parksSelect').findAll('option')
    await parkElement.at(0).setSelected()
    await flushPromises()

    const trailElement = wrapper.find('#trailsSelect').findAll('option')
    await trailElement.at(0).setSelected()
    await flushPromises()

    expect(wrapper.find('#disliked').exists()).toBeFalsy()
    expect(wrapper.find('#liked').exists()).toBeTruthy()

    await wrapper.find('#liked').trigger('click')
    await flushPromises()

    //2 fois: 1 au select, et l'autre au click
    expect(spyUpdate).toBeCalledTimes(2)
    expect(spyDislike).toBeCalledTimes(1)
    expect(wrapper.find('#trailName').element.textContent).toContain(firstTrail.name)
    expect(wrapper.find('#parkName').element.textContent).toContain(firstTrail.park)
    //normalement change après le update
    expect(wrapper.find('#nbLikes').element.textContent).toBe(likes.length + "")
  })
  test('Si veut like et qu une erreur survient, affiche un msg.', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails,
          likes: likes
        }
      },
      getters: {
        'authentication/isLoggedIn': true
      },
      dispatch(txt) { 
        if (txt === 'profile/hasLiked') return false
        if (txt === 'profile/likeTrail') throw new Error()
      }
    }
    const toastSpy = jest.fn()
    const mocks = {
      $store: store,
      $bvToast: {
        toast: param => toastSpy(param)
      }
    }
    const wrapper = shallowMount(Selection, {
      stubs: { "b-col": true, "b-row": true, "b-container": true },
      mocks: mocks
    })
    const spyUpdate = jest.spyOn(wrapper.vm, 'updateLikes')
    const spyLike = jest.spyOn(wrapper.vm, 'like')
  
    const parkElement = wrapper.find('#parksSelect').findAll('option')
    await parkElement.at(0).setSelected()
    await flushPromises()

    const trailElement = wrapper.find('#trailsSelect').findAll('option')
    await trailElement.at(0).setSelected()
    await flushPromises()

    expect(wrapper.find('#liked').exists()).toBeFalsy()
    expect(wrapper.find('#disliked').exists()).toBeTruthy()

    await wrapper.find('#disliked').trigger('click')
    await flushPromises()

    expect(toastSpy).toBeCalledWith(ui.Selection.IMPOSSIBLE_ACTION('like'))
    //2 fois: 1 au select, et l'autre au click
    expect(spyUpdate).toBeCalledTimes(2)
    expect(spyLike).toBeCalledTimes(1)
    expect(wrapper.find('#trailName').element.textContent).toContain(firstTrail.name)
    expect(wrapper.find('#parkName').element.textContent).toContain(firstTrail.park)
    //normalement change après le update
    expect(wrapper.find('#nbLikes').element.textContent).toBe(likes.length + "")
  })

  test('Si veut dislike et qu une erreur survient affiche un msg .', async () => {
    //ne fonctionne pas si fait par fonction
    const store = {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails,
          likes: likes
        }
      },
      getters: {
        'authentication/isLoggedIn': true
      },
      dispatch(txt) { 
        if (txt === 'profile/hasLiked') return true
        if (txt === 'profile/dislikeTrail') throw new Error()
      }
    }
    const toastSpy = jest.fn()
    const mocks = {
      $store: store,
      $bvToast: {
        toast: param => toastSpy(param)
      }
    }
    const wrapper = shallowMount(Selection, {
      stubs: { "b-col": true, "b-row": true, "b-container": true },
      mocks: mocks
    })
    const spyUpdate = jest.spyOn(wrapper.vm, 'updateLikes')
    const spyLike = jest.spyOn(wrapper.vm, 'unlike')
  
    const parkElement = wrapper.find('#parksSelect').findAll('option')
    await parkElement.at(0).setSelected()
    await flushPromises()

    const trailElement = wrapper.find('#trailsSelect').findAll('option')
    await trailElement.at(0).setSelected()
    await flushPromises()

    expect(wrapper.find('#disliked').exists()).toBeFalsy()
    expect(wrapper.find('#liked').exists()).toBeTruthy()

    await wrapper.find('#liked').trigger('click')
    await flushPromises()

    expect(toastSpy).toBeCalledWith(ui.Selection.IMPOSSIBLE_ACTION('retirer un like'))
    //2 fois: 1 au select, et l'autre au click
    expect(spyUpdate).toBeCalledTimes(2)
    expect(spyLike).toBeCalledTimes(1)
    expect(wrapper.find('#trailName').element.textContent).toContain(firstTrail.name)
    expect(wrapper.find('#parkName').element.textContent).toContain(firstTrail.park)
    //normalement change après le update
    expect(wrapper.find('#nbLikes').element.textContent).toBe(likes.length + "")
  })
})

function createSimpleStore (isConnected) {
  return {
    state: {
      profile: {
        onError: false,
        name: ANY_NAME
      },
      map: {
        parks: parks,
        trails: trails,
        likes: likes
      }
    },
    getters: {
      'authentication/isLoggedIn': isConnected
    },
    dispatch: jest.fn()
  }
}

function createListnerDispatchStore (isConnected, dispatch) {
    return {
      state: {
        profile: {
          onError: false,
          name: ANY_NAME
        },
        map: {
          parks: parks,
          trails: trails,
          likes: likes
        }
      },
      getters: {
        'authentication/isLoggedIn': isConnected
      },
      dispatch: dispatch
    }
  }