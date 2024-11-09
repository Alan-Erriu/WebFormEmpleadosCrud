using Dapper;
using System.Data.SqlClient;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1.App_Data
{

    public class AuthData
    {
        private string _connectionString = @"data source=DESKTOP-KCGGJDV\SQLEXPRESS;initial Catalog=ejemplo; Integrated Security=True;";

        private string _selectClientByEmail = @"SELECT * FROM [client] WHERE client_email = @Email";

        private SqlConnection _connection;

        public Client LoginData(LoginRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Email = request.client_email
                };
                var client = connection.QueryFirstOrDefault<Client>(_selectClientByEmail, parameters);

                return client;
            }
        }
    }
}