using System.Data.SqlClient;

namespace Fetch.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        private string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";

        public List<Category> GetCategories()
        {
            List<Category> catList = new List<Category>();
            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = "select * from Category";
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Category cat = new Category();
                    cat.CategoryId = dr["CategoryId"].ToString();
                    cat.CategoryName = dr["CategoryName"].ToString();
                    catList.Add(cat);
                }
            }
            con.Close();

            return catList;
        }

    }
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string PublisherName { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }

        private string connectionString = "Data Source=ALOK\\SQLEXPRESS; Initial Catalog=Books; Integrated Security=true;";

        public List<Book> GetBooks()
        {
            List<Book> bookList = new List<Book>();
            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData";
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
                dr.Close();
            }

            con.Close();

            return bookList;
        }

        public Book GetBookData(int bookId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = "select BookId, Title, Isbn, PublisherName, AuthorName, CategoryName from GetBookData where BookId = " + bookId;
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
            Book book = new Book();
            if (dr != null)
            {
                while (dr.Read())
                {
                    book.BookId = Convert.ToInt32(dr["BookId"]);
                    book.Title = dr["Title"].ToString();
                    book.Isbn = dr["ISBN"].ToString();
                    book.PublisherName = dr["PublisherName"].ToString();
                    book.AuthorName = dr["AuthorName"].ToString();
                    book.CategoryName = dr["CategoryName"].ToString();
                }
                dr.Close();
            }
            con.Close();

            return book;
        }
    }
}
