using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Fetch.Pages
{
    public class SignUpModel : PageModel
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

            string selectSQL = "select * from USERS WHERE UserName = '" + name + "'";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            Data2 data = new Data2();
            if (dr.Read())
            {
                data.UserId = Convert.ToInt32(dr["UserId"]);
                data.UserName = dr["UserName"].ToString();
                data.UserPassword = dr["UserPassword"].ToString();
            }
            con.Close();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            Console.WriteLine("Data : " + data.UserId + data.UserName + data.UserPassword);
            Console.WriteLine("jsonString : " + jsonString);
            Response.ContentType = "application/text";
            Response.ContentLength = jsonString.Length;
            Response.WriteAsync(jsonString).Wait();
            Response.CompleteAsync().Wait();
        }

    }
    public class Data2
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

    }

}




