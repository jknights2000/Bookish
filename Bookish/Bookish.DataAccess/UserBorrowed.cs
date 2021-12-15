using System;
using System.Collections.Generic;
using System.Text;

namespace Bookish.DataAccess
{
    public class UserBorrowed
    {
        public string UserName { get; set; }
        public int ID { get; set; }
        public DateTime Duedate { get; set; }
    }
}
