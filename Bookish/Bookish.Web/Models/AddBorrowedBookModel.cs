using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookish.Web.Models
{
    public class AddBorrowedBookModel
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        //public DateTime DueDate { get; set; }
    }
}
