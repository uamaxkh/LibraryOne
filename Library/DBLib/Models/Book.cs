using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TitlePic { get; set; }
        public short Year { get; set; }
        public bool LibraryReading { get; set; }
        public int Quantity { get; set; }
        public int Pages { get; set; }
        public string ISBN { get; set; }
        public Section Section { get; set; }
        public Publisher Publisher { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<BooksRenting> BooksRenting { get; set; }
    }
}
