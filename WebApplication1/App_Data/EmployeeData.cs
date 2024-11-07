using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1.App_Data
{
    public class EmployeeData
    {


        private string _connectionString = @"data source=DESKTOP-KCGGJDV\SQLEXPRESS;initial Catalog=ejemplo; Integrated Security=True;";

        private string _insertNewEmployeeQuery = @"INSERT INTO [users] (name, last_name, phone_number, date_of_birth,position) values(@Name,@LastName, @PhoneNumber,@DateOfBrith,@Position)";
        private string _selectEmployees = @"SELECT * FROM [users]";
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
                        Position = request.position,
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

        public List<Employee> GetAllEmployees()
        {

            var listEmployees = new List<Employee>();
            try
            {

                using (var connect = new SqlConnection(_connectionString))
                {
                    connect.Open();
                    using (var command = new SqlCommand(_selectEmployees, connect))
                    {
                        using (var dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var employeed = new Employee();
                                employeed.user_id = int.Parse((dr["user_id"].ToString()));
                                employeed.name = dr["name"].ToString();
                                employeed.last_name = dr["last_name"].ToString();
                                employeed.phone_number = dr["phone_number"].ToString();
                                employeed.date_of_birth = dr["date_of_birth"] != DBNull.Value
                                ? Convert.ToDateTime(dr["date_of_birth"])
                                : DateTime.MinValue;
                                employeed.position = dr["position"].ToString();
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
    }
}