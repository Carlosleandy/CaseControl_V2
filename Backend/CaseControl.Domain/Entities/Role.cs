using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        


        public ICollection<Access_Role>? Access_Roles { get; set; }
        public ICollection<UserLevel>? UserLevels { get; set; }
    }
}
