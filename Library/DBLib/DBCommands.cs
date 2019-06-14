using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DBLib.Models;
using Library.Models;

namespace DBLib
{
    public class DBCommands
    {
        public static List<Book> GetAllBooks()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.ToList();
            }
        }

        public static Book GetBookById(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Find(Id);
            }
        }
    }
}
