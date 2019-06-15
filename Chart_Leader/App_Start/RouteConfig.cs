﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chart_Leader
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          
            //enabling attribute routing
            routes.MapMvcAttributeRoutes();

            //AreaRegistration.RegisterAllAreas();
            //convention-based routing
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "Chart_Leader.Areas.Users.Controllers" }
            );





        }
    }
}
