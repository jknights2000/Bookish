using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class EditBookModel
    {
        public int currentISBN { get; set; }
        public int newISBN { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Picture { get; set; }
        public int BarCode { get; set; }
    }

  
}
