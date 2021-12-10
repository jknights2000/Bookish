using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Bookish.DataAccess;
using Dapper;

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
        public IActionResult BookInfo(int ISBN)
        {
            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            
            //string sql = "select * from Books WHERE ISBN = " + ISBN;
            //List<Book> books = (List<Book>)db.Query<Book>(sql);

            string sql2 = "select BookName from BookINFO WHERE ISBN = " + ISBN;
            string name = db.Query<string>(sql2).Single();

            string sql3 = "select count(*) from Books WHERE ISBN = " + ISBN;
            int numberofcopies = db.Query<int>(sql3).Single();

            string sql4 = "SELECT count(books.id) from books, borrowed where books.id = borrowed.bookid and books.ISBN = " + ISBN;
            int numberoftaken = db.Query<int>(sql4).Single();
            // SELECT books.id from books, borrowed where book.id = borrowed.bookid
            //Select Accounts.accountname, book.id,borrowed.duedate from books, borrowed, accounts where books.id = borrowed.bookid and borrowed.userid = Accounts.id and books.ISBN =
            string sql5 = "Select Accounts.accountname, books.id,borrowed.duedate from books, borrowed, accounts where books.id = borrowed.bookid and borrowed.userid = Accounts.id and books.ISBN = " +ISBN;
            List<UserBorrowed> userb = (List<UserBorrowed>)db.Query<UserBorrowed>(sql5);

            NoCopiesInfo noCopiesInfo = new NoCopiesInfo();
            noCopiesInfo.UserBorrowed = userb;
            noCopiesInfo.Name = name;
            noCopiesInfo.copies = numberofcopies;
            noCopiesInfo.avaiable = numberofcopies - numberoftaken;
            return View(noCopiesInfo);
        }
        public IActionResult UserPage()
        {
            //User.Identity.
            return View();
        }
        public IActionResult Index()
        {
            return View();
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
