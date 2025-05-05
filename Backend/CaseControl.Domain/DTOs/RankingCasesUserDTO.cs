using CaseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class RankingCasesUserDTO
    {
        public int CasesTotal { get; set; }
        public List<CasesUserDTO>? CasesUserDTOs { get; set; }
    }

    public class CasesUserDTO
    {
        public int CasesCount { get; set; }
        public User? User { get; set; }
    }
}
