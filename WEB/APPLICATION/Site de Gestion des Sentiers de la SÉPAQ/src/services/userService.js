import { API } from '@/shared/config'
import requestInterceptor from '@/shared/requestInterceptor'
import { parseAxiosErrorToAppError } from '@/shared/errorHelper'

async function getUserById (userId) {
  try {
    const response = await requestInterceptor.get(`${API}/users/${userId}`)
    return response.data
  } catch (error) {
    throw parseAxiosErrorToAppError(error)
  }
}

async function likeTrail (userId, trailId) {
  const response = await requestInterceptor.post(`${API}/api/likes`, {
    userId: userId,
    trailId: trailId
  })
  return response.data
}

async function dislikeTrail (likeId) {
  const response = await requestInterceptor.delete(`${API}/api/likes/${likeId}`)
  return response.data
}

export const userService = {
  getUserById,
  likeTrail,
  dislikeTrail
}
