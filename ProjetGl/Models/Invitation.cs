using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetGl.Models
{
    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Etat { get; set; }
        public int ProjetId { get; set; }
        public virtual Projet Projet { get; set; }
        public string EmetteurId { get; set; }
        public virtual ApplicationUser Emetteur { get; set; }
        public  string RecepteurId { get; set; }
        public virtual ApplicationUser Recepteur { get; set; }
    }
}