using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Binnacle
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripción")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Fecha de Registro")]
        public DateTime DateRegistered { get; set; } = DateTime.Now;


        public int CaseID { get; set; }
        public int UserID { get; set; }


        public Case? Case { get; set; }
        public User? User { get; set; }
    }
}
