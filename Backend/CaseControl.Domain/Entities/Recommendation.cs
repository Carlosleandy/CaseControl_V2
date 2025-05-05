using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Recommendation
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Título")]
        public string? Title { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Unidad a la que va dirigida")]
        public string? UnitToWhichItIsAddressed { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Contacto")]
        public string? Contact { get; set; }

                
        [Display(Name = "Respuesta")]
        public string? Response { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Fecha de Registro")]
        public DateTime DateRegistered { get; set; } = DateTime.Now;



        public int RecommendationStatusID { get; set; }
        public int CaseID { get; set; }
        public int UserID { get; set; }
        public int RecommendationTypeID { get; set; }


        public RecommendationStatus? RecommendationStatus { get; set; }
        public Case? Case { get; set; }
        public User? User { get; set; }
        public RecommendationType? RecommendationType { get; set; }



    }
}
