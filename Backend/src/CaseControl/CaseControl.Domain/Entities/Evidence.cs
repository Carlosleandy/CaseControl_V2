using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Evidence
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Hash")]
        public Guid Hash { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Ruta")]
        public string? Path { get; set; }


       
        public string? Extension { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Tamaño KB")]
        public decimal SizeKB { get; set; }


        [Required]
        public DateTime DateRegistered { get; set; }


        public int CaseID { get; set; }
        public int EvidenceClassificationID { get; set; }


        public Case? Case { get; set; }

        public EvidenceClassification? EvidenceClassification { get; set; }
        
    }
}
