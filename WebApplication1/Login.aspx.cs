using System;
using System.Web.Services;
using WebApplication1.App_Entities.Request;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string HandleLogin(LoginRequest credentials)
        {

            return credentials.Email;
        }

    }
}