using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;
using Library.Models;
using DBLib;

namespace Library.Controllers
{
    public class LibraryManageController : Controller
    {
        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(AddBooksViewModels bookViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var book = new Book(bookViewModel.ISBN, bookViewModel.Title, bookViewModel.LibraryReading, bookViewModel.Pages,
                           bookViewModel.Quantity, bookViewModel.Year);

                    //Adding book picture
                    if (bookViewModel.TitlePic != null)
                    {
                        bool picSaved = SaveTitlePic(bookViewModel.TitlePic, book.Id);

                        if (picSaved == false)
                        {
                            throw new ExceptionExt("Файл обкладинки не вдалося додати");
                        }
                    }

                    DBLib.DBCommands.AddBook(book, bookViewModel.AuthorsId, bookViewModel.PublisherName, bookViewModel.SectionName);
                }
                catch (ExceptionExt ex)
                {
                    return RedirectToAction("Error", "Home", new ExceptionExt("Помилка додавання книги", ex.ToString(), MessageState.Error));
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Error", "Home", new ExceptionExt("Книгу додано", null, MessageState.Succes));
        }

        public bool SaveTitlePic(HttpPostedFileBase titlePic, Guid Id)
        {
            if (titlePic != null && titlePic.ContentLength > 0)
            {
                try
                {
                    if (titlePic.ContentType != "image/jpeg")
                    {
                        return false;
                    }
                    string path = Path.Combine(Server.MapPath("~/TitlePic/"), Id.ToString() + ".jpg");

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    titlePic.SaveAs(path);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public void RemoveTitlePic(Guid Id)
        {
            string path = Path.Combine(Server.MapPath("~/TitlePic/"), Id.ToString() + ".jpg");

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public JsonResult GetAuthorsJson()
        {
            var authors = DBCommands.GetAllAuthors();
            var authorsNameAndId = authors.Select(a => new { Id = a.Id, Name = a.Name });
            return new JsonResult { Data = authorsNameAndId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult AddAuthorToDB(string authorName)
        {
            if (authorName.Length > 3)
            {
                string result = DBLib.DBCommands.AddAuthor(authorName);

                return new JsonResult() { Data = result };
            }
            else
            {
                return new JsonResult() { Data = "Закоротке ім'я" };
            }
        }

        public JsonResult GetPublishersJson()
        {
            var publishers = DBCommands.GetAllPublishers();
            var publishersNameAndId = publishers.Select(a => a.Name);
            return new JsonResult { Data = publishersNameAndId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSectionsJson()
        {
            var sections = DBCommands.GetAllSections();
            var sectionsNameAndId = sections.Select(a => a.Name);
            return new JsonResult { Data = sectionsNameAndId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPublisher(Publisher publisher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBCommands.addPublisher(publisher);
                }
                else
                {
                    return View();
                }
            }
            catch (ExceptionExt ex)
            {
                return RedirectToAction("Error", "Home", ex);
            }

            return RedirectToAction("Error", "Home", new ExceptionExt("Видання успішно додано", null, MessageState.Succes));
        }

        [HttpGet]
        public ActionResult AddSection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSection(Section section)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBCommands.addSection(section);
                }
                else
                {
                    return View();
                }
            }
            catch (ExceptionExt ex)
            {
                return RedirectToAction("Error", "Home", ex);
            }

            return RedirectToAction("Error", "Home", new ExceptionExt("Розділ успішно додано", null, MessageState.Succes));
        }

        public ActionResult RegisteredUsers()
        {
            var registeredUsers = DBCommands.getOnlyRegisteredUsers();

            return View(registeredUsers);
        }

        public ActionResult ShowUserInfo(string id)
        {
            var usersInfo = DBCommands.GetUserById(id);
            if(usersInfo == null)
            {
                return RedirectToAction("Error", "Home", new ExceptionExt("Користувач не знайдений", null, MessageState.Error));
            }

            ViewBag.usersInfo = usersInfo;

            List<BooksRenting> allBooksRentings = DBCommands.getUserOrderedBooks(id);

            List<BooksRenting> booksOrdered = allBooksRentings.Where(br => br.TakingDate == null).ToList();

            List<BooksRenting> booksTaked = allBooksRentings.Where(br => br.ReturningDate == null
                && br.TakingDate != null).ToList();

            ViewBag.booksOrdered = booksOrdered;
            ViewBag.booksTaked = booksTaked;

            return View(allBooksRentings);
        }

        [HttpPost]
        public ActionResult UserTakingBook(string orderForTakingId, string userId)
        {
            DBCommands.TakeBook(orderForTakingId);

            return RedirectToAction("ShowUserInfo", new { id = userId });
        }

        [HttpPost]
        public ActionResult UserReturningBook(string orderForReturningId, string userId)
        {
            DBCommands.ReturnBook(orderForReturningId);

            return RedirectToAction("ShowUserInfo", new { id = userId });
        }

        [HttpGet]
        public ActionResult EditBook(Guid? id)
        {
            if(id == null)
            {
                return RedirectToAction("Error", "Home", new ExceptionExt("Id книги не вказано", null, MessageState.Error));
            }

            var book = DBCommands.GetBookWithAdditionalInfoById((Guid)id);
            ViewBag.Publishers = DBCommands.GetAllPublishers();
            ViewBag.Sections = DBCommands.GetAllSections();
            ViewBag.Authors = DBCommands.GetAllAuthors();

            if (book == null)
            {
                return RedirectToAction("Error", "Home", new ExceptionExt("Книгу не знайдено", null, MessageState.Error));
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book book, Guid Section, Guid Publisher, Guid[] Authors, HttpPostedFileBase TitlePic)
        {
            try
            {
                DBCommands.EditBook(book, Section, Publisher, Authors);

                if (TitlePic != null)
                {
                    bool picSaved = SaveTitlePic(TitlePic, book.Id);

                    if (picSaved == false)
                    {
                        throw new ExceptionExt("Файл обкладинки не вдалося додати");
                    }
                }

                return RedirectToAction("Error", "Home", new ExceptionExt("Успішо відредаговано", null, MessageState.Succes));
            }
            catch (ExceptionExt ex)
            {
                return RedirectToAction("Error", "Home", ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteBook(Guid bookId)
        {
            DBCommands.DeleteBook(bookId);
            return RedirectToAction("Error", "Home", new ExceptionExt("Успішо видалено", null, MessageState.Succes));
        }

        [HttpPost]
        public ActionResult ReturnBook(Guid bookId)
        {
            DBCommands.ReturnBook(bookId);
            return RedirectToAction("Error", "Home", new ExceptionExt("Успішо повернуто", null, MessageState.Succes));
        }
    }
}