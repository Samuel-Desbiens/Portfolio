import axios from 'axios'
import {parksJsonFake} from '@/../tests/data/parksJsonFake'
import {trailsJsonFake} from '@/../tests/data/trailsJsonFake'
import {parksService} from '@/services/parksService'
import MockAdapter from 'axios-mock-adapter'

//jest.mock('axios')
var mockAxios = new MockAdapter(axios)
const API = process.env.VUE_APP_API
let parks
let trails

beforeEach(() => {
    parks = [...parksJsonFake]
    trails = [...trailsJsonFake]
    mockAxios.reset()
})

describe("parksService.js Test",() => 
{
    test("GetParks doit retourner tout les parks du fichier Json", async () =>{
        mockAxios.onGet(`${API}/api/parks`).reply(200, parks)
        const response = await parksService.getParks()
        expect(response[0]).toStrictEqual(parks[0])
        expect(response[1]).toStrictEqual(parks[1])
        expect(response[2]).toStrictEqual(parks[2])
    }),
    test("getTrailsByParkId doit retourner les trails appartenant au park avec son id" , async () => {
        const trailOf15 = trails[3];
        mockAxios.onGet(`${API}/api/parks/15/trails`).reply(200,trailOf15)
        const response = await parksService.getTrailsByParkId(15)
        expect(response).toStrictEqual(trails[3])
    })
})