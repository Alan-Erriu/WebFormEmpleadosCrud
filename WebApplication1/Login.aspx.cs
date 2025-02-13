using Entities.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Services;
using Commom.Request;
using Data.DAO.Implementation;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Cliente HandleLogin(LoginRequest credentials)
        {
                ClienteDAO authData = new ClienteDAO();
            try
            {

                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(credentials.email, emailPattern)) throw new ArgumentException("El correo electrónico no es válido.");

                var client = authData.LoginData(credentials);

                return client;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }

    }
}