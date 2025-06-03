using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class EvidenceDTO : Evidence
    {
        public IFormFile? File { get; set; }
    }
}
