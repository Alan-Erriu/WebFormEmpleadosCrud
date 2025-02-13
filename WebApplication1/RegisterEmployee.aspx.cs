using Commom.Request;
using Entities.Models;
using Data.DAO.Implementation;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private readonly EmpleadoDAO employeeData = new EmpleadoDAO();
        //no debería ver esto en la rama master
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var listPosition = employeeData.GetAllPosition();

                ddl_puestos.DataSource = listPosition;
                ddl_puestos.DataValueField = "id";
                ddl_puestos.DataTextField = "descripcion";
                ddl_puestos.DataBind();
                updateEmployeesGrid(1);

            }
        }


        protected void updateEmployeesGrid(int pageNumber)
        {

            
            grid_empleados.VirtualItemCount = employeeData.GetTotalEmployeesNumber();
            var list = employeeData.GetAllEmployees(pageNumber, grid_empleados.PageSize);
            grid_empleados.DataSource = list;
            grid_empleados.PageIndex = pageNumber - 1;
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

            var request = new CrearEmpleadoRequest
            {
                nombre = txt_nombre.Text,
                apellido = txt_apellido.Text,
                numero_telefono = txt_numero_celular.Text,
                fecha_nacimiento = date_of_birth,
                rol_id = int.Parse(ddl_puestos.SelectedValue)
            };


            var rowsAffected = employeeData.CreateNewEmployee(request);
            if (rowsAffected == 0) return;
            updateEmployeesGrid(1);
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

        protected void grid_empleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
               
                int userId = Convert.ToInt32(e.Keys["user_id"]);
                employeeData.UpdateStatusEmployee(userId);
                updateEmployeesGrid(1);

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
            ddl_puestos.DataTextField = grid_empleados.SelectedRow.Cells[7].Text;



        }

        protected void grid_empleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grid_empleados.PageIndex = e.NewPageIndex;
            //arranca del 0 el index, pasarlo 
            updateEmployeesGrid(e.NewPageIndex + 1);



        }



        protected void btn_editar_Click(object sender, EventArgs e)
        {
            try
            {

                var userId = int.Parse((grid_empleados.SelectedRow.Cells[2].Text));
                var employedFromInputs = new Empleado()
                {
                    id = userId,
                    nombre = txt_nombre.Text,
                    apellido = txt_apellido.Text,
                    numero_telefono = txt_numero_celular.Text,
                    fecha_nacimiento = Convert.ToDateTime(txt_fecha_nacimiento.Text),
                    rol_id = int.Parse(ddl_puestos.SelectedValue)
                };
                
                employeeData.UpdateEmployeeData(employedFromInputs);
                int currentPage = grid_empleados.PageIndex;
                updateEmployeesGrid(currentPage + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}