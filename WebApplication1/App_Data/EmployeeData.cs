using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.App_Entities.DTOs;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1.App_Data
{
    public class EmployeeData
    {

        #region
        private string _connectionString = @"data source=DESKTOP-KCGGJDV\SQLEXPRESS;initial Catalog=ejemplo; Integrated Security=True;";

        private string _insertNewEmployeeQuery = @"INSERT INTO [users] (name, last_name, phone_number, date_of_birth,position_id) values(@Name,@LastName, @PhoneNumber,@DateOfBrith,@PositionId)";

        private string _selectEmployees = @"
    SELECT 
        u.user_id, u.name, u.last_name, u.phone_number, u.date_of_birth, p.description 
    FROM 
        [users] u
    JOIN 
        [position] p ON p.position_id = u.position_id
    WHERE 
        u.status = 1
    ORDER BY 
        u.user_id
    OFFSET 
        (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT 
        @PageSize ROWS ONLY;";

        private string _updateStatusEmployee = @"UPDATE [users] SET status = @StatusUser WHERE user_id = @UserId";

        private string _updateEmployee = "UPDATE [users] SET name = @Name, last_name = @LastName, phone_number = @PhoneNumber, date_of_birth = @Date, position_id = @Position WHERE user_id = @UserId";

        private string _selectAllPosition = @"select position_id, description from [position]";

        private string _countTotalEmployees = @"select COUNT(*) from [users] u where u.status = 1";
        #endregion
        public int CreateNewEmployee(CreateEmployeeRequest request)
        {

            try
            {

                using (var connect = new SqlConnection(_connectionString))
                {
                    var parameters = new
                    {
                        Name = request.name,
                        LastName = request.last_name,
                        PhoneNumber = request.phone_number,
                        DateOfBrith = request.date_of_birth,
                        PositionId = request.position_id,
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
                                employeed.user_id = int.Parse((dr["user_id"].ToString()));
                                employeed.name = dr["name"].ToString();
                                employeed.last_name = dr["last_name"].ToString();
                                employeed.phone_number = dr["phone_number"].ToString();
                                employeed.date_of_birth = dr["date_of_birth"] != DBNull.Value
                                ? Convert.ToDateTime(dr["date_of_birth"])
                                : DateTime.MinValue;
                                employeed.description = (dr["description"].ToString());
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

        public List<Position> GetAllPosition()
        {

            var listPosition = new List<Position>();
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
                                var position = new Position();
                                position.position_id = int.Parse((dr["position_id"].ToString()));
                                position.description = dr["description"].ToString();

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

        public int UpdateEmployeeData(Employee employee)
        {

            var parameters = new
            {
                UserId = employee.user_id,
                Name = employee.name,
                LastName = employee.last_name,
                PhoneNumber = employee.phone_number,
                Date = employee.date_of_birth,
                Position = employee.position_id,
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(_updateEmployee, parameters);
                return rowsAffected;
            }
        }
    }
}

