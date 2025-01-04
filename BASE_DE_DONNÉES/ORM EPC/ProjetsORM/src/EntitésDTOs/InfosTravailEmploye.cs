using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.EntiteDTOs
{
    public class InfosTravailEmploye
    {
        public short NoEmploye;
        public string Nom;
        public string Prenom;
        public int NbEmployesSupervises;
        public int NbProjetsEnCoursGeres;
        public int NbProjetsEnCoursContactClient;

        public static bool operator ==(InfosTravailEmploye statsA, InfosTravailEmploye statsB)
        {
            return statsA.NoEmploye == statsB.NoEmploye
                && statsA.Nom == statsB.Nom
                && statsA.Prenom == statsB.Prenom
                && statsA.NbEmployesSupervises == statsB.NbEmployesSupervises
                && statsA.NbProjetsEnCoursGeres == statsB.NbProjetsEnCoursGeres
                && statsA.NbProjetsEnCoursContactClient == statsB.NbProjetsEnCoursContactClient;
        }
        public static bool operator !=(InfosTravailEmploye statsA, InfosTravailEmploye statsB)
        {
            return !(statsA == statsB);
        }
        public override bool Equals(object o)
        {
            InfosTravailEmploye infoEmployeTravail = (InfosTravailEmploye)o;
            return this == infoEmployeTravail;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NoEmploye, Nom, Prenom, NbEmployesSupervises, NbProjetsEnCoursGeres, NbProjetsEnCoursContactClient);
        }
    }
}
