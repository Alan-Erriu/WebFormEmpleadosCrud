using System;

namespace WebApplication1.App_Entities.Request
{
    public class CreateEmployeeRequest
    {

        public string name { get; set; }

        public string last_name { get; set; }


        public string phone_number { get; set; }

        public DateTime date_of_birth { get; set; }

        public int position_id { get; set; }
    }
}