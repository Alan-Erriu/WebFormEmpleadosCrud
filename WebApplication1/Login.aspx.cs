using System;
using System.Web.Services;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string HandleLogin()
        {

            return "email";
        }

    }
}