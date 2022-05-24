using Microsoft.AspNetCore.Mvc.RazorPages;
using BookAppWithPurchase.Models;

namespace BookAppWithPurchase.Pages
{
    public class PurchaseModel : PageModel
    {
        public List<Book> books = new List<Book>();
        public string amount = "";
        public void OnGet()
        {
            QueryString query = Request.QueryString;
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
