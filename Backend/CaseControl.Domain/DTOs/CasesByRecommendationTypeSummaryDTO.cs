using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CasesByRecommendationTypeSummaryDTO
    {
        public int ID { get; set; }
        public string? RecommendationType { get; set; }
        public int Count { get; set; }
    }
}
