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
    //public class DBCover
    //{
    //    public ApplicationDbContext Context { get; set; }

    //    public DBCover(ApplicationDbContext extContext = null)
    //    {
    //        if(extContext == null)
    //    }
    //}

    public class DBCommands
    {
        public static List<Book> GetAllBooks()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.OrderByDescending(b => b.AddingDate).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).ToList();
            }
        }

        public static List<Book> GetBooksRange(int startNum, int takenNum, string sortingBy, string sortingOrder)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                --startNum;
                
                switch (sortingBy)
                {
                    case "Title":
                        if(sortingOrder == "ASC")
                            return db.Books.OrderBy(b => b.Title).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                        else
                            return db.Books.OrderByDescending(b => b.Title).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                    case "Author":
                        if (sortingOrder == "ASC")
                        {
                            var allBooks = GetAllBooks();
                            return allBooks.OrderBy(b => b.getFirstAuthorName).Skip(startNum).Take(takenNum).ToList();
                        }
                        else
                        {
                            var allBooks = GetAllBooks();
                            return allBooks.OrderByDescending(b => b.getFirstAuthorName).Skip(startNum).Take(takenNum).ToList();
                        }
                    case "Publisher":
                        if (sortingOrder == "ASC")
                            return db.Books.OrderBy(b => b.Publisher.Name).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                        else
                            return db.Books.OrderByDescending(b => b.Publisher.Name).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                    case "PublicationDate":
                        if (sortingOrder == "ASC")
                            return db.Books.OrderBy(b => b.Year).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                        else
                            return db.Books.OrderByDescending(b => b.Year).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                    case "AddingDate":
                        if (sortingOrder == "ASC")
                            return db.Books.OrderBy(b => b.AddingDate).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                        else
                            return db.Books.OrderByDescending(b => b.AddingDate).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
                }

                return db.Books.OrderByDescending(b => b.AddingDate).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Skip(startNum).Take(takenNum).ToList();
            }
        }

        public static int GetBooksCount()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Count();
            }
        }

        public static Book GetBookWithAdditionalInfoById(Guid Id)
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
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    book.Publisher = GetPublisherByName(PublisherName);

                    if (book.Publisher == null)
                        throw new ExceptionExt("Такого видавця не існує");

                    db.Publishers.Attach(book.Publisher);
                    book.Section = GetSectionByName(SectionName);

                    if (book.Section == null)
                        throw new ExceptionExt("Такого розділу не існує");

                    db.Sections.Attach(book.Section);

                    foreach (var authorId in AuthorsId)
                    {
                        Author author = GetAuthorById(authorId);
                        if (author == null)
                            throw new ExceptionExt("Був обраний неіснуючий автор!");
                        db.Authors.Attach(author);
                        book.Authors.Add(author);
                    }

                    db.Books.Add(book);
                    int a = db.SaveChanges();
                }
                return true;
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

        public static bool saveBookOrderToDB(Guid BookId, string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser applicationUser = GetUserById(UserId, db);
                Book book = GetBookById(BookId, db);

                if(book.FreeBooksCount() < 1)
                {
                    throw new Exception("Неможливо замовити книгу, кількість менша за 1");
                }

                BooksRenting booksRenting = new BooksRenting() { ApplicationUser = applicationUser,
                    Book = book, OrderDate = DateTime.Now};
                db.BooksRenting.Add(booksRenting);
                db.SaveChanges();
                return true;
            }
        }

        public static BooksRenting GetBookOrderByBookAndUserId(Guid BookId, string UserId, ApplicationDbContext applicationDbContext = null)
        {
            BooksRenting booksRenting;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();

                booksRenting = applicationDbContext.BooksRenting.Where(br => br.ApplicationUser.Id == UserId
                    && br.Book.Id == BookId && br.TakingDate == null).FirstOrDefault();

                applicationDbContext.Dispose();
            }
            else
            {
                booksRenting = applicationDbContext.BooksRenting.Where(br => br.ApplicationUser.Id == UserId
                    && br.Book.Id == BookId && br.TakingDate == null).FirstOrDefault();
            }

            return booksRenting;
        }

        public static bool cancelBookOrderInDB(Guid BookId, string UserId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                BooksRenting booksRenting = GetBookOrderByBookAndUserId(BookId, UserId, db);
                db.BooksRenting.Remove(booksRenting);
                db.SaveChanges();
                return true;
            }
        }

        public static int FreeBookCountById(Guid Id, ApplicationDbContext dbContext = null)
        {
            int takenBookCount;
            int availableBookCount;
            int freeBookCount;

            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();

                takenBookCount = dbContext.BooksRenting.Where(b => b.Book.Id == Id && b.ReturningDate == null).Count();
                availableBookCount = dbContext.Books.Where(b => b.Id == Id).Select(b => b.Quantity).Single();
                freeBookCount = availableBookCount - takenBookCount;

                dbContext.Dispose();
            }
            else
            {
                takenBookCount = dbContext.BooksRenting.Where(b => b.Book.Id == Id && b.ReturningDate == null).Count();
                availableBookCount = dbContext.Books.Where(b => b.Id == Id).Select(b => b.Quantity).Single();
                freeBookCount = availableBookCount - takenBookCount;
            }

            if(freeBookCount < 0)
            {
                throw new Exception("Кількість вільних книг від'ємна");
            }

            return freeBookCount;
        }

        public static void addPublisher(Publisher publisher)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
            }
        }

        public static void addSection(Section section)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Sections.Add(section);
                db.SaveChanges();
            }
        }
    }
}
