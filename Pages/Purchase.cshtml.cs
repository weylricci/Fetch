using Microsoft.AspNetCore.Mvc.RazorPages;
using Fetch.Models;

namespace Fetch.Pages
{
    public class PurchaseModel : PageModel
    {
        public List<Book> books = new List<Book>();
        public string amount = "";
        public void OnGet()
        {
            amount = Request.Query["amount"];

            string bookIDs = Request.Query["bookIDs"];
            string[] bookIDArray = bookIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string bookID in bookIDArray)
            {
                Book book = new Book();
                book = book.GetBookData(Convert.ToInt32(bookID));
                books.Add(book);
            }
        }

        public void OnPost()
        {
            amount = Request.Query["amount"];

            string bookIDs = Request.Form["bookIDs"];
            string[] bookIDArray = bookIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string bookID in bookIDArray)
            {
                Book book = new Book();
                book = book.GetBookData(Convert.ToInt32(bookID));
                books.Add(book);
            }
        }
    }
}
