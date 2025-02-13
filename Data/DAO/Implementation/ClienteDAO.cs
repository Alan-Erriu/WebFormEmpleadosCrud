using Commom.Request;
using Dapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.DAO.Implementation
{
   public class ClienteDAO
    {
        private string _connectionString = @"data source=DESKTOP-SA7J56I\SQLEXPRESS;initial Catalog=empleados; Integrated Security=True;";

        private string _selectClientByEmail = @"SELECT id, email,contrasena FROM [cliente] WHERE email = @Email";



        public Cliente LoginData(LoginRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Email = request.email
                };
                var client = connection.QueryFirstOrDefault<Cliente>(_selectClientByEmail, parameters);

                return client;
            }
        }
    }
}
