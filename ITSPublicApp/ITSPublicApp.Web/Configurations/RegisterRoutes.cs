using System.Web.Routing;
using System.Web.Mvc;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;

namespace ITSPublicApp.Web.Configurations
{
    public class RegisterRoutes : ITask
    {
        private readonly RouteCollection routes;

        public RegisterRoutes()
        {
            routes = RouteTable.Routes;
        }

        public void Run()
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[] { "ITSPublicApp.Web.Controllers" }
            );
            routes.MapRoute(
               "User", // Route name
               "{controller}/{action}/{id}/{cnt}", // URL with parameters
               new { controller = "User", action = "ResetPassword", id = UrlParameter.Optional, cnt = UrlParameter.Optional }, // Parameter defaults
               new string[] { "ITSPublicApp.Web.Controllers" }
           );
        }
    }
}