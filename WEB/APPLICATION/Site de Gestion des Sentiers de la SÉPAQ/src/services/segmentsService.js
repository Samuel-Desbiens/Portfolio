import * as axios from 'axios'
import { API } from '@/shared/config.js'

async function getSegmentById (id) {
  const response = await axios.get(`${API}/api/segments/` + id + '/')
  return response.data
}

export const segmentsService = {
  getSegmentById
}
