using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.EntiteDTOs
{
    public class InfosProjetsEnCours
    {
        public string NomClient;
        public string NomProjet;
        public decimal? Budget;
        public string Gestionnaire;
        public string ContactClient;

        public static bool operator ==(InfosProjetsEnCours statsA, InfosProjetsEnCours statsB)
        {
            return statsA.NomClient == statsB.NomClient
                && statsA.NomProjet == statsB.NomProjet
                && statsA.Budget == statsB.Budget
                && statsA.Gestionnaire == statsB.Gestionnaire
                && statsA.ContactClient == statsB.ContactClient;
        }
        public static bool operator !=(InfosProjetsEnCours statsA, InfosProjetsEnCours statsB)
        {
            return !(statsA == statsB);
        }
        public override bool Equals(object o)
        {
            InfosProjetsEnCours infoProjet = (InfosProjetsEnCours)o;
            return this == infoProjet;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NomClient, NomProjet, Budget, Gestionnaire, ContactClient);
        }
    }
}
