import { shallowMount, RouterLinkStub } from '@vue/test-utils'
import { resetAllWhenMocks } from 'jest-when'
import NavigationBar from '@/components/NavigationBar.vue'

describe('NavigationBar.vue', () => {
  test('Doit contenir un lien sur la page Home.', async () => {
    const isLoggedIn = false
    const store = {
      getters: {
        'authentication/isLoggedIn': isLoggedIn
      }
    }
    const mocks = {
      $store: store
    }
    const wrapper = shallowMount(NavigationBar, {
      stubs: {
        RouterLink: RouterLinkStub
      },
      mocks: mocks
    })
    const routerLinks = wrapper
      .findAllComponents(RouterLinkStub)
      .wrappers.map(routerLink => routerLink.props().to)

    expect(routerLinks).toContainEqual({
      name: 'Home'
    })
  })
  test('Doit contenir un lien sur la page Login si non connecte, donc n affiche pas lougout.', async () => {
    const isLoggedIn = false
    const store = {
      getters: {
        'authentication/isLoggedIn': isLoggedIn
      }
    }
    const mocks = {
      $store: store
    }
    const wrapper = shallowMount(NavigationBar, {
      stubs: {
        RouterLink: RouterLinkStub
      },
      mocks: mocks
    })
    const routerLinks = wrapper
      .findAllComponents(RouterLinkStub)
      .wrappers.map(routerLink => routerLink.props().to)

    expect(routerLinks).toContainEqual({
      name: 'Login'
    })
    const logoutLink = wrapper.find('#logoutLink')
    expect(logoutLink.exists()).toBe(false)
  })

  test('Doit pas contenir un lien sur la page Login si connecte, donc affiche logout a la place.', async () => {
    const isLoggedIn = true
    const store = {
      getters: {
        'authentication/isLoggedIn': isLoggedIn
      }
    }
    const mocks = {
      $store: store
    }
    const wrapper = shallowMount(NavigationBar, {
      stubs: {
        RouterLink: RouterLinkStub
      },
      mocks: mocks
    })
    const routerLinks = wrapper
      .findAllComponents(RouterLinkStub)
      .wrappers.map(routerLink => routerLink.props().to)

    expect(routerLinks).not.toContainEqual({
      name: 'Login'
    })
    const logoutLink = wrapper.find('#logoutLink')
    expect(logoutLink.exists()).toBe(true)
  })

  test('Logout fait les actions desire.', async () => {
    const isLoggedIn = true
    const dispatchSpy = jest.fn()
    const store = {
      getters: {
        'authentication/isLoggedIn': isLoggedIn
      },
      dispatch: dispatchSpy
    }
    const pushSpy = jest.fn()
    const router = {
      push: param => pushSpy(param)
    }
    const mocks = {
      $store: store,
      $router: router
    }
    const wrapper = shallowMount(NavigationBar, {
      stubs: {
        RouterLink: RouterLinkStub
      },
      mocks: mocks
    })
    const logoutLink = wrapper.find('#logoutLink')
    await logoutLink.trigger('click')
    expect(dispatchSpy).toBeCalledWith('authentication/logout')
    expect(pushSpy).toBeCalledWith({ name: 'Login' })
  })
})
