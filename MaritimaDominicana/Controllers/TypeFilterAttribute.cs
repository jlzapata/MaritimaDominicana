using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaritimaDominicana.Controllers
{
    public class TypeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(HttpContext.Current.Session["type"].ToString().ToLower() != "admin")
            {
                HttpContext.Current.Session["userId"] = null;
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