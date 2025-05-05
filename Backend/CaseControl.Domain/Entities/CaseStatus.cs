using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class CaseStatus
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Porciento")]
        public decimal Percent { get; set; }


        public ICollection<Case>? Cases { get; set; }
        public ICollection<CaseStatusChange>? CaseStatusChanges { get; set; }
    }
}
