using CaseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CasesRecoverySummaryDTO
    {
        public Case? Case { get; set; }
        public decimal AmountInvestigated { get; set; }
        public decimal AmountRecovery { get; set; }
        public decimal AmountDifference { get; set; }
        public decimal PercentRecovery { get; set; }
    }
}
