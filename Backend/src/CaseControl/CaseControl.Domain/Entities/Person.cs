using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CaseControl.Domain.Entities
{
    public class Person
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripcón")]
        public string? LastName { get; set; }


        public string? Identification { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? Address { get; set; }
        public string FullName => $"{Name} {LastName}";
    }
}
