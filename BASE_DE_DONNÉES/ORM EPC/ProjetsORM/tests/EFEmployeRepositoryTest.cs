using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using ProjetsORM.Entites;
using ProjetsORM.EntiteDTOs;
using ProjetsORM.Persistence;

namespace ProjetsORM.AccesDonnees
{
    public class EFEmployeRepositoryTest
    {
        private EFEmployeRepository RepoEmployes;
        private EFProjetRepository RepoProjets;
        private void SetUp()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "TestEmployer_db");
            var contexte = new ProjetsORMContexte(builder.Options);
            RepoEmployes = new EFEmployeRepository(contexte);
        }
        private void SetUpEmployeProjet()
        {
            var builder = new DbContextOptionsBuilder<ProjetsORMContexte>();
            builder.UseInMemoryDatabase(databaseName: "TestEmployer_db");
            var contexte = new ProjetsORMContexte(builder.Options);
            RepoEmployes = new EFEmployeRepository(contexte);
            RepoProjets = new EFProjetRepository(contexte);
        }
        //ObtenirEmploye Tests
        [Fact]
        public void ObtenirEmployeQuandUnEmployeExiste()
        {
            SetUp();
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
            RepoEmployes.AjouterEmploye(e1);

            Employe Received = RepoEmployes.ObtenirEmploye(e1.NoEmploye);

            Assert.Same(e1, Received);
        }

        [Fact]
        public void ObtenirUnEmployeParmisPlusieurs()
        {
            SetUp();
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
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            Employe Received = RepoEmployes.ObtenirEmploye(e2.NoEmploye);

            Assert.NotSame(e1, Received);
            Assert.Same(e2, Received);
            Assert.NotSame(e3, Received);
        }
        [Fact]
        public void ObtenirEmployeQuandEmployeNExistePasRetourneException()
        {
            SetUp();

            Assert.Throws<ArgumentException>(() => RepoEmployes.ObtenirEmploye(0));
        }
        [Fact]
        public void ObtenirUnClientQuiNExistePasParmisPlusieursRetourneException()
        {
            SetUp();
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
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            Assert.Throws<ArgumentException>(() => RepoEmployes.ObtenirEmploye(0));
        }

        //RechercherTousLesSuperviseurs Tests
        [Fact]
        public void ObtenirUnSuperviseurQuandUnSuperviseurExiste()
        {
            SetUp();
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
                NoSuperviseur = 101
            };
            Employe s1 = new Employe
            {
                NoEmploye = 101,
                Nas = 111111111,
                Nom = "1",
                Prenom = "Sa",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(s1);

            ICollection<Employe> Result = new List<Employe>() { s1 };

            ICollection<Employe> Received = RepoEmployes.RechercherTousLesSuperviseurs();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirUnSuperviseurQuandUnSuperviseurQuiSupervisePlusieursEmployeExiste()
        {
            SetUp();
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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 101
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 101
            };
            Employe s1 = new Employe
            {
                NoEmploye = 101,
                Nas = 111111111,
                Nom = "1",
                Prenom = "Sa",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);
            RepoEmployes.AjouterEmploye(s1);

            ICollection<Employe> Result = new List<Employe>() { s1 };

            ICollection<Employe> Received = RepoEmployes.RechercherTousLesSuperviseurs();

            Assert.Equal(Result, Received);
        }
        [Fact]
        public void ObtenirPlusieursSuperviseurQuandPlusieurSuperviseurSuperviseDesEmployeExiste()
        {
            SetUp();
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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 102
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 103
            };
            Employe s1 = new Employe
            {
                NoEmploye = 101,
                Nas = 111111111,
                Nom = "1",
                Prenom = "Sa",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe s2 = new Employe
            {
                NoEmploye = 102,
                Nas = 222222222,
                Nom = "2",
                Prenom = "Sb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe s3 = new Employe
            {
                NoEmploye = 103,
                Nas = 333333333,
                Nom = "3",
                Prenom = "Sc",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);
            RepoEmployes.AjouterEmploye(s1);
            RepoEmployes.AjouterEmploye(s2);
            RepoEmployes.AjouterEmploye(s3);

            ICollection<Employe> Result = new List<Employe>() { s1, s2, s3 };

            ICollection<Employe> Received = RepoEmployes.RechercherTousLesSuperviseurs();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirSuperviseurSansEmployeRetourneListeVide()
        {
            SetUp();

            ICollection<Employe> Result = new List<Employe>() { };
            ICollection<Employe> Received = RepoEmployes.RechercherTousLesSuperviseurs();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirSuperviseurSansSuperviseurMaisAvecEmployeRetourneListeVide()
        {
            SetUp();
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
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            ICollection<Employe> Result = new List<Employe>() { };
            ICollection<Employe> Received = RepoEmployes.RechercherTousLesSuperviseurs();

            Assert.Equal(Result, Received);
        }

        //RechercherEmployesSansSuperviseur Tests

        [Fact]
        public void ObtenirEmployeNonSuperviseQuandUnEmployeNonSuperviserExiste()
        {
            SetUp();
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
            RepoEmployes.AjouterEmploye(e1);

            ICollection<Employe> Result = new List<Employe>() { e1 };
            ICollection<Employe> Received = RepoEmployes.RechercherEmployesSansSuperviseur();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirEmployeNonSuperviseParmisDAutreEmployeSuperviser()
        {
            SetUp();
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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 101
            };
            Employe s1 = new Employe
            {
                NoEmploye = 101,
                Nas = 111111111,
                Nom = "1",
                Prenom = "Sa",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);
            RepoEmployes.AjouterEmploye(s1);

            ICollection<Employe> Result = new List<Employe>() { e2, s1 };
            ICollection<Employe> Received = RepoEmployes.RechercherEmployesSansSuperviseur();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirPlusieursEmployeNonSuperVise()
        {
            SetUp();
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
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            ICollection<Employe> Result = new List<Employe>() { e1, e2, e3 };
            ICollection<Employe> Received = RepoEmployes.RechercherEmployesSansSuperviseur();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirEmployeNonSuperviseQuandAucunEmployeExisteRetourneListeVide()
        {
            SetUp();

            ICollection<Employe> Result = new List<Employe>() { };
            ICollection<Employe> Received = RepoEmployes.RechercherEmployesSansSuperviseur();

            Assert.Equal(Result, Received);
        }

        [Fact]
        public void ObtenirEmployeNonSuperviseQuandTousLesEmployeSontSuperviseRetourneListeVide()
        {
            SetUp();

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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 102
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 103
            };
            Employe s1 = new Employe
            {
                NoEmploye = 101,
                Nas = 111111111,
                Nom = "1",
                Prenom = "Sa",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-01-01"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 102
            };
            Employe s2 = new Employe
            {
                NoEmploye = 102,
                Nas = 222222222,
                Nom = "2",
                Prenom = "Sb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 103
            };
            Employe s3 = new Employe
            {
                NoEmploye = 103,
                Nas = 333333333,
                Nom = "3",
                Prenom = "Sc",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2020-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 101
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);
            RepoEmployes.AjouterEmploye(s1);
            RepoEmployes.AjouterEmploye(s2);
            RepoEmployes.AjouterEmploye(s3);

            ICollection<Employe> Result = new List<Employe>() { };
            ICollection<Employe> Received = RepoEmployes.RechercherEmployesSansSuperviseur();

            Assert.Equal(Result, Received);
        }

        //ObtenirInformationEmploye Tests

        [Fact]
        public void ObtenirInformationEmployeQuandEmployeExiste()
        {
            SetUpEmployeProjet();
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
                NoSuperviseur = 101
            };
            RepoEmployes.AjouterEmploye(e1);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = "bob",
                DateDebut = Convert.ToDateTime("2021-01-02"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);

            InfosTravailEmploye Result = new InfosTravailEmploye
            {
                NoEmploye = e1.NoEmploye,
                Nom = e1.Nom,
                Prenom = e1.Prenom,
                NbEmployesSupervises = e1.Supervises.Count,
                NbProjetsEnCoursGeres = 1,
                NbProjetsEnCoursContactClient = 1
            };

            InfosTravailEmploye Received = RepoEmployes.ObtenirInformationEmploye(e1.NoEmploye);

            Assert.Equal(Result, Received);
        }
        [Fact]
        public void ObtenirInformationEmployeQuandPlusieurEmployeExiste()
        {
            SetUpEmployeProjet();
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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 1
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-01-02"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 2
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 123,
                NoGestionnaire = 2,
                NoContactClient = 1
            };
            Projet p4 = new Projet
            {
                NomProjet = "p4",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1234,
                NoGestionnaire = 3,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);
            RepoProjets.AjouterProjet(p4);

            InfosTravailEmploye Result = new InfosTravailEmploye
            {
                NoEmploye = e1.NoEmploye,
                Nom = e1.Nom,
                Prenom = e1.Prenom,
                NbEmployesSupervises = e1.Supervises.Count,
                NbProjetsEnCoursGeres = 2,
                NbProjetsEnCoursContactClient = 3
            };

            InfosTravailEmploye Received = RepoEmployes.ObtenirInformationEmploye(e1.NoEmploye);

            Assert.Equal(Result, Received);
        }
        [Fact]
        public void ObtenirInformationEmployeQuandEmployeNExistePasRetourneUneException()
        {
            SetUpEmployeProjet();

            Assert.Throws<ArgumentException>(() => RepoEmployes.ObtenirInformationEmploye(0));
        }

        [Fact]
        public void ObtenirInformationEmployeQuandEmployeNExistePasParmisEmployeRetourneUneException()
        {
            SetUpEmployeProjet();
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
                NoSuperviseur = 101
            };
            Employe e2 = new Employe
            {
                NoEmploye = 2,
                Nas = 234567890,
                Nom = "2",
                Prenom = "Eb",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-02-02"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = null
            };
            Employe e3 = new Employe
            {
                NoEmploye = 3,
                Nas = 345678901,
                Nom = "3",
                Prenom = "Ec",
                DateNaissance = null,
                DateEmbauche = Convert.ToDateTime("2021-03-03"),
                Salaire = null,
                TelephoneBureaux = null,
                Adresse = null,
                NoSuperviseur = 1
            };
            RepoEmployes.AjouterEmploye(e1);
            RepoEmployes.AjouterEmploye(e2);
            RepoEmployes.AjouterEmploye(e3);

            Projet p1 = new Projet
            {
                NomProjet = "p1",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-01-02"),
                DateFin = null,
                Budget = 100,
                NoGestionnaire = 1,
                NoContactClient = 1
            };
            Projet p2 = new Projet
            {
                NomProjet = "p2",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1000,
                NoGestionnaire = 1,
                NoContactClient = 2
            };
            Projet p3 = new Projet
            {
                NomProjet = "p3",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 123,
                NoGestionnaire = 2,
                NoContactClient = 1
            };
            Projet p4 = new Projet
            {
                NomProjet = "p4",
                NomClient = "Bob",
                DateDebut = Convert.ToDateTime("2021-02-02"),
                DateFin = null,
                Budget = 1234,
                NoGestionnaire = 3,
                NoContactClient = 1
            };
            RepoProjets.AjouterProjet(p1);
            RepoProjets.AjouterProjet(p2);
            RepoProjets.AjouterProjet(p3);
            RepoProjets.AjouterProjet(p4);

            Assert.Throws<ArgumentException>(() => RepoEmployes.ObtenirInformationEmploye(0));
        }
    }
}
