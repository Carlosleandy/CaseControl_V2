using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CaseAssignmentDto
    {
        public int ID { get; set; }
        public string? userNameRegistered { get; set; }
        public string? Observations { get; set; }
        public DateTime DateRegistered { get; set; }
        public int CaseID { get; set; }
        public int UserID { get; set; }
    }
}
