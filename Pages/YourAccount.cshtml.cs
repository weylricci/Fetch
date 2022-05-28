using Microsoft.AspNetCore.Mvc.RazorPages;


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
        public void OnGet()
        {
            books = new List<BookData>();
            BookData book = new BookData();
            book.userId = 9;
            book.title = "Title";
            book.isbn = "ISBN";
            book.publisher = "Publisher";
            book.author = "Author";
            book.price = 100;

            books.Add(book);
        }
    }
}
