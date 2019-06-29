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

    public static class DBCommands
    {
        public static List<Book> SearchBookByTitle(string searchString)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Where(b=>b.Title.ToLower().Contains(searchString.ToLower()) && b.Deleted == false).ToList();
            }
        }

        public static List<Author> SearchAuthorByName(string searchString)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.Where(b => b.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
        }

        public static Author GetAuthorByIdWithBooks(Guid? Id)
        {
            if (Id == null)
                return null;
            Author author;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                author = db.Authors.Include(a => a.Books)
                    .Where(a => a.Id == Id).FirstOrDefault();
                if(author != null)
                {
                    List<Book> notDeletedbooks = author.Books
                        .Where(b => b.Deleted == false).ToList();

                    if (notDeletedbooks != null)
                    {
                        author.Books = notDeletedbooks;
                    }
                    else
                    {
                        author.Books = new List<Book>();
                    }
                }


                return author;
            }
        }

        public static Publisher GetPublisherByIdWithBooks(Guid? Id)
        {
            if (Id == null)
                return null;

            Publisher publisher;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                publisher = db.Publishers.Include(a => a.Books)
                    .Where(a => a.Id == Id).FirstOrDefault();
                if(publisher != null)
                {
                    List<Book> notDeletedbooks = publisher.Books
                        .Where(b => b.Deleted == false).ToList();

                    if (notDeletedbooks != null)
                    {
                        publisher.Books = notDeletedbooks;
                    }
                    else
                    {
                        publisher.Books = new List<Book>();
                    }
                }


                return publisher;
            }
        }

        public static Section GetSectionByIdWithBooks(Guid? Id)
        {
            if (Id == null)
                return null;

            Section section;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                section = db.Sections.Include(a => a.Books)
                    .Where(a => a.Id == Id).FirstOrDefault();
                if(section != null)
                {
                    List<Book> notDeletedbooks = section.Books
                        .Where(b => b.Deleted == false).ToList();

                    if (notDeletedbooks != null)
                    {
                        section.Books = notDeletedbooks;
                    }
                    else
                    {
                        section.Books = new List<Book>();
                    }
                }

                return section;
            }
        }

        public static List<string> GetAllTitles()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Where(b => b.Deleted == false).OrderBy(a => a.Title).Select(b => b.Title).ToList();
            }
        }

        public static List<string> GetAllAuthorsName()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.OrderBy(a => a.Name).Select(a => a.Name).ToList();
            }
        }

        public static List<Book> GetAllBooks()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Where(b => b.Deleted == false).OrderByDescending(b => b.AddingDate).Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).ToList();
            }
        }

        public static List<Book> GetBooksRange(int startNum, int takenNum, string sortingBy, string sortingOrder)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                --startNum;
                var allBooks = GetAllBooks();
                IOrderedEnumerable<Book> orderedBooks = allBooks.OrderBy(b => b.Title);

                switch (sortingBy)
                {
                    case "Title":
                        if(sortingOrder == "ASC")
                        {
                            orderedBooks = allBooks.OrderBy(b => b.Title); break;
                        }
                        else
                        {
                            orderedBooks = allBooks.OrderByDescending(b => b.Title); break;
                        }
                    case "Author":
                        if (sortingOrder == "ASC")
                        {
                            orderedBooks = allBooks.OrderBy(b => b.getFirstAuthorName); break;
                        }
                        else
                        {
                            orderedBooks = allBooks.OrderByDescending(b => b.getFirstAuthorName); break;
                        }
                    case "Publisher":
                        if (sortingOrder == "ASC")
                        {
                            orderedBooks = allBooks.OrderBy(b => b.Publisher.Name); break;
                        }
                        else
                        {
                            orderedBooks = allBooks.OrderByDescending(b => b.Publisher.Name); break;
                        }
                    case "PublicationDate":
                        if (sortingOrder == "ASC")
                        {
                            orderedBooks = allBooks.OrderBy(b => b.Year); break;
                        }
                        else
                        {
                            orderedBooks = allBooks.OrderByDescending(b => b.Year); break;
                        }
                    case "AddingDate":
                        if (sortingOrder == "ASC")
                        {
                            orderedBooks = allBooks.OrderBy(b => b.AddingDate); break;
                        }
                        else
                        {
                            orderedBooks = allBooks.OrderByDescending(b => b.AddingDate); break;
                        }
                }

                return orderedBooks.Skip(startNum).Take(takenNum).ToList();
            }
        }

        public static int GetBooksCount()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Books.Where(b => b.Deleted == false).Count();
            }
        }

        public static Book GetBookWithAdditionalInfoById(Guid Id, ApplicationDbContext applicationDbContext = null)
        {
            Book book;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                book = applicationDbContext.Books.Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Include(b => b.Comments).SingleOrDefault(b => b.Id == Id);
                applicationDbContext.Dispose();
            }
            else
            {
                book = applicationDbContext.Books.Include(b => b.Authors).Include(b => b.Section).Include(b => b.Publisher).Include(b => b.Comments).SingleOrDefault(b => b.Id == Id);
            }

            return book;
        }

        public static void AddComment(string userId, Guid id, string commentText)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = GetUserById(userId, db);
                Book book = GetBookById(id, db);


                Comment comment = new Comment() { ApplicationUser = user, Book = book, CommentText = commentText };
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public static List<Comment> GetCommentsByBookId(Guid bookId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Comments.OrderByDescending(b => b.Date).Include(b => b.ApplicationUser).Where(b => b.Book.Id == bookId).ToList();
            }
        }

        public static List<Author> GetAllAuthors()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.OrderBy(a => a.Name).ToList();
            }
        }

        public static List<Section> GetAllSections()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Sections.OrderBy(a => a.Name).ToList();
            }
        }

        public static List<Publisher> GetAllPublishers()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Publishers.OrderBy(a => a.Name).ToList();
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

        public static Author GetAuthorById(Guid Id, ApplicationDbContext applicationDbContext = null)
        {
            Author author;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                author = applicationDbContext.Authors.Find(Id);
                applicationDbContext.Dispose();
            }
            else
            {
                author = applicationDbContext.Authors.Find(Id);
            }

            return author;
        }

        public static Author GetAuthorByName(string Name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Authors.Where(a => a.Name == Name).SingleOrDefault();
            }
        }

        public static Publisher GetPublisherById(Guid Id, ApplicationDbContext applicationDbContext = null)
        {
            Publisher publisher;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                publisher = applicationDbContext.Publishers.Find(Id);
                applicationDbContext.Dispose();
            }
            else
            {
                publisher = applicationDbContext.Publishers.Find(Id);
            }

            return publisher;
        }

        public static Section GetSectionById(Guid Id, ApplicationDbContext applicationDbContext = null)
        {
            Section section;

            if (applicationDbContext == null)
            {
                applicationDbContext = new ApplicationDbContext();
                section = applicationDbContext.Sections.Find(Id);
                applicationDbContext.Dispose();
            }
            else
            {
                section = applicationDbContext.Sections.Find(Id);
            }

            return section;
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
        //!!!
        public static void cancelBookOrderById(Guid bookRentingId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var bookRenting = db.BooksRenting.Find(bookRentingId);

                if(bookRenting != null)
                {
                    db.BooksRenting.Remove(bookRenting);

                    db.SaveChanges();
                }
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

        //with verifying user
        public static void deleteComment(string userId, Guid commentId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Comment comment = db.Comments.Include(c => c.ApplicationUser)
                    .Where(c => c.Id == commentId).FirstOrDefault();

                if(comment.ApplicationUser.Id == userId)
                {
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                }
            }
        }
        
        public static List<BooksRenting> getUserOrderedBooks(string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.BooksRenting.OrderBy(br => br.OrderDate).Include(br => br.Book).Where(br => br.ApplicationUser.Id == userId
                    && br.ReturningDate == null).ToList();
            }
        }
        
        public static bool UserHasPenalty(string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var rentings = db.BooksRenting.Where(br => br.ApplicationUser.Id == userId).ToList();
                foreach(var rent in rentings)
                {
                    if (rent.IsHasPenalty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        //public static List<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> getOnlyRegisteredUsers()
        public static List<ApplicationUser> getOnlyRegisteredUsers()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string librarianRoleId = db.Roles.Where(r => r.Name == "librarian").Select(r => r.Id).SingleOrDefault();

                var users = db.Users.OrderBy(u => u.Surname).Include(u => u.BooksRenting).Where(u => u.Roles.All(r => r.RoleId != librarianRoleId)).ToList();

                return users;
            }
        }
        
        public static void TakeBook(string orderForTakingId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                BooksRenting booksrenting = db.BooksRenting.Find(Guid.Parse(orderForTakingId));
                booksrenting.TakingDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static void ReturnBook(string orderForTakingId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            { 
                BooksRenting booksrenting = db.BooksRenting.Find(Guid.Parse(orderForTakingId));
                booksrenting.ReturningDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static bool EditBook(Book editedBook, Guid SectionId, Guid editedPublisherId, Guid[] editedAuthorsId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Book book = GetBookWithAdditionalInfoById(editedBook.Id, db);
                if (book != null)
                {
                    Section section = GetSectionById(SectionId, db);
                    Publisher publisher = GetPublisherById(editedPublisherId, db);

                    List<Author> authors = new List<Author>();

                    foreach(var authorId in editedAuthorsId)
                    {
                        Author author = GetAuthorById(authorId, db);
                        authors.Add(author);
                    }

                    book.ISBN = editedBook.ISBN;
                    book.LibraryReading = editedBook.LibraryReading;
                    book.Pages = editedBook.Pages;
                    book.Quantity = editedBook.Quantity;
                    book.Title = editedBook.Title;
                    book.Year = editedBook.Year;

                    book.Publisher = publisher;
                    book.Section = section;
                    book.Authors = authors;

                    db.Entry(book).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            return true;
        }

        public static void DeleteBook(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Book book = GetBookById(Id, db);
                book.Deleted = true;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void ReturnBook(Guid Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Book book = GetBookById(Id, db);
                book.Deleted = false;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void SetUserBlocking(string userId, bool setBlock)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = GetUserById(userId, db);
                user.IsBanned = setBlock;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static List<ApplicationUser> getLibrarians()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string librarianRoleId = db.Roles.Where(r => r.Name == "librarian").Select(r => r.Id).SingleOrDefault();

                var users = db.Users.OrderBy(u => u.Surname).Where(u => u.Roles.Any(r => r.RoleId == librarianRoleId)).ToList();

                return users;
            }
        }

        public static List<ApplicationUser> getAllUsers()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var users = db.Users.Include(u => u.Roles).OrderBy(u => u.Surname).ToList();

                return users;
            }
        }

        public static string getLibrarianRoleId()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string librarianRoleId = db.Roles.Where(r => r.Name == "librarian").Select(r => r.Id).SingleOrDefault();
                return librarianRoleId;
            }
        }

        public static bool isUserBannedById(string userEmail)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Where(u => u.Email == userEmail).SingleOrDefault();

                if (user != null)
                {
                    return user.IsBanned;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void removeOldUserOrders(string userId)
        {
            DateTime Today = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<BooksRenting> oldUserOrders = db.BooksRenting.Where(br => br.ApplicationUser.Id == userId
                    && br.TakingDate == null).ToList();
                if (oldUserOrders.Count > 0 && oldUserOrders != null)
                {
                    foreach(var order in oldUserOrders)
                    {
                        if ((Today - order.OrderDate).Days > 1)
                        {
                            db.BooksRenting.Remove(order);
                        }
                    }

                    db.SaveChanges();
                }
            }
        }

        public static void EditAuthor(Author newAuthor)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Author author = GetAuthorById(newAuthor.Id);
                if (author != null)
                {
                    author.Name = newAuthor.Name;

                    db.Entry(author).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        public static void EditPublisher(Publisher newPublisher)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Publisher publisher = GetPublisherById(newPublisher.Id);
                if (publisher != null)
                {
                    publisher.Name = newPublisher.Name;
                    publisher.City = newPublisher.City;

                    db.Entry(publisher).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        public static void EditSection(Section newSection)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Section section = GetSectionById(newSection.Id);
                if (section != null)
                {
                    section.Name = newSection.Name;

                    db.Entry(section).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        public static int GetBookRatingByBookId(Guid bookId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.BooksRenting.Where(br => br.Book.Id == bookId 
                    && br.TakingDate != null).Count();
            }
        }
    }
}
