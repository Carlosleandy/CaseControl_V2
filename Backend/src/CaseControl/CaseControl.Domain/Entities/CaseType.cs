using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class CaseType
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripción")]
        public string? Name { get; set; }


        public ICollection<Case>? Cases { get; set; }
    }
}
