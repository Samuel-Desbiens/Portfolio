using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;


namespace ProjetsORM.Entites
{
    [Table("CLIENT")]
    public class Client
    {
        #region Propriétés
        public string NomClient { get; set; }
        public short NoEnregistrement { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public decimal? Telephone { get; set; }


        #endregion Propriétés

        #region Clés étrangères
        //(None)
        #endregion Clés étrangères

        #region Propriétés de navigation 
        public virtual ICollection<Projet> Projets { get; set; }
        #endregion Propriétés de navigation 


        #region Constructeur
        public Client()
        {
           Projets = new List<Projet>();
        }
        #endregion Constructeur
    }
}
