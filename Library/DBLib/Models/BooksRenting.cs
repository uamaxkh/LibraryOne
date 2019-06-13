using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace DBLib.Models
{
    public class BooksRenting
    {
        public Book Books { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }
        public DateTime TakingDate { get; set; }
        public DateTime ReturningDate { get; set; }
    }
}
