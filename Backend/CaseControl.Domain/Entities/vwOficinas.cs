using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class vwOficinas
    {
        [Key]
        public int ID { get; set; }
        public string? Oficina { get; set; }
        public string? Oficina_Parsed { get; set; }
        public string? Nombre_Oficina { get; set; }
        public string? Tipo_Oficina { get; set; }
        public string? Provincia { get; set; }
        public string? Localidad { get; set; }
        public string? Oficina_Completa { get; set; }
    }
}
