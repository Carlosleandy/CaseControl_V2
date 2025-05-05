using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class Access
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Key { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;


        public ICollection<Access_Role>? Access_Roles { get; set; }

    }
}
