using System;
using System.Collections.Generic;
using System.Text;

namespace Bookish.DataAccess
{
    public class PersonalBook
    {
        public int ID { get; set; }
        public string BookName { get; set; }

        public DateTime Duedate { get; set; }
    }
}
