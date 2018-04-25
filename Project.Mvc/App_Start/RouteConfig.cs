using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "VehicleMake",
                url: "VehicleMakes/{action}/{id}",
                defaults: new { controller = "VehicleMakes", action = "IndexAsync", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VehicleModel",
                url: "VehicleModels/{action}/{id}",
                defaults: new { controller = "VehicleModels", action = "IndexAsync", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
