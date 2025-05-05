using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CaseControl.Domain.Entities
{
    public class CaseAssignment
    {
        public int ID { get; set; }

        public string? userNameRegistered { get; set; }
        public string? Observations { get; set; }
                

        [Required]
        public DateTime DateRegistered { get; set; }

        public int CaseID { get; set; }
        public int UserID { get; set; }
        public Case? Case { get; set; }
        public User? User { get; set; }

    }
}
