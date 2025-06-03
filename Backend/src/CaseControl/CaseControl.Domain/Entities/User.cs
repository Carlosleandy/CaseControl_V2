using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CaseControl.Domain.Entities
{
    public class User 
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Usuario")]
        public string? UserName { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Contraseña")]
        public string? PasswordHash { get; set; }


        [DefaultValue(true)]
        [Display(Name = "Está Activo?")]
        public bool IsActive { get; set; }


        [DefaultValue(false)]
        [Display(Name = "Es Administrador?")]
        public bool IsAdmin { get; set; }


        public int WorkingGroupID { get; set; }
        public int UserLevelID { get; set; }
        public int IDGerencia { get; set; }


        // Propiedades de navegación - se excluyen de la serialización por defecto
        [JsonIgnore]
        public WorkingGroup? WorkingGroup { get; set; }
        
        [JsonIgnore]
        public UserLevel? UserLevel { get; set; }

        [JsonIgnore]
        public ICollection<Binnacle>? Binnacles { get; set; }
        
        [JsonIgnore]
        public ICollection<Recommendation>? Recommendations { get; set; }
        
        [JsonIgnore]
        public ICollection<Case>? Cases { get; set; }
        
        [JsonIgnore]
        public ICollection<RecoveryHistory>? RecoveryHistories { get; set; }

        [JsonIgnore]
        public ICollection<CaseAssignment>? CaseAssignments { get; set; }


        [NotMapped]
        public vwEmployee? Employee { get; set; }

        [NotMapped]
        public string? Password { get; set; }

        [NotMapped]
        public string? CurrentPassword { get; set; }

        [Display(Name = "Última fecha de cambio de contraseña")]
        public DateTime? LastPasswordChangeDate { get; set; }
        [JsonIgnore]
        Gerencia? Gerencia { get; set; }
    }
}
