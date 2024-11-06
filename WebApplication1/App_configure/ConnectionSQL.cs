namespace WebApplication1.App_configure
{
    public class ConnectionSQL
    {
        //pasar al settings.json
        private string _connectionString = @"data source=DESKTOP-KCGGJDV\\SQLEXPRESS;initial Catalog=ejemplo; Integrated Security=True;";

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}