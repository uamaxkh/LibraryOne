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
                    return RedirectToAction("Error", new MessageBag { MessageText = "Такої книги не існує", AdditionalInfo = "Не вказано код книги", State = MessageState.Error });
                }

                var book = DBLib.DBCommands.GetBookById((Guid)id);
                return View(book);
            }
            catch
            {
                return RedirectToAction("Error", new MessageBag { MessageText = "Такої книги не існує", AdditionalInfo = "Не знайдено книги за цим кодом" });
            }
        }

        public ActionResult Error(MessageBag ms)
        {
            return View(ms);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            var authorsList = DBLib.DBCommands.GetAllAuthors();
            SelectList authorsSelectList = new SelectList(authorsList, "Id", "Name");
            ViewBag.authorsSelectList = authorsSelectList;

            var sectionsList = DBLib.DBCommands.GetAllSections();
            SelectList sectionsSelectList = new SelectList(sectionsList, "Id", "Name");
            ViewBag.sectionsSelectList = sectionsSelectList;

            var publishersList = DBLib.DBCommands.GetAllPublishers();
            SelectList publishersSelectList = new SelectList(publishersList, "Id", "Name");
            ViewBag.publishersSelectList = publishersSelectList;
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

                    DBLib.DBCommands.AddBook(book, bookViewModel.AuthorsId, bookViewModel.PublisherId, bookViewModel.SectionId);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new MessageBag { MessageText = "Помилка додавання книги", AdditionalInfo = ex.Message });
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Error", new MessageBag { MessageText = "Книгу додано", State = MessageState.Succes });
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