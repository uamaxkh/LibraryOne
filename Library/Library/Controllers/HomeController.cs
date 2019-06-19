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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var books = DBLib.DBCommands.GetAllBooks();

            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowBook(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Error", new ExceptionExt("Такої книги не існує", "Не вказано код книги", MessageState.Error));
                }

                var book = DBLib.DBCommands.GetBookById((Guid)id);
                return View(book);
            }
            catch
            {
                return RedirectToAction("Error", new ExceptionExt("Такої книги не існує", "Не знайдено книги за цим кодом", MessageState.Error));
            }
        }

        public ActionResult Error(ExceptionExt ms)
        {
            return View(ms);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(AddBooksViewModels bookViewModel)
        {
            if (ModelState.IsValid/* && bookViewModel.AuthorsId.Count > 0*/)
            {
                try
                {
                    var book = new Book(bookViewModel.ISBN, bookViewModel.LibraryReading, bookViewModel.Pages,
                        bookViewModel.Quantity, bookViewModel.Title, bookViewModel.Year);

                    //Adding book picture
                    if (bookViewModel.TitlePic != null)
                    {
                        string titlePicFileName = SaveTitlePic(bookViewModel.TitlePic);
                        book.TitlePic = titlePicFileName;

                        if (titlePicFileName == string.Empty)
                        {
                            throw new Exception("Файл обкладинки не вдалося додати");
                        }
                    }

                    DBLib.DBCommands.AddBook(book, bookViewModel.AuthorsId, bookViewModel.PublisherName, bookViewModel.SectionName);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new ExceptionExt("Помилка додавання книги", ex.ToString(), MessageState.Error));
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Error", new ExceptionExt("Книгу додано", null, MessageState.Succes));
        }

        public string SaveTitlePic(HttpPostedFileBase titlePic)
        {
            string titlePicFileName = Path.GetFileName(titlePic.FileName);
            if (titlePic != null && titlePic.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/TitlePic/"), titlePicFileName);
                    titlePic.SaveAs(path);
                }
                catch
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
            return titlePicFileName;
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

        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------
        //test section----------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(string authorsNames)
        {
            ViewBag.message = authorsNames;
            return View();
        }
    }
}