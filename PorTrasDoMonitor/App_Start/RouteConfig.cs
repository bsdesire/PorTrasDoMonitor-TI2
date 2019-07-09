using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Define a rota default categoria optional
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{categoria}",
                defaults: new { controller = "Noticias", action = "Index", categoria = UrlParameter.Optional }
            );
        }
    }
}