using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaritimaDominicana.Models;
using System.Web.Routing;

namespace MaritimaDominicana.Controllers
{
    public class LoginFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "Users" },
                        {"action", "Login" }
                    });
            }
        }
    }
}