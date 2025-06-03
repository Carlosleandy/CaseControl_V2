using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseControl.Domain.Entities
{
    public class vwEmployee
    {
        [Key]
        public int ID { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Nombre_Completo { get; set; }
        public string? Usuario { get; set; }
        public string? Correo { get; set; }
        public string? Identificacion { get; set; }
        public string? ID_de_Puesto { get; set; }
        public string? Puesto { get; set; }
        public string? Tipo_de_Empleado { get; set; }
        public string? Gerencia { get; set; }
        public string? Ubicacion { get; set; }
        public string? Empresa { get; set; }
        public DateTime? Fecha_de_Nacimiento { get; set; }
        public DateTime? Fecha_de_Ingreso { get; set; }


    }
}
