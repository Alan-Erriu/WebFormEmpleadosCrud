using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.App_Entities.Request
{
    public class CreateEmployeeRequest
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres y máximo 100 caracteres")]
        public string name { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El apellido debe tener al menos 3 caracteres y máximo 100 caracteres")]
        public string last_name { get; set; }

        [MinLength(10, ErrorMessage = "El número de celular debe tener al menos 10 dígitos")]
        [MaxLength(14, ErrorMessage = "El número de teléfono no puede tener más de 14 dígitos")]
        [RegularExpression(@"^[0-9+]+$", ErrorMessage = "El campo celular debe contener solo valores numéricos")]
        public string phone_number { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha inválido")]
        public DateTime date_of_birth { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El puesto debe tener al menos 3 caracteres y máximo 100 caracteres")]
        public string position { get; set; }
    }
}