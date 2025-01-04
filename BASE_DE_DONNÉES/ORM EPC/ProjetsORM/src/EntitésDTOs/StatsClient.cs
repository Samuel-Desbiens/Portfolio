using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.EntiteDTOs
{
	public class StatsClient
    {
        public string NomClient;
        public int NombreProjets;
        public decimal? BudgetTotal;
        public decimal? BudgetMoyen;
        public decimal? BudgetMax;

        public static bool operator ==(StatsClient statsA, StatsClient statsB)
        {
            return statsA.NomClient == statsB.NomClient
                && statsA.NombreProjets == statsB.NombreProjets
                && statsA.BudgetTotal == statsB.BudgetTotal
                && statsA.BudgetMoyen == statsB.BudgetMoyen
                && statsA.BudgetMax == statsB.BudgetMax;
        }
        public static bool operator !=(StatsClient statsA, StatsClient statsB)
        {
            return !(statsA == statsB);
        }
        public override bool Equals(object o)
        {
            StatsClient statsClient = (StatsClient)o;
            return this == statsClient;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NomClient, NombreProjets, BudgetTotal, BudgetMoyen, BudgetMax);
        }
    }
}
