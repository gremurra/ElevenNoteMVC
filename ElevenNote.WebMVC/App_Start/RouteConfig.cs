using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElevenNote.WebMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                //url: what builds out the URL we see on the webpage
                //{controller}: the particular controller we are using (in this project, it's probably Note)
                //{action}: the ActionResult we are calling on (Create, Details, Edit or Delete)
                //{id}: the optional parameter that will only be used when we are working with a specific note.
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
