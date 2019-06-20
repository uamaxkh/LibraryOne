using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace DBLib.Models
{
    public class BooksRenting : BaseEntity
    {
        public Book Book { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? TakingDate { get; set; }

        public DateTime? ReturningDate { get; set; }
    }
}
