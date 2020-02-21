using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ITS.Infrastructure.DependencyResolution;
using System.Web;
using System;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Text;
using ITS.Infrastructure.ApplicationServices;


namespace ITSWebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        


        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            container = new UnityContainer().LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            BootStrap.App.Init();
            // RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
        protected void Session_End(object sender, EventArgs e)
        {
            string sessionId = this.Session.SessionID;
            //your code...
            ITSService.UserService.UserServiceClient _userServiceClient = new ITSService.UserService.UserServiceClient();
            _userServiceClient.ResetUserSessionID(((ITSUser)(sender)).UserID);
        }

        //protected void Application_Error()
        //{
        //    var ex = Server.GetLastError();

        //    EmailService _emailService = new EmailService();
        //    _emailService.CreateErrorLog(ex.Message, ex.StackTrace);
        //    Response.Redirect("/Home/Error");
        //}
    }
}