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
        
        // Relación con Gerencia
        public int? GerenciaID { get; set; }



        // Propiedades de navegación - se excluyen de la serialización por defecto para evitar referencias cíclicas
        [JsonIgnore]
        public ReceptionMedium? ReceptionMedium { get; set; }
        
        [JsonIgnore]
        public CaseType? CaseType { get; set; }
        
        [JsonIgnore]
        public CaseStatus? CaseStatus { get; set; }
        
        [JsonIgnore]
        public User? User { get; set; }
        
        [JsonIgnore]
        public Gerencia? Gerencia { get; set; }

        [JsonIgnore]
        public ICollection<Recommendation>? Recommendations { get; set; }
        
        [JsonIgnore]
        public ICollection<Binnacle>? Binnacles { get; set; }
        
        [JsonIgnore]
        public ICollection<RecoveryHistory>? RecoveryHistories { get; set; }
        
        [JsonIgnore]
        public ICollection<CaseAssignment>? CaseAssignments { get; set; }
        
        [JsonIgnore]
        public ICollection<CaseStatusChange>? CaseStatusChanges { get; set; }
        
        [JsonIgnore]
        public ICollection<Evidence>? Evidences { get; set; }
        
        [JsonIgnore]
        public ICollection<Linked>? Linkeds { get; set; }
        
        [JsonIgnore]
        public ICollection<Interview>? Interviews { get; set; }
        
        [JsonIgnore]
        public ICollection<FaultLinked>? FaultLinkeds { get; set; }
    }
}
