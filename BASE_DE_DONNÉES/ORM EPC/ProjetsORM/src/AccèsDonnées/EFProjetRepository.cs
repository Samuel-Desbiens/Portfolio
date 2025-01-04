using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.AccesDonnees
{
    class EFProjetRepository
    {
        #region Champs
        private ProjetsORMContexte contexte;
        #endregion

        #region Constructeur
        public EFProjetRepository (ProjetsORMContexte contexte)
        {
            this.contexte = contexte;
        }
        #endregion

        #region Méthodes
        public void AjouterProjet(Projet projet)
        {
            this.contexte.Projets.Add(projet);
            this.contexte.SaveChanges();
        }
        #endregion
    }
}
