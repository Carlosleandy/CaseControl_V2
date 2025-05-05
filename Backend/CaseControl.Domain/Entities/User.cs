using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CaseControl.Domain.Entities
{
    public class User 
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Usuario")]
        public string? UserName { get; set; }


        [DefaultValue(true)]
        [Display(Name = "Está Activo?")]
        public bool IsActive { get; set; }


        [DefaultValue(false)]
        [Display(Name = "Es Administrador?")]
        public bool IsAdmin { get; set; }


        public int WorkingGroupID { get; set; }
        public int UserLevelID { get; set; }


        public WorkingGroup? WorkingGroup { get; set; }
        public UserLevel? UserLevel { get; set; }


        public ICollection<Binnacle>? Binnacles { get; set; }
        public ICollection<Recommendation>? Recommendations { get; set; }
        public ICollection<Case>? Cases { get; set; }
        public ICollection<RecoveryHistory>? RecoveryHistories { get; set; }

        public ICollection<CaseAssignment>? CaseAssignments { get; set; }


        [NotMapped]
        public vwEmployee? Employee { get; set; }

    }
}
