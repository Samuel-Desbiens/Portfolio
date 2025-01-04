import { shallowMount,RouterLinkStub } from "@vue/test-utils";
import Login from '@/views/Login.vue'
import flushPromises from "flush-promises";

jest.mock('@/services/authService')
jest.mock('@/shared/tokenHelper')

const store = {
    state: {
        authentication:{
            OnError: false,
            authServiceError: 'allo'
        }
    },
    dispatch: jest.fn(),
    commit: jest.fn(),
    authServiceError: jest.fn()
}

beforeEach(() => {
    jest.clearAllMocks()
})

describe('Login.vue Tests', () => {
    test('Par defaut, doit afficher le email et mot de passe vide', async () => {
        const wrapper = await LoginShallowMount()

        expect(wrapper.vm.email).toBe('')
        expect(wrapper.vm.password).toBe('')
        
    })
    test('Si lusager clique sur le bouton connection doit appeler la méthode login', async () => {
        const wrapper = await LoginShallowMount()
        const spyLogin = jest.spyOn(wrapper.vm, 'login')
        await wrapper.find('#login').trigger('click')
        await flushPromises()

        expect(spyLogin).toHaveBeenCalled()  
    })
    test('le lien créer un compte doit lier vers la page Register', async () => {
        const wrapper = await LoginShallowMount()

        expect(wrapper.find('#toRegister').props().to).toStrictEqual({"name": "Register"})  
    })
    test('Si lusager clique sur le bouton connection avec de mauvaise information doit afficher le méssage derreur', async () => {
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
        const wrapper = shallowMount(Login, {
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
        await wrapper.find('#login').trigger('click')
        await flushPromises()

        expect(wrapper.find('#errorMsg').isVisible()).toBe(true)
        expect(wrapper.find('#errorMsg').text()).toBe('allo')
    })

})

async function LoginShallowMount() {
    const pushToHome = jest.fn()
    const wrapper = shallowMount(Login, {
        mocks: {
            $store:store,
            $router: {
                push: param => pushToHome(param)
            }
        },
        stubs: {
            RouterLink:RouterLinkStub
        }
    })

    await flushPromises()
    return wrapper
}