function decrypt (token) {
  var base64Payload = token.split('.')[1]
  var payload = Buffer.from(base64Payload, 'base64')
  return JSON.parse(payload.toString())
}

function getExpiration (token) {
  return decrypt(token).exp
}

function getUserId (token) {
  return decrypt(token).sub
}

function isAlive (token) {
  const exp = decrypt(token).exp
  if (Date.now() >= exp * 1000) {
    return false
  }
  return true
}

export default {
  decrypt,
  isAlive,
  getUserId,
  getExpiration
}
