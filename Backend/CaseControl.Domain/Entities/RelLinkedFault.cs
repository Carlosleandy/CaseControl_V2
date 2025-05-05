using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class RelLinkedFault
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateFault { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? Description { get; set; }
        public string? UserNameRegistered { get; set; }
        public DateTime DateRegistered { get; set; }

        public int FaultID { get; set; }
        public Fault? Fault { get; set; }

        public int LinkedID { get; set; }
        public Linked? Linked { get; set; }

        
    }
}
