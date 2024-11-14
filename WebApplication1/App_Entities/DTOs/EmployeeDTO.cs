using System;

namespace WebApplication1.App_Entities.DTOs
{
    public class EmployeeDTO
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string description { get; set; }
    }
}