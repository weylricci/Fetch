using Fetch.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fetch.Pages
{
    public class BooksModel : PageModel
    {
        public List<Book> books = new List<Book>();
        public List<Category> categories = new List<Category>();
        public void OnGet()
        {
            Book book = new Book();
            books = book.GetBooks();

            Category category = new Category();
            categories = category.GetCategories();
        }
        public void OnPost()
        {
        }
    }
}