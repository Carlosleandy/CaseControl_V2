using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CasesByStatusSummaryDTO
    {
        public int ID { get; set; }
        public string? Status { get; set; }
        public int Count { get; set; }
    }
}
