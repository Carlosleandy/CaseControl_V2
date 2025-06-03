using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Token.DTOs
{
    public class JWTResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
