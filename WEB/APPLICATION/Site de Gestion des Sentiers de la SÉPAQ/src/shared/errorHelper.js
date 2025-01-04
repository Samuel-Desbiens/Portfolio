import AppError from '@/shared/AppError'

export function parseAxiosErrorToAppError (error) {
  if (isNetworkError(error)) {
    const appError = new AppError(
      'Erreur avec le réseau. Impossible de communiquer avec le serveur.'
    )
    return appError
  } else {
    // Les messsages d'erreurs générés par l'api rest local (json-server-auth) sont en anglais (exemple: Incorrect password, Cannot find user, etc.)
    // Pour corriger ce problème, il faudrait coder nous même la gestion des autorisations au lieu d'utiliser json-server-auth.
    // Dans le cadre du cours, on peut considérer que l'api rest en production générerait les erreurs en français...
    const appError = new AppError(
      error.response.data,
      error.response.statusText,
      error.response.status
    )
    return appError
  }
}

function isNetworkError (error) {
  // https://github.com/axios/axios/issues/383
  return !!error.isAxiosError && !error.response // !! Converts Object to boolean. If it was falsey (e.g. 0, null, undefined, etc.), it will be false, otherwise, true.
}
