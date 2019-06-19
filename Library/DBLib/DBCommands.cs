using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DBLib.Models;
using Library.Models;
using System.IO;
using System.Web;
using DBLib;

namespace DBLib
{
    public class DBCommands
    {
        public static List<Book> GetAllBooks()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).ToList();
            }
        }

        public static Book GetBookById(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Book book = db.Books.Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).SingleOrDefault(b => b.Id == Id);
                return book;
            }
        }

        public static List<Author> GetAllAuthors()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.ToList();
            }
        }

        public static List<Section> GetAllSections()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Sections.ToList();
            }
        }

        public static List<Publisher> GetAllPublishers()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Publishers.ToList();
            }
        }

        public static bool AddBook(Book book, ICollection<Guid> AuthorsId, Guid PublisherId, Guid SectionId)
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    book.Publisher = GetPublisherById(PublisherId);
                    db.Publishers.Attach(book.Publisher);
                    book.Section = GetSectionById(SectionId);
                    db.Sections.Attach(book.Section);

                    foreach (var authorId in AuthorsId)
                    {
                        Author author = GetAuthorById(authorId);
                        if (author == null)
                            throw new Exception("Був обраний неіснуючий автор!");
                        db.Authors.Attach(author);
                        book.Authors.Add(author);
                    }

                    db.Books.Add(book);
                    int a = db.SaveChanges();
                }
            return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Author GetAuthorById(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.Find(Id);
            }
        }

        public static Author GetAuthorByName(string Name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.Where(a => a.Name == Name).SingleOrDefault();
            }
        }

        public static Publisher GetPublisherById(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Publishers.Find(Id);
            }
        }

        public static Section GetSectionById(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Sections.Find(Id);
            }
        }

        public static string AddAuthor(string Name)
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    bool isExits = AuthorIsExistByName(db, Name);

                    if (isExits)
                    {
                        return "0";
                    }

                    var newAuthor = new Author() { Name = Name };
                    db.Authors.Add(newAuthor);
                    db.SaveChanges();
                    return newAuthor.Id.ToString();
                }
            }
            catch
            {
                return "-1";
            }
        }

        public static bool AuthorIsExistByName(ApplicationDbContext db, string Name)
        {
            int isExist = db.Authors.Where(a => a.Name.Contains(Name)).Count();
            if (isExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
