﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
