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
                            throw new Exception("Файл обкладинки не вдалося додати");
                        }
                    }

                    DBLib.DBCommands.AddBook(book, bookViewModel.AuthorsId, bookViewModel.PublisherName, bookViewModel.SectionName);
                }
                catch (Exception ex)
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

        public ActionResult AddPublisher()
        {
            return View();
        }

        public ActionResult AddSection()
        {
            return View();
        }
    }
}