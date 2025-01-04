import axios from 'axios'
import { authService } from '@/services/authService'
import MockAdapter from 'axios-mock-adapter'

//jest.mock('axios')
var mockAxios = new MockAdapter(axios)
const API = process.env.VUE_APP_API

beforeEach(() => {
    mockAxios.reset()
})

describe('authService.js tests', () => {
    test("getToken doit retourner le token", async () => {
        const EXPECTED_RESPONSE = {
            accessToken: 'aaa'
        }
        const credential = {
            email: "email",
            password: "password"
        }
        mockAxios.onPost(`${API}/login`).reply(200, EXPECTED_RESPONSE)
        const response = await authService.getToken(credential)
        expect(response).toEqual(EXPECTED_RESPONSE.accessToken)
    })
    test("PostUser doit retourner code 200", async () =>{
        const user = {
            email:"email",
            password:"pass",
            nom:"nom"
        }
        mockAxios.onPost(`${API}/register`).reply(200)
        // non testable comme retourne rien
        const response = await authService.postUser(user)
    })
})