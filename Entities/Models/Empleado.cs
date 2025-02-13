using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
   public class Empleado
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string numero_telefono { get; set; }
        public DateTime fecha_nacimiento  { get; set; }
        public int rol_id { get; set; }
        public int status { get; set; }
    }
}
