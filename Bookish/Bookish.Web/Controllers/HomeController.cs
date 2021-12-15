﻿using Bookish.Web.Models;
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
            BookRepository bookRepo = new BookRepository(db);

            NoCopiesInfo noCopiesInfo = new NoCopiesInfo();
            noCopiesInfo.UserBorrowed = bookRepo.GetListOfBorrowedCopies(ISBN);
            noCopiesInfo.Name = bookRepo.GetBookName(ISBN);
            noCopiesInfo.copies = bookRepo.GetTotalNumberOfCopies(ISBN);
            noCopiesInfo.avaiable = bookRepo.GetNumberOfAvailableCopies(ISBN);
            noCopiesInfo.borrowed = bookRepo.GetNumberOfBorrowedCopies(ISBN);
            return View(noCopiesInfo);
        }
        [HttpGet]
        public IActionResult AddBookForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBookForm(AddBookModel addBook)
        {
            string BookName = addBook.BookName;
            string Author = addBook.Author;
            int ISBN = addBook.ISBN;
            int BarCode = addBook.BarCode;
            int NumberOfCopies = addBook.NumberOfCopies;

            IDbConnection db = new SqlConnection("Server = localhost; Database = Bookish; Integrated Security = True; MultipleActiveResultSets = true;");
            db.Execute($"INSERT INTO BookInfo (ISBN, BookName, Author, BarCode) VALUES ('{ISBN}', '{BookName}', '{Author}', '{BarCode}')");
            int MaxId = db.Query<int>("SELECT MAX(ID) FROM Books").Single();

            for (int i = 1; i <= NumberOfCopies; i++)
            {
                string MaxIdQuery = $"INSERT INTO Books VALUES ({MaxId + i} , {ISBN})";
                db.Execute(MaxIdQuery);
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
