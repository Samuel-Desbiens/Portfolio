using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;


namespace ProjetsORM.Entites
{
    [Table("EMPLOYE")]
    public class Employe
    {
        #region Propriétés
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short NoEmploye { get; set; }
        public decimal Nas { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public DateTime DateEmbauche { get; set; }
        public decimal? Salaire { get; set; }
        public decimal? TelephoneBureaux { get; set; }
        public string Adresse { get; set; }

        #endregion Propriétés

        #region Clés étrangères
        public short? NoSuperviseur { get; set; }
        #endregion Clés étrangères

        #region Propriétés de navigation 
        [ForeignKey("NoSuperviseur")]
        public virtual Employe Superviseur { get; set; }
        public virtual ICollection<Employe> Supervises { get; set; }

        public virtual ICollection<Projet> ProjetsGerer { get; set; }
        public virtual ICollection<Projet> ProjetsContact { get; set; }
        #endregion Propriétés de navigation 

        #region Constructeur
        public Employe()
        {
            Supervises = new List<Employe>();
            ProjetsGerer = new List<Projet>();
            ProjetsContact = new List<Projet>();
        }
        #endregion Constructeur
    }
}

