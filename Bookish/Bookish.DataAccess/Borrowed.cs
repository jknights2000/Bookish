using System;
using System.Collections.Generic;
using System.Text;

namespace Bookish.DataAccess
{
    public class Borrowed
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime Duedate { get; set; }
    }
}
