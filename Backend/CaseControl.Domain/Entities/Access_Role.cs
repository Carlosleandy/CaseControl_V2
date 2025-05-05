using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Access_Role
    {
        [Key]
        public int ID { get; set; }

        public int RoleID { get; set; }
        public int AccessID { get; set; }

        public Role? Role { get; set; }
        public Access? Access { get; set; }
    }
}
