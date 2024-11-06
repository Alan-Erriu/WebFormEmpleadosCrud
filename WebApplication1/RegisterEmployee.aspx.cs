using System;
using WebApplication1.App_Data;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private EmployeeData _employeeData;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateEmployee(object sender, EventArgs e)
        {
            //var request = new CreateEmployeeRequest
            //{
            //    name = txt_nombre.Text,
            //    last_name = txt_apellido.Text,
            //    phone_number = txt_numero_celular.Text,
            //    position = "operario"
            //};

            _employeeData = new EmployeeData();
            _employeeData.CreateNewEmployee();

        }

    }
}