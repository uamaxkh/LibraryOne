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

        public static bool AddBook(Book book, ICollection<Guid> AuthorsId, string PublisherName, string SectionName)
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    book.Publisher = GetPublisherByName(PublisherName);

                    if (book.Publisher == null)
                        throw new Exception("");

                    db.Publishers.Attach(book.Publisher);
                    book.Section = GetSectionByName(SectionName);

                    if (book.Section == null)
                        throw new Exception("");

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

        private static Section GetSectionByName(string Name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Sections.Where(a => a.Name == Name).SingleOrDefault();
            }
        }

        private static Publisher GetPublisherByName(string Name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Publishers.Where(a => a.Name == Name).SingleOrDefault();
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

        public static ApplicationUser GetUserById(string Id, ApplicationDbContext applicationDbContext = null)
        {
            ApplicationUser user;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                user = applicationDbContext.Users.Where(a => a.Id == Id).SingleOrDefault();
                applicationDbContext.Dispose();
            }
            else
            {
                user = applicationDbContext.Users.Where(a => a.Id == Id).SingleOrDefault();
            }
            
            return user;
        }

        public static Book GetBookById(Guid Id, ApplicationDbContext applicationDbContext = null)
        {
            Book book;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                book = applicationDbContext.Books.Find(Id);
                applicationDbContext.Dispose();
            }
            else
            {
                book = applicationDbContext.Books.Find(Id);
            }

            return book;
        }

        //public static Book GetBookOrderByBookAndUserId(Guid BookId, string UserId, ApplicationDbContext applicationDbContext = null)
        //{
        //    BooksRenting booksRenting;

        //    if (applicationDbContext == null)
        //    {
        //        applicationDbContext = new ApplicationDbContext();
        //        book = applicationDbContext.Books.Find(Id);
        //        applicationDbContext.Dispose();
        //    }
        //    else
        //    {
        //        book = applicationDbContext.Books.Find(Id);
        //    }

        //    return book;
        //}

        public static bool saveBookOrderToDB(Guid BookId, string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = GetUserById(UserId, db);
                db.Users.Attach(applicationUser);
                Book book = GetBookById(BookId, db);
                db.Books.Attach(book);

                BooksRenting booksRenting = new BooksRenting() { ApplicationUser = applicationUser,
                    Book = book, OrderDate = DateTime.Now};
                db.BooksRenting.Add(booksRenting);
                db.SaveChanges();
                return true;
            }
        }

        public static bool cancelBookOrderInDB(Guid BookId, string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = GetUserById(UserId, db);
                db.Users.Attach(applicationUser);
                Book book = GetBookById(BookId, db);
                db.Books.Attach(book);

                BooksRenting booksRenting = new BooksRenting()
                {
                    ApplicationUser = applicationUser,
                    Book = book,
                    OrderDate = DateTime.Now
                };
                db.BooksRenting.Add(booksRenting);
                db.SaveChanges();
                return true;
            }
        }
    }
}
