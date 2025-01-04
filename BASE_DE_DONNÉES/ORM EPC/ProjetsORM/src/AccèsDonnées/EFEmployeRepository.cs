using ProjetsORM.EntiteDTOs;
using ProjetsORM.Entites;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.AccesDonnees
{
    class EFEmployeRepository
    {
        #region Champs 
        private ProjetsORMContexte contexte;
        #endregion

        #region Constructeur
        public EFEmployeRepository(ProjetsORMContexte contexte)
        {
            this.contexte = contexte;
        }
        #endregion


        #region Méthodes
        public void AjouterEmploye(Employe employe)
        {
            GetContexteEmploye().Add(employe);
            contexte.SaveChanges();
        }

        public Employe ObtenirEmploye(short noEmploye)
        {
            ValiderEmployer(noEmploye);
            return GetContexteEmploye().Find(noEmploye);
        }

        public ICollection<Employe> RechercherTousLesSuperviseurs()
        {
            return GetContexteEmploye().Where(Employe => Employe.Supervises.Count != 0).ToList();
        }

        public ICollection<Employe> RechercherEmployesSansSuperviseur()
        {
            return GetContexteEmploye().Where(Employe => Employe.NoSuperviseur == null).ToList();
        }

        public InfosTravailEmploye ObtenirInformationEmploye(short NoEmploye)
        {
            ValiderEmployer(NoEmploye);
            Employe Employe = GetContexteEmploye().Find(NoEmploye);
            return new InfosTravailEmploye
            {
                NoEmploye = NoEmploye,
                Nom = Employe.Nom,
                Prenom = Employe.Prenom,
                NbEmployesSupervises = Employe.Supervises.Count(),
                NbProjetsEnCoursGeres = Employe.ProjetsGerer.Where(e => (e.DateDebut != null && e.DateFin == null)).Count(),
                NbProjetsEnCoursContactClient = Employe.ProjetsContact.Where(e => (e.DateDebut != null && e.DateFin == null)).Count()
            };
        }
        private void ValiderEmployer(short NoEmploye)
        {
            if (GetContexteEmploye().Find(NoEmploye) == null)
            {
                throw new ArgumentException("L'employe n'existe pas");
            }
        }

        private Microsoft.EntityFrameworkCore.DbSet<ProjetsORM.Entites.Employe> GetContexteEmploye()
        {
            return this.contexte.Employes;
        }

        #endregion
    }
}
