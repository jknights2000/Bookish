using System;
using System.Collections.Generic;
using System.Text;

namespace Bookish.DataAccess
{
    public class BookInfo
    {
        public int ISBN { get; set; } 
        public string BookName { get; set; }
        public string Author { get; set; }
        public int BarCode { get; set; }
        public string Picture { get; set; }
    }
}
