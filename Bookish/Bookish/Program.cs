using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Bookish.DataAccess;
using Dapper;

namespace Bookish
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var books = db.Query<Book>("select * from Books");
            foreach (Book book in books)
            {
                Console.WriteLine(book.ID);
                Console.WriteLine(book.ISBN);
            }
            //db.Execute("insert into Books(ID,ISBN) values (11,1234)");
            Console.ReadLine();
        }
    }
}