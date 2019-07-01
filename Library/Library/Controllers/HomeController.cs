﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;
using Library.Models;
using DBLib;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace Library.Controllers
{
    [CheckingIfBanned]
    public class HomeController : Controller
    {
        const int pagesPerPage = 12;
        Pagination pagination = new Pagination(pagesPerPage, DBCommands.GetBooksCount);

        public ActionResult Index(int id = 1, string sortingBy = "none", string sortingOrder = "none")
        {
            pagination.setPage(id);

            //For pagination
            ViewBag.MaxPage = pagination.GetMaxPage;
            ViewBag.currentPage = id;

            //For search
            ViewBag.Titles = DBCommands.GetAllTitles();
            ViewBag.AuthorsName = DBCommands.GetAllAuthorsName();

            var books = DBCommands.GetBooksRange(pagination.StartPage, pagination.Step, sortingBy, sortingOrder);

            return View(books);
        }

        public ActionResult Search(string searchString)
        {
            ViewBag.findedBooks = DBCommands.SearchBookByTitle(searchString);
            ViewBag.findedAuthors = DBCommands.SearchAuthorByName(searchString);
            return View();
        }

        public ActionResult ShowAuthor(Guid? Id)
        {
            Author author = DBCommands.GetAuthorByIdWithBooks(Id);

            if (Id == null || author == null || author.Books.Count == 0)
            {
                ViewBag.OtherAuthors = DBCommands.GetAllAuthors();
            }

            return View(author);
        }

        public ActionResult ShowPublisher(Guid? Id)
        {
            Publisher publisher = DBCommands.GetPublisherByIdWithBooks(Id);

            if (Id == null || publisher == null || publisher.Books.Count == 0)
            {
                ViewBag.OtherPublishers = DBCommands.GetAllPublishers();
            }

            return View(publisher);
        }

        public ActionResult ShowSection(Guid? Id)
        {
            Section section = DBCommands.GetSectionByIdWithBooks(Id);

            if (Id == null || section == null || section.Books.Count == 0)
            {
                ViewBag.OtherSections = DBCommands.GetAllSections();
            }

            return View(section);
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
                    return RedirectToAction("ErrorExt", "Error",  new ExceptionExt("Такої книги не існує", "Не вказано код книги", MessageState.Error));
                }

                var book = DBLib.DBCommands.GetBookWithAdditionalInfoById((Guid)id);
                ViewBag.freeBookCount = book.FreeBooksCount();
                ViewBag.BookRating = DBCommands.GetBookRatingByBookId((Guid)id);

                var userId = User.Identity.GetUserId();

                if (userId != null)
                {
                    ViewBag.userId = userId;
                    ViewBag.userExist = true;
                    ViewBag.userHasPenalty = DBCommands.UserHasPenalty(userId);

                    //Перевірка, чи цей користувач вже замовив книгу
                    bool userOrderedTheBook = DBCommands.GetBookOrderByBookAndUserId((Guid)id, userId) != null;
                    ViewBag.userOrderedTheBook = userOrderedTheBook;

                    bool userTakeTheBook = DBCommands.IsUserTakeThisBook((Guid)id, userId);
                    ViewBag.userTakeTheBook = userTakeTheBook;
                }
                else
                {
                    ViewBag.userExist = false;
                    ViewBag.userOrderedTheBook = false;
                }

                //For _Comments
                ViewBag.Comments = DBCommands.GetCommentsByBookId((Guid)id);
                ViewBag.bookId = (Guid)id;
                ViewBag.userId = userId;

                return View(book);
            }
            catch
            {
                return RedirectToAction("ErrorExt", "Error",  new ExceptionExt("Такої книги не існує", "Не знайдено книги за цим кодом", MessageState.Error));
            }
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
                    return new JsonResult { Data = "success" };
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
                    return new JsonResult { Data = "success" };
                }
                return new JsonResult { Data = "Помилка при скасуванні замовлення книги" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddCommnet(string CommentText, Guid bookId)
        {
            if(CommentText.Length > 1)
            {
                var userId = User.Identity.GetUserId();

                if (userId != null)
                {
                    DBCommands.AddComment(userId, bookId, CommentText);
                }

                return RedirectToAction("ShowBook", new { id = bookId });
            }
            else
            {
                return RedirectToAction("ErrorExt", "Error",  new ExceptionExt("Коментар без коментаря?", "Коментар повинен містити текст", MessageState.Error));
            }
        }
        
        [HttpPost]
        public ActionResult DeleteComment(Guid commentId, Guid bookId)
        {
            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
                DBCommands.deleteComment(commentId);
            }

            return RedirectToAction("ShowBook", new { id = bookId });
        }

        public ActionResult MyBooks()
        {
            var userId = User.Identity.GetUserId();

            if(userId == null)
                return RedirectToAction("Login", "Account");


            List<BooksRenting> allBooksRentings = DBCommands.getUserOrderedBooks(userId);

            List<BooksRenting> booksOrdered = allBooksRentings.Where(br => br.TakingDate == null).ToList();

            List<BooksRenting> booksTaked = allBooksRentings.Where(br => br.ReturningDate == null
                && br.TakingDate != null).ToList();

            ViewBag.booksOrdered = booksOrdered;
            ViewBag.booksTaked = booksTaked;
            return View();
        }

        [HttpPost]
        public ActionResult CancelOrder(Guid orderId)
        {
            DBCommands.cancelBookOrderById(orderId);
            return RedirectToAction("MyBooks");
        }
    }
}