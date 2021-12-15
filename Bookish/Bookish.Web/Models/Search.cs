using Bookish.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookish.Web.Models
{
    public class Search
    {
        public SearchBy searchby { get; set; }
        public string searchstring { get; set; }
        public List<BookInfo> Results { get; set; } = new List<BookInfo>();
    }
    public enum SearchBy
    {
        Author,
        BookName,
        ISBN,
        Barcode
    }
}