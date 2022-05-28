using Microsoft.AspNetCore.Mvc.RazorPages;
using Fetch.Models;
using System.Text.Json;
using System.Data.SqlClient;

namespace Fetch.Pages
{
    public class CategoryModel : PageModel
    {
        private string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";
        public void OnGet()
        {
            string category = Request.Query["category"];

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string selectSQL = "";
            if (category == "All")
                selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData";
            else
                selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData where CategoryName = '" + category + "'";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();

            List<Book> bookList = new List<Book>();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.BookId = Convert.ToInt32(dr["BookId"]);
                    book.Title = dr["Title"].ToString();
                    book.Isbn = dr["ISBN"].ToString();
                    book.PublisherName = dr["PublisherName"].ToString();
                    book.AuthorName = dr["AuthorName"].ToString();
                    book.CategoryName = dr["CategoryName"].ToString();
                    bookList.Add(book);
                }
            }
            con.Close();


            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(bookList, options);

            Response.ContentType = "application/text";
            Response.ContentLength = jsonString.Length;
            Response.WriteAsync(jsonString).Wait();
            Response.CompleteAsync().Wait();

        }
    }
}
