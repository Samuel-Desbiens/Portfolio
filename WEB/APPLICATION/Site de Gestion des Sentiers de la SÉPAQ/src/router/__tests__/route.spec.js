import { mount, createLocalVue } from '@vue/test-utils'
import { when, resetAllWhenMocks } from 'jest-when'
import App from '@/App.vue'
import VueRouter from 'vue-router'
import routes from '@/router/routes'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import Register from '@/views/Register.vue'
import PageNotFound from '@/views/PageNotFound'

const localVue = createLocalVue()
localVue.use(VueRouter)

jest.mock('@/views/Home.vue', () => ({
  render: () => ''
}))
jest.mock('@/views/Login.vue', () => ({
  render: () => ''
}))
jest.mock('@/views/Register.vue', () => ({
  render: () => ''
}))

let wrapper
let router
let store

beforeEach(() => {
  resetAllWhenMocks()
  store = createMockStore()
  router = new VueRouter({ routes, mode: 'abstract' })
  wrapper = mount(App, {
    localVue,
    router,
    mocks: {
      $store: store
    }
  })
})

describe('routes.js', () => {
  test('/home doit afficher la page douverture.', async () => {
    await router.push('/home')
    
    expect(wrapper.findComponent(Home).exists()).toBe(true)
  })

  test('/ doit afficher la page douverture.', async () => {
    await router.push('/')
    
    expect(wrapper.findComponent(Home).exists()).toBe(true)
  })

  test('/login doit afficher la page de login.', async () => {
    await router.push('/login')
    
    expect(wrapper.findComponent(Login).exists()).toBe(true)
  })

  test('/register doit afficher la page de register.', async () => {
    await router.push('/register')
    
    expect(wrapper.findComponent(Register).exists()).toBe(true)
  })

  test('/no-idea doit afficher la page not found.', async () => {
    await router.push('/no-idea')
    
    expect(wrapper.findComponent(PageNotFound).exists()).toBe(true)
  })
})

function createMockStore () {
  const getIsLoggedIn = jest.fn()
  when(getIsLoggedIn).mockReturnValue(false)
  const store = {
    state: {
      posts: {
          onError: false
      }
    },
      getters: {
        'authentication/isLoggedIn': getIsLoggedIn
      },
      dispatch: jest.fn()
    }
    return store
}

