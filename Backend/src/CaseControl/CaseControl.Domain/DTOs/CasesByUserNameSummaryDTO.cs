using CaseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CasesByUserNameSummaryDTO
    {
        public User? User { get; set; }
        public int Count { get; set; }
    }
}
