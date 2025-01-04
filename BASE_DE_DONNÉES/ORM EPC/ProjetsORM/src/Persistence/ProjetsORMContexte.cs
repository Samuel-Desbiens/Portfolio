using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetsORM.Entites;

namespace ProjetsORM.Persistence
{
    public class ProjetsORMContexte : DbContext
    {
        #region Propriétés DBSet
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Projet> Projets { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        #endregion Propriétés DBSet

        #region Constructeur
        public ProjetsORMContexte(DbContextOptions<ProjetsORMContexte> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        #endregion Constructeur

       


        protected override void OnModelCreating(ModelBuilder builder) 
        {
            BuildEmploye(builder);
            BuildProjet(builder);
            BuildClient(builder);
        }

        private void BuildEmploye(ModelBuilder builder)
        {
            //Clé Primaire
            builder.Entity<Employe>()
                .HasKey(e => e.NoEmploye);

            //Index Uniques
            builder.Entity<Employe>()
                .HasIndex(e => e.Nas)
                .IsUnique();

            //Clés Etrangere
            builder.Entity<Employe>()
                .HasOne(employe => employe.Superviseur)
                .WithMany(super => super.Supervises)
                .OnDelete(DeleteBehavior.Restrict);

            //Colonnes
            builder.Entity<Employe>()
                .Property(e => e.Nom)
                .IsRequired()
                .HasColumnType("varchar(10)");
            builder.Entity<Employe>()
               .Property(e => e.Prenom)
               .IsRequired()
               .HasColumnType("varchar(10)");
            builder.Entity<Employe>()
               .Property(e => e.DateNaissance);
            builder.Entity<Employe>()
               .Property(e => e.DateNaissance)
               .IsRequired();
            builder.Entity<Employe>()
               .Property(e => e.Salaire);
            builder.Entity<Employe>()
               .Property(e => e.TelephoneBureaux);
            builder.Entity<Employe>()
               .Property(e => e.Adresse)
                .HasColumnType("varchar(15)");
            ;

        }
        private void BuildProjet(ModelBuilder builder)
        {
            //Clé Primaire
            builder.Entity<Projet>()
              .HasKey(e => new { e.NomProjet, e.NomClient });


            //Index Uniques
            //(None)

            //Clés Etrangere
            builder.Entity<Projet>()
                .HasOne(projet => projet.Client)
                .WithMany(client => client.Projets)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Projet>()
                .HasOne(e => e.Gestionnaire)
                .WithMany(gesti => gesti.ProjetsGerer)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.Entity<Projet>()
               .HasOne(e => e.Contact)
               .WithMany(contact => contact.ProjetsContact)
               .OnDelete(DeleteBehavior.Restrict);

            //Colonnes
            builder.Entity<Projet>()
                .Property(e => e.DateDebut);
            builder.Entity<Projet>()
                .Property(e => e.DateFin);
            builder.Entity<Projet>()
                .Property(e => e.Budget);
        }
        private void BuildClient(ModelBuilder builder)
        {
            //Clé Primaire
            builder.Entity<Client>()
               .HasKey(e => e.NomClient);

            //Index Uniques
            builder.Entity<Client>()
                .HasIndex(e => e.NoEnregistrement)
                .IsUnique();

            //Clés Etrangere   
            //(None)

            //Colonnes
            builder.Entity<Client>()
               .Property(e => e.Rue);
            builder.Entity<Client>()
                .Property(e => e.Rue)
                .HasColumnType("varchar(10)");
            builder.Entity<Client>()
                .Property(e => e.Ville)
                .HasColumnType("varchar(10)")
                .IsRequired();
            builder.Entity<Client>()
                .Property(e => e.CodePostal)
                .IsRequired();
            builder.Entity<Client>()
                .Property(e => e.Telephone);
        }

    }
}
