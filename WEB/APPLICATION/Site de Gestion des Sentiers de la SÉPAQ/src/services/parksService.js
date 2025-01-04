import * as axios from 'axios'
import { API } from '@/shared/config.js'

async function getParks () {
  const response = await axios.get(`${API}/api/parks`)
  return response.data
}

async function getTrailsByParkId (id) {
  const response = await axios.get(`${API}/api/parks/` + id + '/trails')
  return response.data
}

export const parksService = {
  getParks,
  getTrailsByParkId
}
