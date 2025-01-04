using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace ProjetsORM.Entites
{
    [Table("PROJET")]
    public class Projet
    {
        #region Propriétés
        public string NomProjet { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public short? Budget { get; set; }
        #endregion Propriétés

        #region Clés étrangères
        public string NomClient { get; set; }
        public short NoGestionnaire { get; set; }
        public short? NoContactClient { get; set; }
        #endregion Clés étrangères

        #region Propriétés de navigation 
        [ForeignKey("NoGestionnaire")]
        public virtual Employe Gestionnaire { set; get; }
        [ForeignKey("NoContactClient")]
        public virtual Employe Contact { set; get; }
        [ForeignKey("NomClient")]
        public virtual Client Client { get; set; }
        #endregion Propriétés de navigation 

        #region Constructeur
        //Not Needed...
        #endregion Constructeur
    }
}
