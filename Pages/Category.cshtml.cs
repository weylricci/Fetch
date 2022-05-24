using Microsoft.AspNetCore.Mvc.RazorPages;
using BookAppWithPurchase.Models;
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

            /*
            Book book = new Book();
            book.Title = "Title-Test";
            book.AuthorName = "Author-Test";
            book.PublisherName = "Publisher-Test";
            book.CategoryName = category;
            book.Isbn = "Isbn-Test";
            book.BookId = 2;
            */

            List<Book> bookList = new List<Book>();
            SqlConnection con = new SqlConnection(connectionString);

            string selectSQL = "";
            if (category == "All")
                selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData";
            else
                selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData where CategoryName = '" + category + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
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
