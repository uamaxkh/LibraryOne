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