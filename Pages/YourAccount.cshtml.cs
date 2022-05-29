using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Fetch.Pages
{
    public class BookData
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public string publisher { get; set; }
        public string author { get; set; }
        public float price { get; set; }
    }
    public class YourAccountModel : PageModel
    {
        public List<BookData> books;
        private string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";
        public void OnGet()
        {
            books = new List<BookData>();
            string userId = Request.Query["userId"];
            if (string.IsNullOrEmpty(userId))
                return;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string selectSQL = "select * from USERACCOUNT WHERE UserId=" + userId;
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    BookData book = new BookData();
                    book.userId = Convert.ToInt32(dr["UserId"]);
                    book.title = dr["Title"].ToString();
                    book.isbn = dr["ISBN"].ToString();
                    book.publisher = dr["Publisher"].ToString();
                    book.author = dr["Author"].ToString();
                    book.price = Convert.ToInt32(dr["Price"]);
                    books.Add(book);
                }
                dr.Close();
            }

            con.Close();

            Response.WriteAsync("Success").Wait();
            Response.CompleteAsync().Wait();

        }
    }
}
