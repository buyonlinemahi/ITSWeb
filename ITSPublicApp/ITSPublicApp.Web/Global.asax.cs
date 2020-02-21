using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ITSPublicApp.Infrastructure.DependencyResolution;
using ITSPublicApp.Infrastructure.ApplicationServices;

namespace ITSPublicApp.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        protected void Application_Start()
        {
            container = new UnityContainer().LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            BootStrap.App.Init();
        }
        protected void Application_End()
        {
            container.Dispose();
        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            ITSCommonService _itscommanService = new ITSCommonService();
            _itscommanService.CreateErrorLog(ex.Message, ex.StackTrace);
            Response.Redirect("/Home/Error");
        }        
    }
}