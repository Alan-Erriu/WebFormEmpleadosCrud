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
                //ddl_puesto
                //txt_puesto.DataValueField aca el id
                //txt_puesto.DataTextField lista de string (descripcion)
                //txt_puesto.DataSource = listPosition;

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

        //1 busca de que tipo es la fila (botones, texto etc)
        //2 busca por celdas []rows[]colum, ahi tiene que estar si o si el boton eliminar
        //3 si el comando el "delete" agrega un onClick con una funcion js
        protected void grid_empleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton deleteButton = (LinkButton)e.Row.Cells[0].Controls[0];

                if (deleteButton != null && deleteButton.CommandName == "Delete")
                {
                    deleteButton.OnClientClick = "return confirm('¿Estás seguro de que deseas eliminar este empleado?');";
                }
            }
        }
        //modal  preguntar
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

            txt_nombre.Text = grid_empleados.SelectedRow.Cells[3].Text;
            txt_apellido.Text = grid_empleados.SelectedRow.Cells[4].Text;
            txt_numero_celular.Text = grid_empleados.SelectedRow.Cells[5].Text;
            txt_fecha_nacimiento.Text = Convert.ToDateTime(grid_empleados.SelectedRow.Cells[6].Text).ToString("yyyy-MM-dd");
            txt_puesto.Text = grid_empleados.SelectedRow.Cells[7].Text;



        }


        protected void btn_editar_Click(object sender, EventArgs e)
        {
            try
            {

                var userId = int.Parse((grid_empleados.SelectedRow.Cells[2].Text));
                var employedFromInputs = new Employee()
                {
                    user_id = userId,
                    name = txt_nombre.Text,
                    last_name = txt_apellido.Text,
                    phone_number = txt_numero_celular.Text,
                    date_of_birth = Convert.ToDateTime(txt_fecha_nacimiento.Text),
                    position = txt_puesto.Text
                };
                EmployeeData employeeData = new EmployeeData();
                employeeData.UpdateEmployeeData(employedFromInputs);
                updateEmployeesGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}