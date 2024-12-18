﻿using System;
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

                var listPosition = _employeeData.GetAllPosition();

                ddl_puestos.DataSource = listPosition;
                ddl_puestos.DataValueField = "position_id";
                ddl_puestos.DataTextField = "description";
                ddl_puestos.DataBind();
                updateEmployeesGrid(1);

            }
        }


        protected void updateEmployeesGrid(int pageNumber)
        {

            EmployeeData employeeData = new EmployeeData();

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

            var request = new CreateEmployeeRequest
            {
                name = txt_nombre.Text,
                last_name = txt_apellido.Text,
                phone_number = txt_numero_celular.Text,
                date_of_birth = date_of_birth,
                position_id = int.Parse(ddl_puestos.SelectedValue)
            };


            var rowsAffected = _employeeData.CreateNewEmployee(request);
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
                EmployeeData employeeData = new EmployeeData();
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
                var employedFromInputs = new Employee()
                {
                    user_id = userId,
                    name = txt_nombre.Text,
                    last_name = txt_apellido.Text,
                    phone_number = txt_numero_celular.Text,
                    date_of_birth = Convert.ToDateTime(txt_fecha_nacimiento.Text),
                    position_id = int.Parse(ddl_puestos.SelectedValue)
                };
                EmployeeData employeeData = new EmployeeData();
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