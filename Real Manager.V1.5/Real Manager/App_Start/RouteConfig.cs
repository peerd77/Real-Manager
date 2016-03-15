using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Real_Manager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "teams",
                url: "teams",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "editTeam",
                url: "editTeam",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "players",
                url: "players",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
               name: "editPlayer",
               url: "editPlayer",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
