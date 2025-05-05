using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Linked : Person
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Código")]
        public string? Code { get; set; }


        public int LinkTypeID { get; set; }
        public LinkType? LinkType { get; set; }

        public int CaseID { get; set; }
        public Case? Case { get; set; }


        public ICollection<RelLinkedFault>? RelUserFaults { get; set; }
        public ICollection<Interview>? Interviews { get; set; }
        public ICollection<FaultLinked>? FaultLinkeds { get; set; }
    }
}
