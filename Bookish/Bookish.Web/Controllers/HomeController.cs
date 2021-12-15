using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Bookish.DataAccess;
using Dapper;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult BookPage()
        {
            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            List<BookInfo> books = (List<BookInfo>)db.Query<BookInfo>("select * from BookInfo ORDER BY bookname");
            return View(books);
        }
        [HttpGet]
        public IActionResult BookInfo(int ISBN)
        {
            return View(getNoCopiesInfo(ISBN));
        }
        [HttpPost]
        public IActionResult BorrowedBook(int bookID)
        {
            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            BookRepository bookRepo = new BookRepository(db);

            int ISBN = bookRepo.GetISBNUsingBookID(bookID);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DateTime dueDateTime = DateTime.Now.AddDays(30);
            bookRepo.InsertNewBookIntoBorrowed(userId, bookID, dueDateTime);
            return RedirectToAction("BookInfo",new { ISBN });
        }
        public NoCopiesInfo getNoCopiesInfo(int ISBN)
        {
            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            BookRepository bookRepo = new BookRepository(db);

            NoCopiesInfo noCopiesInfo = new NoCopiesInfo();
            noCopiesInfo.UserBorrowed = bookRepo.GetListOfBorrowedCopies(ISBN);
            noCopiesInfo.AvailableBooks = bookRepo.GetListOfAvailableBooks(ISBN);
            noCopiesInfo.Name = bookRepo.GetBookName(ISBN);
            noCopiesInfo.Copies = bookRepo.GetTotalNumberOfCopies(ISBN);
            noCopiesInfo.Avaiable = bookRepo.GetNumberOfAvailableCopies(ISBN);
            noCopiesInfo.Borrowed = bookRepo.GetNumberOfBorrowedCopies(ISBN);
            return noCopiesInfo;
        }

        [HttpGet]
        public IActionResult AddBookForm(AddBorrowedBookModel borrowedBook)
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBookForm(AddBookModel addBook)
        {

            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            BookRepository bookRepo = new BookRepository(db);

            bookRepo.InsertNewBookIntoBookInfo(addBook.ISBN, addBook.BookName, addBook.Author, addBook.BarCode);
            int MaxId = bookRepo.GetMaxIDFromBooks();

            for (int i = 1; i <= addBook.NumberOfCopies; i++)
            {
                bookRepo.InsertNewBookIntoBooks(MaxId + i, addBook.ISBN);
            }
        /*    Response.Redirect("~/")*/
            return View();
        }

        public IActionResult UserPage()
        {
            //User.Identity.
            return View();
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
                BookRepository bookRepo = new BookRepository(db);
                List<PersonalBook> output = bookRepo.GetListOfBooksCurrentUser(userid);
                return View(output);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
