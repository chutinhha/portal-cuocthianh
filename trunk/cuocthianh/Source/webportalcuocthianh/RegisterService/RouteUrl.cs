using System;
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
            routes.MapRoute(
            "product_details", // Route name
            "{Link}-p{Id}.html", // URL with parameters
            new { controller = "Product", action = "Details" },
            new { id = @"\d+" }// Parameter defaults
        );
            routes.MapRoute(
            "category_details", // Route name
            "{Link}-c{Id}.html", // URL with parameters
            new { controller = "Product", action = "ProductCategory" },
            new { id = @"\d+" }// Parameter defaults
        );

            routes.MapRoute(
           "manufacture_details", // Route name
           "{Link}-m{Id}.html", // URL with parameters
           new { controller = "Product", action = "ListProduct" },
           new { id = @"\d+" }// Parameter defaults
       );
            routes.MapRoute(
                    "HomePage", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );


        }
    }
}
