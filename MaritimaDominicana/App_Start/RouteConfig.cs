using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaritimaDominicana
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ProblemDetails", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Follow",
                url: "Follow/{userId}/{followerId}",
                defaults: new {controller = "Users", action = "Follow", userId = "", followerId = ""}
            );
        }
    }
}
