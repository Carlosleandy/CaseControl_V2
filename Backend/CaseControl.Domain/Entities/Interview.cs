using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Interview
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? Description { get; set; }


        [Display(Name = "Fecha de Entrevista")]
        public DateTime DateInterview { get; set; }


        [Required]
        public DateTime DateRegistered { get; set; }


        public int IntervieweeTypeID { get; set; }
        public IntervieweeType? IntervieweeType { get; set; }


        public int CaseID { get; set; }
        public Case? Case { get; set; }


        public int LinkedID { get; set; }
        public Linked? Linked { get; set; }

    }
}
