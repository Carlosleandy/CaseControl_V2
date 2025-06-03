using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Gerencia
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Descripción")]
        public string? Name { get; set; }
        
        // Relación con la entidad Case
        // Evitar referencias cíclicas en la serialización JSON
        [JsonIgnore]
        public ICollection<Case>? Cases { get; set; }
    }
}