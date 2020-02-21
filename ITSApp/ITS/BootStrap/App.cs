using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSWebApp.Configurations;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationServices.Contracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ITS.Infrastructure.DependencyResolution;
namespace ITSWebApp.BootStrap
{
    public static class App
    {
        public static void Init()
        {
            AreaRegistration.RegisterAllAreas();
            IEnumerable<ITask> tasks = DependencyResolver.Current.GetServices<ITask>();
            foreach (ITask task in tasks)
                task.Run();
        }
    }
}