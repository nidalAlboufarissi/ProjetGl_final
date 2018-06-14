using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetGl.Models
{
    public class Collaboration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProjetId { get; set; }
        public virtual Projet Projet { get; set; }
        public string CollaborateurId { get; set; }
        public virtual ApplicationUser Collaborateur { get; set; }
    }
}