import axios from 'axios'
import { usersJsonFake } from '@/../tests/data/usersJsonFake'
import { userService } from '@/services/userService'
import MockAdapter from 'axios-mock-adapter'
import interceptor from '@/shared/requestInterceptor'

jest.mock('@/shared/requestInterceptor')
let users

beforeEach(() => {
    users = [...usersJsonFake]
})

describe('userService.js Tests', () =>{
    test('getUserById retourne le user avec son id', async () =>{
        let user = users[0]
        const EXPECTED_RESPONSE = {
            data: user
        }
        interceptor.get.mockResolvedValue(EXPECTED_RESPONSE)
        //mockAxios.onGet(`${API}/users/0`).reply(200, user)
        const response = await userService.getUserById(0)
        expect(response).toStrictEqual(user)
    })
    test('likeTrail retourne le code 200', async () =>{
        const EXPECTED_RESPONSE = {
            data: 200
        }
        interceptor.post.mockResolvedValue(EXPECTED_RESPONSE)
        const response = await userService.likeTrail(1,1)
        expect(response).toEqual(200)
    })
    test('dislikeTrail retourne le code 200', async () =>{
        const EXPECTED_RESPONSE = {
            data: 200
        }
        interceptor.delete.mockResolvedValue(EXPECTED_RESPONSE)
        const response = await userService.dislikeTrail(1)
        expect(response).toEqual(200)
    })
})