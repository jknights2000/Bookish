using Bookish.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bookish.Web.Models
{
    public class NoCopiesInfo
    {
        public List<UserBorrowed> UserBorrowed;
        public List<Book> AvailableBooks;
        public string Name;
        public int Copies;
        public int Avaiable;
        public int Borrowed;
        
    }
}
