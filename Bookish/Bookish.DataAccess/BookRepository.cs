using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bookish.DataAccess
{
    public class BookRepository
    {
        public IDbConnection DatabaseConnection;
        public BookRepository(IDbConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }

        public List<T> ExecuteGetListQuery<T>(string query) => DatabaseConnection.Query<T>(query).ToList();
        public T ExecuteGetSingleQuery<T>(string query) => DatabaseConnection.Query<T>(query).Single();


        public List<Book> GetListOfBooks(int ISBN) => ExecuteGetListQuery<Book>("select * from Books WHERE ISBN = " + ISBN);

        public string GetBookName(int ISBN) => ExecuteGetSingleQuery<string>("select BookName from BookINFO WHERE ISBN = " + ISBN);

        public string GetAuthorName(int ISBN) => ExecuteGetSingleQuery<string>("select Author from BookINFO WHERE ISBN = " + ISBN);

        public int GetBarCode(int ISBN) => ExecuteGetSingleQuery<int>("select BarCode from BookINFO WHERE ISBN = " + ISBN);

        public int GetTotalNumberOfCopies(int ISBN) => ExecuteGetSingleQuery<int>("select count(*) from Books WHERE ISBN = " + ISBN);

        public int GetNumberOfBorrowedCopies(int ISBN) => ExecuteGetSingleQuery<int>("SELECT count(books.id) from books, borrowed where books.id = borrowed.bookid and books.ISBN = " + ISBN);

        public int GetNumberOfAvailableCopies(int ISBN) => ExecuteGetSingleQuery<int>($"select(select count(*) from books where isbn = {ISBN}) - (select count(*) from books, borrowed where books.id = borrowed.bookid and books.ISBN = {ISBN})");

        public List<UserBorrowed> GetListOfBorrowedCopies(int ISBN) => ExecuteGetListQuery<UserBorrowed>("Select AspNetUsers.UserName, books.id,borrowed.duedate from books, borrowed, AspNetUsers where books.id = borrowed.bookid and borrowed.userid = AspNetUsers.id and books.ISBN = " + ISBN);

        public List<PersonalBook> GetListOfBooksCurrentUser(string userid) => ExecuteGetListQuery<PersonalBook>($"Select Books.ID,BookInfo.BookName,Borrowed.duedate from Books,BookInfo,Borrowed,AspNetUsers where AspNetUsers.id = N'{userid}' and AspNetUsers.Id = Borrowed.userID and Borrowed.bookID = Books.ID and Books.ISBN = BookInfo.ISBN");

    }
}