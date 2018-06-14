using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetGl.Models
{
    public class Sprint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string DateDebut { get; set; }
        public string DateLimite { get; set; }
       
        public int BacklogId { get; set; }
        public virtual Backlog Backlog { get; set; }
    }
}