using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;
using Library.Models;
using DBLib;
using Microsoft.AspNet.Identity;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        Pagination pagination = new Pagination(12, DBCommands.GetBooksCount);

        public ActionResult Index(int id = 1, string sortingBy = "none", string sortingOrder = "none")
        {
            pagination.setPage(id);
            ViewBag.MaxPage = pagination.GetMaxPage;
            ViewBag.currentPage = id;

            var books = DBCommands.GetBooksRange(pagination.StartPage, pagination.Step, sortingBy, sortingOrder);

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

                var book = DBLib.DBCommands.GetBookWithAdditionalInfoById((Guid)id);
                ViewBag.freeBookCount = book.FreeBooksCount();

                var userId = User.Identity.GetUserId();                if (userId != null)
                {
                    ViewBag.userId = userId;
                    ViewBag.userExist = true;

                    //Перевірка, чи цей користувач вже замовив книгу
                    bool userOrderedTheBook = DBCommands.GetBookOrderByBookAndUserId((Guid)id, userId) != null;
                    ViewBag.userOrderedTheBook = userOrderedTheBook;
                }                else
                {
                    ViewBag.userExist = false;
                    ViewBag.userOrderedTheBook = false;
                }
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

        [HttpPost]
        public JsonResult bookOrder(Guid? BookId, string UserId)
        {
            try
            {
                if (BookId == null && UserId.Length < 1)
                {
                    return new JsonResult { Data = "Помилка. Не вказано Ід книги чи користувача" };
                }

                bool state = DBCommands.saveBookOrderToDB((Guid)BookId, UserId);

                if (state)
                {
                    return new JsonResult { Data = "Успішно додано!" };
                }
                return new JsonResult { Data = "Помилка при замовленні книги" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult cancelBookOrder(Guid? BookId, string UserId)
        {
            try
            {
                if (BookId == null && UserId.Length < 1)
                {
                    return new JsonResult { Data = "Помилка. Не вказано Ід книги чи користувача" };
                }

                bool state = DBCommands.cancelBookOrderInDB((Guid)BookId, UserId);

                if (state != false)
                {
                    return new JsonResult { Data = "Успішно видалено!" };
                }
                return new JsonResult { Data = "Помилка при скасуванні замовлення книги" };
            }
            catch (Exception ex)
            {
                throw ex;
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