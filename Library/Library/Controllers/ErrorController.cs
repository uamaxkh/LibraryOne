using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBLib.Models;
using System.IO;

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
            List<string> errMessage = new List<string>();

            errMessage.Add("---SystemException [" + DateTime.Now + "] ---");
            errMessage.Add("Message: " + ms.Message);
            errMessage.Add("Data: " + ms.Data);
            errMessage.Add("InnerException: " + ms.InnerException);
            errMessage.Add("Source: " + ms.Source);
            errMessage.Add("StackTrace: " + ms.StackTrace + "\n");

            DBLib.Logger.Log(errMessage, Server.MapPath("~/Log/"));
            return View(ms);
        }

        public ActionResult ErrorExt(ExceptionExt ms)
        {
            List<string> errMessage = new List<string>();

            errMessage.Add("---ExceptionExt [" + DateTime.Now + "] ---");
            errMessage.Add("Message: " + ms.Message);
            errMessage.Add("MessageExt: " + ms.MessageExt);
            errMessage.Add("AdditionalInfo: " + ms.AdditionalInfo + "\n");

            DBLib.Logger.Log(errMessage, Server.MapPath("~/Log/"));
            return View(ms);
        }
    }
}