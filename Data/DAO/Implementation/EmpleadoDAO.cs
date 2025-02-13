using Commom.Request;
using Dapper;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace Data.DAO.Implementation
{
    public class EmpleadoDAO
    {

        #region
        private string _connectionString = @"data source=DESKTOP-SA7J56I\SQLEXPRESS;initial Catalog=empleados; Integrated Security=True;";

        private string _insertNewEmployeeQuery = @"INSERT INTO [empleado] (nombre, apellido, numero_telefono, fecha_nacimiento,rol_id) values(@Name,@LastName, @PhoneNumber,@DateOfBrith,@PositionId)";

        private string _selectEmployees = @"
    SELECT 
        u.id, u.nombre, u.apellido, u.numero_telefono, u.fecha_nacimiento, p.descripcion 
    FROM 
        [empleado] u
    JOIN 
        [rol_empleado] p ON p.id = u.id
    WHERE 
        u.status = 1
    ORDER BY 
        u.id
    OFFSET 
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT 
        @PageSize ROWS ONLY;";

        private string _updateStatusEmployee = @"UPDATE [cliente] SET status = @StatusUser WHERE id = @UserId";

        private string _updateEmployee = "UPDATE [empleado] SET nombre = @Name, apellido = @LastName, numero_telefono = @PhoneNumber, fecha_nacimiento = @Date, rol_id = @Position WHERE id = @UserId";

        private string _selectAllPosition = @"select id, descripcion from [rol_empleado]";

        private string _countTotalEmployees = @"select COUNT(*) from [empleado] u where u.status = 1";
        #endregion
        public int CreateNewEmployee(CrearEmpleadoRequest request)
        {

            try
            {

                using (var connect = new SqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        Name = request.nombre,
                        LastName = request.apellido,
                        PhoneNumber = request.numero_telefono,
                        DateOfBrith = request.fecha_nacimiento,
                        PositionId = request.rol_id,
                    };
                    var rowsAffected = connect.Execute(_insertNewEmployeeQuery, parameters);
                    return rowsAffected;

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }

        public int GetTotalEmployeesNumber()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var totalRows = connection.QueryFirstOrDefault<int>(_countTotalEmployees);
                return totalRows;
            }
        }
        public List<EmployeeDTO> GetAllEmployees(int pageNumber, int pageSize)
        {

            var listEmployees = new List<EmployeeDTO>();
            try
            {

                using (var connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
                    using (var command = new SqlCommand(_selectEmployees, connect))
                    {
                        command.Parameters.AddWithValue("@PageNumber", pageNumber);
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                        using (var dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var employeed = new EmployeeDTO();
                                employeed.user_id = int.Parse((dr["id"].ToString()));
                                employeed.name = dr["nombre"].ToString();
                                employeed.last_name = dr["apellido"].ToString();
                                employeed.phone_number = dr["numero_telefono"].ToString();
                                employeed.date_of_birth = dr["fecha_nacimiento"] != DBNull.Value
                                ? Convert.ToDateTime(dr["fecha_nacimiento"])
                                : DateTime.MinValue;
                                employeed.description = (dr["descripcion"].ToString());
                                listEmployees.Add(employeed);
                            }

                            return listEmployees;
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return listEmployees;
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return listEmployees;
            }

        }

        public List<RolEmpleado> GetAllPosition()
        {

            var listPosition = new List<RolEmpleado>();
            try
            {

                using (var connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
                    using (var command = new SqlCommand(_selectAllPosition, connect))
                    {
                        using (var dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var position = new RolEmpleado();
                                position.id = int.Parse((dr["id"].ToString()));
                                position.descripcion = dr["descripcion"].ToString();

                                listPosition.Add(position);
                            }

                            return listPosition;
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return listPosition;
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return listPosition;
            }

        }

        public int UpdateStatusEmployee(int user_id)
        {
            var parameters = new
            {
                UserId = user_id,
                StatusUser = 0
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(_updateStatusEmployee, parameters);
                return rowsAffected;
            }
        }

        public int UpdateEmployeeData(Empleado employee)
        {

            var parameters = new
            {
                UserId = employee.id,
                Name = employee.nombre,
                LastName = employee.apellido,
                PhoneNumber = employee.numero_telefono,
                Date = employee.fecha_nacimiento,
                Position = employee.rol_id,
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(_updateEmployee, parameters);
                return rowsAffected;
            }
        }
    }
}

