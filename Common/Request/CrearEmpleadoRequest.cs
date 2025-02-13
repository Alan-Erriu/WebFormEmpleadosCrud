using System;

namespace Commom.Request
{
    public class CrearEmpleadoRequest
    {

        public string nombre { get; set; }

        public string apellido { get; set; }


        public string numero_telefono { get; set; }

        public DateTime fecha_nacimiento{ get; set; }

        public int rol_id { get; set; }
    }
}