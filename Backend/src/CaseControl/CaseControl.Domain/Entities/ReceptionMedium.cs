using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CaseControl.Domain.Entities
{
    public class ReceptionMedium
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripción")]
        public string? Name { get; set; }



        public ICollection<Case>? Cases { get; set; }
    }
}
