using System;
using System.Text.RegularExpressions;
using System.Web.Services;
using WebApplication1.App_Data;
using WebApplication1.App_Entities.Model;
using WebApplication1.App_Entities.Request;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static Client HandleLogin(LoginRequest credentials)
        {
            try
            {

                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(credentials.client_email, emailPattern)) throw new ArgumentException("El correo electrónico no es válido.");


                var authData = new AuthData();

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