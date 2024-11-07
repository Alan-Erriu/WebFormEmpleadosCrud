using System;
using System.Collections.Generic;
using WebApplication1.App_Data;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private EmployeeData _employeeData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Employee> list = new List<Employee>();
                List<string> listPosition = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    Employee employee = new Employee();
                    employee.user_id = i;
                    employee.name = "alan " + i.ToString();
                    employee.last_name = "erriu " + i.ToString();
                    listPosition.Add("jefe de planta" + i.ToString());
                    list.Add(employee);
                }
                grid_empleados.DataSource = list;
                grid_empleados.DataBind();
                txt_puesto.DataSource = listPosition;
                txt_puesto.DataBind();
            }
        }



        protected void btn_crear_Click(object sender, EventArgs e)
        {

            DateTime date_of_birth;
            DateTime.TryParse(txt_fecha_nacimiento.Text, out date_of_birth);

            var request = new CreateEmployeeRequest
            {
                name = txt_nombre.Text,
                last_name = txt_apellido.Text,
                phone_number = txt_numero_celular.Text,
                date_of_birth = date_of_birth,
                position = txt_puesto.Text
            };

            _employeeData = new EmployeeData();
            _employeeData.CreateNewEmployee(request);
        }
    }
}