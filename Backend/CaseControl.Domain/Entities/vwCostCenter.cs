using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class vwCostCenter
    {
        [Key]
        public int ID { get; set; }
        public string? Center { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int Code { get; set; }
        public string? Full_Description { get; set; }
    }
}
