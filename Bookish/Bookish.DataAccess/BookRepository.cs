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
        public T ExecuteGetSingleQuery<T>(string query,object param=null) => DatabaseConnection.Query<T>(query).Single();

        public void ExecuteInsertionQuery(string query) => DatabaseConnection.Query(query);


        public List<Book> GetListOfBooks(int ISBN) => ExecuteGetListQuery<Book>("select * from Books WHERE ISBN = " + ISBN);

        public string GetBookName(int ISBN) => ExecuteGetSingleQuery<string>("select BookName from BookINFO WHERE ISBN = " + ISBN);

        public string GetAuthorName(int ISBN) => ExecuteGetSingleQuery<string>("select Author from BookINFO WHERE ISBN = " + ISBN);

        public int GetBarCode(int ISBN) => ExecuteGetSingleQuery<int>("select BarCode from BookINFO WHERE ISBN = " + ISBN);

        public int GetTotalNumberOfCopies(int ISBN) => ExecuteGetSingleQuery<int>("select count(*) from Books WHERE ISBN = " + ISBN);

        public int GetNumberOfBorrowedCopies(int ISBN) => ExecuteGetSingleQuery<int>("SELECT count(books.id) from books, borrowed where books.id = borrowed.bookid and books.ISBN = " + ISBN);

        public int GetNumberOfAvailableCopies(int ISBN) => ExecuteGetSingleQuery<int>($"select(select count(*) from books where isbn = {ISBN}) - (select count(*) from books, borrowed where books.id = borrowed.bookid and books.ISBN = {ISBN})");

        public int GetMaxIDFromBooks() => ExecuteGetSingleQuery<int>("SELECT MAX(ID) FROM Books");

        public int GetISBNUsingBookID(int BookId) => ExecuteGetSingleQuery<int>($"select ISBN from books where id={BookId}");
        
        
        public List<UserBorrowed> GetListOfBorrowedCopies(int ISBN) => ExecuteGetListQuery<UserBorrowed>("Select AspNetUsers.UserName, books.id,borrowed.duedate from books, borrowed, AspNetUsers where books.id = borrowed.bookid and borrowed.userid = AspNetUsers.id and books.ISBN = " + ISBN);

        public List<PersonalBook> GetListOfBooksCurrentUser(string userid) => ExecuteGetListQuery<PersonalBook>($"Select Books.ID,BookInfo.BookName,Borrowed.duedate from Books,BookInfo,Borrowed,AspNetUsers where AspNetUsers.id = N'{userid}' and AspNetUsers.Id = Borrowed.userID and Borrowed.bookID = Books.ID and Books.ISBN = BookInfo.ISBN");

        public List<Book> GetListOfAvailableBooks(int ISBN) => ExecuteGetListQuery<Book>($"select ID,ISBN from (select * from books left join Borrowed on books.id = Borrowed.bookID where Borrowed.bookID is null) as ListOfAvailableBooks where ListOfAvailableBooks.ISBN = {ISBN}").ToList();


        public void InsertNewBookIntoBookInfo(int ISBN,string BookName,string Author,int BarCode) => ExecuteInsertionQuery($"INSERT INTO BookInfo (ISBN, BookName, Author, BarCode) VALUES ('{ISBN}', '{BookName}', '{Author}', '{BarCode}')");

        public void InsertNewBookIntoBooks(int Id, int ISBN) => ExecuteInsertionQuery($"INSERT INTO Books VALUES ({Id} , {ISBN})");

        public void InsertNewBookIntoBorrowed( string UserId, int BookId, DateTime DueDate) => ExecuteInsertionQuery($"insert into borrowed values ('{UserId}',{BookId},'{DueDate.ToString("yyyy-MM-dd")}')");

        public void EditBookIntoBookInfo(int currentISBN, int newISBN, string BookName, string Author, int BarCode) => ExecuteInsertionQuery($"UPDATE BOOKINFO SET ISBN='{newISBN}', BookName='{BookName}', Author='{Author}', BarCode='{BarCode}' WHERE ISBN={currentISBN}");
    }
}
