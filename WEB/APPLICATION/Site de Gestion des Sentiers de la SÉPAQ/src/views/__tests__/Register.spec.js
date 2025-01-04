import { shallowMount,RouterLinkStub } from "@vue/test-utils";
import Register from '@/views/Register.vue'
import flushPromises from "flush-promises";

jest.mock('@/services/authService')
jest.mock('@/shared/tokenHelper')

const store = {
    state: {
        authentication:{
            OnError: false
        }
    },
    dispatch: jest.fn(),
    commit: jest.fn()
}

beforeEach(() => {
    jest.clearAllMocks()
})

describe('Register.vue Tests', () => {
    test('Par defaut, doit afficher le email, mot de passe et nom est vide', async () => {
        const wrapper = await RegisterShallowMount()

        expect(wrapper.vm.email).toBe('')
        expect(wrapper.vm.password).toBe('')
        expect(wrapper.vm.name).toBe('')
        
    })
    test('Si lusager clique sur le bouton créer le compte doit appeler la méthode register', async () => {
        const wrapper = await RegisterShallowMount()
        const spyRegister = jest.spyOn(wrapper.vm, 'register')
        await wrapper.find('#register').trigger('click')
        await flushPromises()

        expect(spyRegister).toHaveBeenCalled()  
    })
    test('Si lusager clique sur le bouton créer le compte avec de mauvaise information doit afficher le message derreur', async () => {
        const store2 = {
            state: {
                authentication:{
                    authServiceError: 'allo'
                }
            },
            dispatch: jest.fn(),
            commit: jest.fn(),
            authServiceError: jest.fn()
        }
        const wrapper = shallowMount(Register, {
            mocks: {
                $store:store2,
                $router: {
                    push: param => jest.fn(param)
                }
            },
            stubs: {
                RouterLink:RouterLinkStub
            }
        })
        await wrapper.find('#register').trigger('click')
        await flushPromises()

        expect(wrapper.find('#errorMsg').isVisible()).toBe(true)
        expect(wrapper.find('#errorMsg').text()).toBe('allo')
    })
})

async function RegisterShallowMount() {
    const pushToLogin = jest.fn()
    const wrapper = shallowMount(Register, {
        mocks: {
            $store:store,
            $router: {
                push: param => pushToLogin(param)
            }
        },
        stubs: {
            RouterLink:RouterLinkStub
        },
        
    })

    await flushPromises()
    return wrapper
}