using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LibManagementController : Controller
    {
        // GET: LibManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookAdding()
        {
            return View();
        }
    }
}