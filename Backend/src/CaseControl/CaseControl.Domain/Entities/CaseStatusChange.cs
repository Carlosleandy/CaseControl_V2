using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class CaseStatusChange
    {
        public int ID { get; set; }

        public string? userNameRegistered { get; set; }
        
        [Required]
        public DateTime DateRegistered { get; set; }

        public int CaseID { get; set; }
        public int CaseStatusID { get; set; }
        public Case? Case { get; set; }
        public CaseStatus? CaseStatus { get; set; }

    }
}
