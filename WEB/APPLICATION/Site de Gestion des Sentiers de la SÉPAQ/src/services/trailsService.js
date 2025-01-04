import * as axios from 'axios'
import { API } from '@/shared/config.js'

async function getNbLikesByTrailId (id) {
  const response = await axios.get(`${API}/api/trails/` + id + '/likes')
  return response.data
}

export const trailsService = {
  getNbLikesByTrailId
}
