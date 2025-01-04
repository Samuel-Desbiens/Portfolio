import axios from 'axios'
import {trailsJsonFake} from '@/../tests/data/trailsJsonFake'
import {likesJsonFake} from '@/../tests/data/likesJsonFake'
import {trailsService} from '@/services/trailsService'
import MockAdapter from 'axios-mock-adapter'

//jest.mock('axios')
var mockAxios = new MockAdapter(axios)
const API = process.env.VUE_APP_API
let trails
let likes

beforeEach(() => {
    trails = [...trailsJsonFake]
    likes = [...likesJsonFake]
    mockAxios.reset()
})

describe('trailService.js Tests', () => {
    test('getNbLikesByTrailId doit retourner le nombre de like mis par des users sur le trail spécifier par son ID',async () => {
        const LikesFor246 = likes[2]
        mockAxios.onGet(`${API}/api/trails/246/likes`).reply(200,LikesFor246)
        const response = await trailsService.getNbLikesByTrailId(246)
        expect (response).toEqual(LikesFor246)
    })
})