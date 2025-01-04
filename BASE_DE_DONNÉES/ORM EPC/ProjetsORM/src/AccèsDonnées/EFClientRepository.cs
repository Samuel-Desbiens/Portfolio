using ProjetsORM.Entites;
using ProjetsORM.EntiteDTOs;
using ProjetsORM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetsORM.AccesDonnees
{
    class EFClientRepository
    {
        #region Champs
        private ProjetsORMContexte contexte;
        #endregion

        #region Constructeur
        public EFClientRepository(ProjetsORMContexte contexte)
        {
            this.contexte = contexte;
        }
        #endregion

        #region Méthodes
        public void AjouterClient(Client client)
        {
            GetContexteClient().Add(client);
            this.contexte.SaveChanges();
        }

        public Client ObtenirClient(string NomClient)
        {
            ValiderClients(NomClient);
            return GetContexteClient().Find(NomClient);
        }

        public ICollection<Client> RechercherClientsPourUneVille(string nomVille)
        {
            return GetContexteClient().Where(client => client.Ville == nomVille).ToList();
        }

        public StatsClient ObtenirStatsPourUnClient(string NomClient)
        {
            ValiderClients(NomClient);
            ICollection<Projet> ContexteFind = GetContexteClient().Find(NomClient).Projets;
            return new StatsClient 
            { 
              NomClient = NomClient, 
              NombreProjets = ContexteFind.Count(),
              BudgetTotal = ContexteFind.Sum(e => e.Budget),
              BudgetMoyen = (decimal?)ContexteFind.Average(e => e.Budget),
              BudgetMax = ContexteFind.Max(e => e.Budget)
            };
        }

        public ICollection<StatsClient> CalculerStatsPourTousLesClients()
        {
            ICollection<StatsClient> result = GetContexteClient().Select(client => ObtenirStatsPourUnClient(client.NomClient)).ToList();
            return result;
        }

        public InfosProjetsEnCours ObtenirUnInfoProjetsEnCours(string NomProjet, string NomClient)
        {
            ValiderProjet(NomProjet, NomClient);
            Employe GestionnaireProjetFind = GetContexteProjet().Find(NomProjet, NomClient).Gestionnaire;
            Employe ContactProjetFind = GetContexteProjet().Find(NomProjet, NomClient).Contact;
            string NomContact = "";
            string NomGestionnaire = "(" + GestionnaireProjetFind.NoEmploye + ") "
                + GestionnaireProjetFind.Nom + " "
                + GestionnaireProjetFind.Prenom;

            if(GetContexteProjet().Find(NomProjet,NomClient).Contact != null)
            {
                NomContact = "(" + ContactProjetFind.NoEmploye + ") "
                + ContactProjetFind.Nom + " "
                + ContactProjetFind.Prenom;
            }

            return new InfosProjetsEnCours
            {
                NomClient = NomClient,
                NomProjet = NomProjet,
                Budget = GetContexteProjet().Find(NomProjet, NomClient).Budget,
                Gestionnaire = NomGestionnaire,
                 ContactClient = NomContact
        
            };
        }

        public ICollection<InfosProjetsEnCours> ObtenirInfoProjetsEnCoursPourUnClient(string NomClient)
        {
            ValiderClients(NomClient);
            ICollection<InfosProjetsEnCours> result = GetContexteProjet().Where(projet => projet.NomClient == NomClient && projet.DateDebut != null && projet.DateFin == null).Select(e => ObtenirUnInfoProjetsEnCours(e.NomProjet, e.NomClient)).ToList();
            return result;
        }



        private void ValiderClients(string NomClient)
        {
            if (GetContexteClient().Find(NomClient) == null) 
            { 
                throw new ArgumentException("Le client n'existe pas"); 
            }
        }

        private void ValiderProjet(string NomProjet, string NomClient)
        {
            if (GetContexteProjet().Find(NomProjet, NomClient) == null)
            {
                throw new ArgumentException("Le projet n'existe pas");
            }
        }

        private Microsoft.EntityFrameworkCore.DbSet<ProjetsORM.Entites.Client> GetContexteClient()
        {
            return this.contexte.Clients;
        }

        private Microsoft.EntityFrameworkCore.DbSet<ProjetsORM.Entites.Projet> GetContexteProjet()
        {
            return this.contexte.Projets;
        }
        #endregion

    }
}
