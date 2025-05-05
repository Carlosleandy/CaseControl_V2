using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Case
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Asunto Comunicación")]
        public string? Subject { get; set; }


        [Display(Name = "Emisor Comunicación")]
        public string? Transmitter { get; set; }


        [Display(Name = "Destinatario Comunicación")]
        public string? Recipient { get; set; }


        [Display(Name = "No. Comunicación")]
        public string? CommunicationNumber { get; set; }


        [Display(Name = "Fecha de Comunicación")]
        public DateTime DateOfCommunication { get; set; }


        [Display(Name = "Fecha de Recepción")]
        public DateTime DateOfReceipt { get; set; }


        [Display(Name = "Referencia de la Comunicación")]
        public string? CommunicationReference { get; set; }


        [Display(Name = "Oficina")]
        public string? Branch { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Detectado")]
        public decimal AmountDetected { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Investigado")]
        public decimal AmountInvestigated { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Recuperado")]
        public decimal AmountRecovered { get; set; }


        
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Monto Pérdida")]
        public decimal AmountLost { get; set; }


        [Display(Name = "Cuenta Afectada")]
        public string? AffectedAccount { get; set; }


        public string? UserNameRegistered { get; set; }



        public int ReceptionMediumID { get; set; }
        public int CaseTypeID { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CaseStatusID { get; set; }
        public int UserID { get; set; }



        public ReceptionMedium? ReceptionMedium { get; set; }
        public CaseType? CaseType { get; set; }
        public CaseStatus? CaseStatus { get; set; }
        public User? User { get; set; }



        public ICollection<Recommendation>? Recommendations { get; set; }
        public ICollection<Binnacle>? Binnacles { get; set; }
        public ICollection<RecoveryHistory>? RecoveryHistories { get; set; }
        public ICollection<CaseAssignment>? CaseAssignments { get; set; }
        public ICollection<CaseStatusChange>? CaseStatusChanges { get; set; }
        public ICollection<Evidence>? Evidences { get; set; }
        public ICollection<Linked>? Linkeds { get; set; }
        public ICollection<Interview>? Interviews { get; set; }
        public ICollection<FaultLinked>? FaultLinkeds { get; set; }
    }
}
