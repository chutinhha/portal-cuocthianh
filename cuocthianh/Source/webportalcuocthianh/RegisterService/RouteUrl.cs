﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.Mvc;
namespace RegisterService
{
    public class AdminRouteUrl
    {
    }
    public class RouteUrl
    {

        public static void Route(RouteCollection routes)
        {
       //     routes.MapRoute(
       //    "Exdetails", // Route name
       //    "profile/{username}-p{id}.html", // URL with parameters
       //    new { controller = "Examinee", action = "Detail" },
       //    new { id = UrlParameter.Optional, username = UrlParameter.Optional },
       //     new[] { "CameraShop.Controllers" }// Parameter defaults// Parameter defaults
       //);
           
            routes.MapRoute(
                    "HomePage", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );


        }
    }
}
