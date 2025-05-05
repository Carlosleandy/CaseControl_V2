using CaseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.DTOs
{
    public class CasesByLinkedCodeDTO
    {
        public Linked? Linked { get; set; }
        public List<Case>? Cases { get; set; }
    }
}
