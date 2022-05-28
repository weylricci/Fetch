using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Data.SqlClient;

namespace Fetch.Pages
{
    public class LoginModel : PageModel
    {
        private string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";

        public void OnGet()
        {   
            string name = Request.Query["name"];
            if (string.IsNullOrEmpty(name))
                return;
            string password = Request.Query["password"];

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string selectSQL = "SELECT * from USERS WHERE UserName = '" + name
                + "' AND UserPassword = '" + password + "'";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            Data data = new Data();
            if (dr.Read()) {
                data.UserId = Convert.ToInt32(dr["UserId"]);
                data.UserName = dr["UserName"].ToString();
                data.UserPassword = dr["UserPassword"].ToString();
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            Console.WriteLine("Data : " + data.UserId + data.UserName + data.UserPassword);
            Console.WriteLine("jsonString : " + jsonString);
            Response.ContentType = "application/text";
            Response.ContentLength = jsonString.Length;
            Response.WriteAsync(jsonString).Wait();
            Response.CompleteAsync().Wait();
            
            con.Close();
            
        }
    }

    public class Data
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

    }
}
