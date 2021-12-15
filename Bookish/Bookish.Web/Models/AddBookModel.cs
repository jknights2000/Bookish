using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookish.Web.Models
{
    public class AddBookModel
    {
        public int ISBN { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int BarCode { get; set; }
        public int NumberOfCopies { get; set; }

    }
}
