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
        public string Name;
        public int copies;
        public int avaiable;
        public int borrowed;
    }
}
