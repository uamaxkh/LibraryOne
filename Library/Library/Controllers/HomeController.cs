using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;

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
                    return RedirectToAction("Error", new MessageBag { MessageText = "Такої книги не існує", AdditionalInfo = "Не вказано код книги" });
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
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/TitlePic/"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new MessageBag { MessageText = "Помилка додавання файлу", AdditionalInfo = ex.Message });
                }
            else
            {
                return RedirectToAction("Error", new MessageBag { MessageText = "Помилка додавання файлу", AdditionalInfo = "НЕМАЄ ФАЙЛУ" });
            }
            return View();
        }
    }
}