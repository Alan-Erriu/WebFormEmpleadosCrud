using Dapper;
using System;
using System.Data.SqlClient;
using WebApplication1.App_Entities.Request;

namespace WebApplication1.App_Data
{
    public class EmployeeData
    {
        //pasar al settings.json
        private string _connectionString = @"data source=DESKTOP-KCGGJDV\SQLEXPRESS;initial Catalog=ejemplo; Integrated Security=True;";

        private string _insertNewEmployeeQuery = @"INSERT INTO [users] (name, last_name, phone_number, date_of_birth,position) values(@Name,@LastName, @PhoneNumber,@DateOfBrith,@Position)";

        public /*async Task*/void CreateNewEmployee(CreateEmployeeRequest request)
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
                    connect.Execute(_insertNewEmployeeQuery, parameters);


                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)

            { Console.WriteLine(ex.ToString()); }

        }
    }
}