using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;

namespace Library.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult Error(Exception ms)
        {
            return View(ms);
        }

        public ActionResult ErrorExt(ExceptionExt ms)
        {
            return View(ms);
        }
    }
}