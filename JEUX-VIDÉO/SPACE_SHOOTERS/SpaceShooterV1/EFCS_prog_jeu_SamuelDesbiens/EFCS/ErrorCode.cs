using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCS
{
  // Codes d'erreur utilisés lors du chargement de la StringTable
  public enum ErrorCode
  {
    // Chargement du fichier de langue OK
    OK,
    // Format de fichier totalement invalide
    BAD_FILE_FORMAT, 
    // Erreur s'il manque une clé ou une valeur de langue.
    MISSING_FIELD,
  }
}
