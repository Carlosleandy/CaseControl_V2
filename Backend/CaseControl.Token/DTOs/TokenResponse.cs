using CaseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Token.DTOs
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public User? User { get; set; }
        //public AuditArea? AuditArea { get; set; }
        public List<string>? AccessList { get; set; }

    }
}
