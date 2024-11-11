using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.App_Data;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private EmployeeData _employeeData = new EmployeeData();
        //no debería ver esto en la rama master
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<string> listPosition = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    listPosition.Add("jefe de planta" + i.ToString());
                }

                txt_puesto.DataSource = listPosition;
                txt_puesto.DataBind();
                updateEmployeesGrid();
            }
        }


        protected void updateEmployeesGrid()
        {

            EmployeeData employeeData = new EmployeeData();
            List<Employee> list = new List<Employee>();
            list = employeeData.GetAllEmployees();
            grid_empleados.DataSource = list;
            grid_empleados.DataBind();
        }


        protected void btn_crear_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txt_nombre.Text))
            {
                lbl_error.Text = "El nombre es obligatorio.";
                lbl_error.ForeColor = System.Drawing.Color.Red;
                return;
            }


            DateTime date_of_birth;
            DateTime.TryParse(txt_fecha_nacimiento.Text, out date_of_birth);

            var request = new CreateEmployeeRequest
            {
                name = txt_nombre.Text,
                last_name = txt_apellido.Text,
                phone_number = txt_numero_celular.Text,
                date_of_birth = date_of_birth,
                position = txt_puesto.SelectedItem.Text
            };


            var rowsAffected = _employeeData.CreateNewEmployee(request);
            if (rowsAffected == 0) return;
            updateEmployeesGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert('Empleado creado con éxito');", true);
            clearInputs();




        }
        protected void clearInputs()
        {
            txt_apellido.Text = "";
            txt_nombre.Text = "";
            txt_nombre.Text = "";
            txt_numero_celular.Text = "";
            txt_fecha_nacimiento.Text = "";
        }

        protected void grid_empleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                EmployeeData employeeData = new EmployeeData();
                int userId = Convert.ToInt32(e.Keys["user_id"]);
                employeeData.UpdateStatusEmployee(userId);
                updateEmployeesGrid();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void grid_empleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dataTime = new DateTime();
            var userId = grid_empleados.SelectedRow.Cells[2].Text;
            txt_nombre.Text = grid_empleados.SelectedRow.Cells[3].Text; // 4 veces esto
            txt_apellido.Text = grid_empleados.SelectedRow.Cells[4].Text;
            txt_numero_celular.Text = grid_empleados.SelectedRow.Cells[5].Text;
            txt_fecha_nacimiento.Text = Convert.ToDateTime(grid_empleados.SelectedRow.Cells[6].Text).ToString("yyyy-MM-dd");
            //txt_fecha_nacimiento = grid_empleados.SelectedRow.Cells[6].Text; 

        }
    }
}