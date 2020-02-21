using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;

namespace ITSPublicApp.Web.BootStrap
{
    public class App
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