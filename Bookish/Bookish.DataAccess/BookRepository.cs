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

        public List<Book> GetListOfBooks(int ISBN)
        {
            string query = "select * from Books WHERE ISBN = " + ISBN;
            return (List<Book>)DatabaseConnection.Query<Book>(query);
        }

        public string GetBookName(int ISBN)
        {
            string query = "select BookName from BookINFO WHERE ISBN = " + ISBN;
            return DatabaseConnection.Query<string>(query).Single();
        }

        public int GetTotalNumberOfCopies(int ISBN)
        {
            string query = "select count(*) from Books WHERE ISBN = " + ISBN;
            return DatabaseConnection.Query<int>(query).Single();
        }

        public int GetNumberOfBorrowedCopies(int ISBN)
        {
            string query = "SELECT count(books.id) from books, borrowed where books.id = borrowed.bookid and books.ISBN = " + ISBN;
            return DatabaseConnection.Query<int>(query).Single();
        }

        public int GetNumberOfAvailableCopies(int ISBN)
        {
            string query = $"select(select count(*) from books where isbn = {ISBN}) - (select count(*) from books, borrowed where books.id = borrowed.bookid and books.ISBN = {ISBN})";
            return DatabaseConnection.Query<int>(query).Single();
        }
        public List<UserBorrowed> GetListOfBorrowedCopies(int ISBN)
        {
            string query = "Select Accounts.accountname, books.id,borrowed.duedate from books, borrowed, accounts where books.id = borrowed.bookid and borrowed.userid = Accounts.id and books.ISBN = " + ISBN;
            return (List<UserBorrowed>)DatabaseConnection.Query<UserBorrowed>(query);
        }
    }
}