using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DBLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Library
{
    public class CheckingIfBanned : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userEmail = filterContext.HttpContext.User.Identity.Name;
                bool isBanned = DBLib.DBCommands.isUserBannedById(userEmail);
                if (isBanned)
                {
                    var AuthenticationManager = filterContext.HttpContext.GetOwinContext().Authentication;
                    AuthenticationManager.SignOut();
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Banned"
                    };
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}