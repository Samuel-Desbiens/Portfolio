using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using ProjetsORM.Entites;
using ProjetsORM.EntiteDTOs;
using ProjetsORM.Persistence;

namespace ProjetsORM.AccesDonnees
{
    public class EFClientRepositoryTest
    {
        private EFClientRepository RepoClients;
        private EFProjetRepository RepoProjets;
        private EFEmployeRepository RepoEmploye;
        private void SetUp()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "TestClient_db");
            var contexte = new ProjetsORMContexte(builder.Options);
            RepoClients = new EFClientRepository(contexte);
        }
        private void SetUpClientProjet()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "TestClientProjet_db");
            var contexte = new ProjetsORMContexte(builder.Options);
            RepoClients = new EFClientRepository(contexte);
            RepoProjets = new EFProjetRepository(contexte);
        }

        private void SetUpClientProjetEmploye()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "TestClientProjet_db");
            var contexte = new ProjetsORMContexte(builder.Options);
            RepoClients = new EFClientRepository(contexte);
            RepoProjets = new EFProjetRepository(contexte);
            RepoEmploye = new EFEmployeRepository(contexte);
        }
        // ObtenirClient Tests
        [Fact]
        public void ObtenirClientQuandUnClientExiste()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Client Received = RepoClients.ObtenirClient(c1.NomClient);

            Assert.Same(c1, Received);
        }

        [Fact]
        public void ObtenirUnClientParmisPlusieurs()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Client Received = RepoClients.ObtenirClient(c2.NomClient);

            Assert.NotSame(c1, Received);
            Assert.Same(c2, Received);
            Assert.NotSame(c3, Received);
        }
        [Fact]
        public void ObtenirClientQuandClientNExistePasRetourneException()
        {
            SetUp();

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirClient("ClairementExistePas"));
        }

        [Fact]
        public void ObtenirUnClientQuiNExistePasParmisPlusieursRetourneException()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirClient("c4"));
        }
        //RechercherClientsPourUneVille Tests

        [Fact]
        public void ObtenirClientQuandUnClientExisteParSaVille()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            ICollection<Client> Result = new List<Client> { c1 };

            ICollection<Client> Received = RepoClients.RechercherClientsPourUneVille(c1.Ville);

            
            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirClientParmisDAutreClientParSaVille()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            ICollection<Client> Result = new List<Client> { c3 };

            ICollection<Client> Received = RepoClients.RechercherClientsPourUneVille(c3.Ville);


            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirPlusieurClientsParmisDAutreClientParLeurVille()
        {
            SetUp();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            Client c4 = new Client
            {
                NomClient = "c4",
                NoEnregistrement = 4,
                Rue = null,
                Ville = "La",
                CodePostal = "a2b3c4",
                Telephone = null
            };
            Client c5 = new Client
            {
                NomClient = "c5",
                NoEnregistrement = 5,
                Rue = null,
                Ville = "Si",
                CodePostal = "d5e6f7",
                Telephone = null
            };
            Client c6 = new Client
            {
                NomClient = "c6",
                NoEnregistrement = 6,
                Rue = null,
                Ville = "Do",
                CodePostal = "g8h9i0",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);
            RepoClients.AjouterClient(c4);
            RepoClients.AjouterClient(c5);
            RepoClients.AjouterClient(c6);

            ICollection<Client> Result = new List<Client> { c2,c5 };

            ICollection<Client> Received = RepoClients.RechercherClientsPourUneVille("Si");


            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirClientQuandUnClientNExistePasParLaVilleRetourneUneListeVide()
        {
            SetUp();
            
            ICollection<Client> Result = new List<Client> { };

            ICollection<Client> Received = RepoClients.RechercherClientsPourUneVille("City");

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirClientQuandVilleNExistePasRetourneUneListeVide()
        {
            SetUp();

            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            Client c4 = new Client
            {
                NomClient = "c4",
                NoEnregistrement = 4,
                Rue = null,
                Ville = "La",
                CodePostal = "a2b3c4",
                Telephone = null
            };
            Client c5 = new Client
            {
                NomClient = "c5",
                NoEnregistrement = 5,
                Rue = null,
                Ville = "Si",
                CodePostal = "d5e6f7",
                Telephone = null
            };
            Client c6 = new Client
            {
                NomClient = "c6",
                NoEnregistrement = 6,
                Rue = null,
                Ville = "Do",
                CodePostal = "g8h9i0",
                Telephone = null
            };

            ICollection<Client> Result = new List<Client> { };

            ICollection<Client> Received = RepoClients.RechercherClientsPourUneVille("City");

            Assert.Equal(Result, Received);
        }
        //ObtenirStatsPourUnClient Tests
        [Fact]
        public void ObtenirStatsPourUnClientAvecUnProjetRetourneLeDtoPourLeClient()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);

            StatsClient Result = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = p1.Budget,
                BudgetMoyen = p1.Budget,
                BudgetMax = p1.Budget
            };

            StatsClient Received = RepoClients.ObtenirStatsPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirStatsPourUnClientAvecUnProjetParmisDAutreClientEtProjet()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c3.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 150,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c2.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);

            StatsClient Result = new StatsClient
            {
                NomClient = c2.NomClient,
                NombreProjets = c2.Projets.Count,
                BudgetTotal = p3.Budget,
                BudgetMoyen = p3.Budget,
                BudgetMax = p3.Budget
            };

            StatsClient Received = RepoClients.ObtenirStatsPourUnClient(c2.NomClient);

            Assert.Equal(Result, Received);

        }

        [Fact]
        public void ObtenirStatsPourUnClientAvecPlusieurProjetParmisDAutreClientEtProjetRetourneLesBonnesInformations()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c2.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);

            StatsClient Result = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = p1.Budget + p2.Budget,
                BudgetMoyen = (p1.Budget + p2.Budget)/2,
                BudgetMax = p2.Budget
            };

            StatsClient Received = RepoClients.ObtenirStatsPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);

        }

        [Fact]
        public void ObtenirStatsPourUnClientAvecPlusieurProjetRetourneLesBonnesInformationsMalgréDonnéeManquante()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = null,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);

            StatsClient Result = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = p1.Budget + p3.Budget,
                BudgetMoyen = (p1.Budget + p3.Budget)/2,
                BudgetMax = p3.Budget
            };

            StatsClient Received = RepoClients.ObtenirStatsPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirStatsPourUnClientAvecUnProjetSansBudgetRetourneLeDtoPourLeClient()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = null,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);

            StatsClient Result = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = 0,
                BudgetMoyen = p1.Budget,
                BudgetMax = p1.Budget
            };

            StatsClient Received = RepoClients.ObtenirStatsPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirStatsPourUnClientQuandClientNExistePasRetourneUneException()
        {
            SetUpClientProjet();

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirStatsPourUnClient("AngusMcFife"));
        }

        [Fact]
        public void ObtenirStatsPourUnClientQuandClientNExistePasParmisDAutreClientsEtProjetsRetourneUneException()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c2.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 500,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c3.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirStatsPourUnClient("AngusMcFife"));
        }

        //CalculerStatsPourTousLesClients Tests

        [Fact]
        public void CalculerLesStatsPourTousLesClientAvecUnClientRetourneUneListeDeDtoAvecLeClient()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);

            StatsClient Sc1 = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = p1.Budget,
                BudgetMoyen = p1.Budget,
                BudgetMax = p1.Budget
            };

            ICollection<StatsClient> Result = new List<StatsClient> { Sc1};

            ICollection<StatsClient> Received = RepoClients.CalculerStatsPourTousLesClients();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void CalculerLesStatsPourTousLesClientAvecPlusieurClientsRetourneUneListeDeDtoAvecLesClient()
        {
            SetUpClientProjet();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c2.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 500,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c3.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 200,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);

            StatsClient Sc1 = new StatsClient
            {
                NomClient = c1.NomClient,
                NombreProjets = c1.Projets.Count,
                BudgetTotal = p1.Budget,
                BudgetMoyen = p1.Budget,
                BudgetMax = p1.Budget
            };
            StatsClient Sc2 = new StatsClient
            {
                NomClient = c2.NomClient,
                NombreProjets = c2.Projets.Count,
                BudgetTotal = p2.Budget,
                BudgetMoyen = p2.Budget,
                BudgetMax = p2.Budget
            };
            StatsClient Sc3 = new StatsClient
            {
                NomClient = c3.NomClient,
                NombreProjets = c3.Projets.Count,
                BudgetTotal = p3.Budget,
                BudgetMoyen = p3.Budget,
                BudgetMax = p3.Budget
            };

            ICollection<StatsClient> Result = new List<StatsClient> { Sc1,Sc2,Sc3 };

            ICollection<StatsClient> Received = RepoClients.CalculerStatsPourTousLesClients();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void CalculerStatsPourTousClientSansClientRetourneUneListeVide()
        {
            SetUpClientProjet();

            ICollection<StatsClient> Result = new List<StatsClient> { };

            ICollection<StatsClient> Received = RepoClients.CalculerStatsPourTousLesClients();

            Assert.Equal(Result, Received);
        }

        /*
         * Les Autres Tests logique serait une répetition des Tests ObtenirStatsPourUnClient
         */

        //ObtenirInfoProjetsEnCoursPourUnClient Tests
        [Fact]
        public void ObtenirInfoProjetsEnCoursPourUnClientAvecUnClientQuiAUnProjetEnCoursRetourneUneListeDeDtoDesProjetsDuClientEnCours()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmploye.AjouterEmploye(e1);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "("+e1.NoEmploye+") "+ e1.Nom+" "+e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };

            ICollection<InfosProjetsEnCours> Result = new List<InfosProjetsEnCours> { Ipc1 };

            ICollection<InfosProjetsEnCours> Received = RepoClients.ObtenirInfoProjetsEnCoursPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirInfoProjetsEnCoursPourUnClientAvecUnClientQuiAPlusieursProjetEnCoursRetourneUneListeDeDtoDesProjetsDuClientEnCours()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);

            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmploye.AjouterEmploye(e1);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };
            InfosProjetsEnCours Ipc2 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p2.NomProjet,
                Budget = p2.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };

            ICollection<InfosProjetsEnCours> Result = new List<InfosProjetsEnCours> { Ipc1,Ipc2 };

            ICollection<InfosProjetsEnCours> Received = RepoClients.ObtenirInfoProjetsEnCoursPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirInfoProjetsEnCoursPourUnClientAvecUnClientQuiAPlusieursProjetParmiDautreClientQuiOntAussiDesProjetsEnCoursRetourneUneListeDeDtoDesProjetsDuClientEnCours()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            Client c2 = new Client
            {
                NomClient = "c2",
                NoEnregistrement = 2,
                Rue = null,
                Ville = "Si",
                CodePostal = "d4e5f6",
                Telephone = null
            };
            Client c3 = new Client
            {
                NomClient = "c3",
                NoEnregistrement = 3,
                Rue = null,
                Ville = "Do",
                CodePostal = "g7h8i8",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);
            RepoClients.AjouterClient(c2);
            RepoClients.AjouterClient(c3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 2
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c2.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 123,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p4 = new Projet
            {
                NomProjet = "p4",
                NomClient = c3.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1234,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);
            RepoProjets.AjouterProjet(p4);

            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };

            RepoEmploye.AjouterEmploye(e1);
            RepoEmploye.AjouterEmploye(e2);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };
            InfosProjetsEnCours Ipc2 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p2.NomProjet,
                Budget = p2.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e2.NoEmploye + ") " + e2.Nom + " " + e2.Prenom
            };

            ICollection<InfosProjetsEnCours> Result = new List<InfosProjetsEnCours> { Ipc1, Ipc2 };

            ICollection<InfosProjetsEnCours> Received = RepoClients.ObtenirInfoProjetsEnCoursPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }
        [Fact]
        public void ObtenirInfoProjetsEnCoursPourUnClientAvecUnClientQuiAPlusieursProjetAutantEnCoursOuNonRetourneUneListeDeDtoDesProjetsDuClientEnCours()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = Convert.ToDateTime("2021-04-04"),
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 2
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 123,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p4 = new Projet
            {
                NomProjet = "p4",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 1234,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);
            RepoProjets.AjouterProjet(p4);

            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmploye.AjouterEmploye(e1);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };
            InfosProjetsEnCours Ipc2 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p3.NomProjet,
                Budget = p3.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };

            ICollection<InfosProjetsEnCours> Result = new List<InfosProjetsEnCours> { Ipc1, Ipc2 };

            ICollection<InfosProjetsEnCours> Received = RepoClients.ObtenirInfoProjetsEnCoursPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirInfoProjetsEnCoursSansContactClientPourUnClientRetourneUneListeDeDtoDesProjetsDuClientAvecLesBonnesInformation()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = null
            };
            RepoProjets.AjouterProjet(p1);

            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmploye.AjouterEmploye(e1);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = ""
            };

            ICollection<InfosProjetsEnCours> Result = new List<InfosProjetsEnCours> { Ipc1 };

            ICollection<InfosProjetsEnCours> Received = RepoClients.ObtenirInfoProjetsEnCoursPourUnClient(c1.NomClient);

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirInfoProjetsEnCoursQuandPasDeClientRetourneUneException()
        {
            SetUpClientProjetEmploye();

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirInfoProjetsEnCoursPourUnClient("Bobby"));
        }

        [Fact]
        public void ObtenirInfoProjetsEnCoursQuandLeClientNExistePasRetourneUneException()
        {
            SetUpClientProjetEmploye();
            Client c1 = new Client
            {
                NomClient = "c1",
                NoEnregistrement = 1,
                Rue = null,
                Ville = "La",
                CodePostal = "a1b2c3",
                Telephone = null
            };
            RepoClients.AjouterClient(c1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-01-01"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = Convert.ToDateTime("2021-04-04"),
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 2
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = c1.NomClient,
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 123,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p4 = new Projet
            {
                NomProjet = "p4",
                NomClient = c1.NomClient,
                DateDebut = null,
                DateFin = null,
                Budget = 1234,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);
            RepoProjets.AjouterProjet(p4);

            Employe e1 = new Employe
            {
                NoEmploye = 1,
                Nas = 123456789,
                Nom = "1",
                Prenom = "Ea",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmploye.AjouterEmploye(e1);

            InfosProjetsEnCours Ipc1 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p1.NomProjet,
                Budget = p1.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };
            InfosProjetsEnCours Ipc2 = new InfosProjetsEnCours
            {
                NomClient = c1.NomClient,
                NomProjet = p3.NomProjet,
                Budget = p3.Budget,
                Gestionnaire = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom,
                ContactClient = "(" + e1.NoEmploye + ") " + e1.Nom + " " + e1.Prenom
            };

            Assert.Throws<ArgumentException>(() => RepoClients.ObtenirInfoProjetsEnCoursPourUnClient("c4"));
        }
    }
}
