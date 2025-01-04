import axios from 'axios'
import {segmentsJsonFake} from '@/../tests/data/segmentsJsonFake'
import {segmentsService} from '@/services/segmentsService'
import MockAdapter from 'axios-mock-adapter'

//jest.mock('axios')
var mockAxios = new MockAdapter(axios)
const API = process.env.VUE_APP_API
let segments

beforeEach(() => {
    segments = [...segmentsJsonFake]
    mockAxios.reset()
})

describe("segmentsService.js Test",() => 
{
    test("getSegmentById doit retourner un segment selon l'id donner", async () => {
        const segment = segments[0];
        mockAxios.onGet(`${API}/api/segments/1151/`).reply(200, segment)
        const response = await segmentsService.getSegmentById(1151)
        expect(response).toStrictEqual(segment)
    })
})