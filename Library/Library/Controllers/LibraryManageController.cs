using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LibraryManageController : Controller
    {
        // GET: LibraryManage
        public ActionResult Index()
        {
            return View();
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