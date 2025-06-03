using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class FaultLinked
    {
        [Key]
        public int ID { get; set; }
        public int CaseID { get; set; }
        public int LinkedID { get; set; }
        public int FaultID { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        public Case? Case { get; set; }
        public Linked? Linked { get; set; }
        public Fault? Fault { get; set; }
    }
}
