using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Fault
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public int FaultTypeID { get; set; }
        public FaultType? FaultType { get; set; }

        public ICollection<FaultLinked>? FaultLinkeds { get; set; }
    }
}
