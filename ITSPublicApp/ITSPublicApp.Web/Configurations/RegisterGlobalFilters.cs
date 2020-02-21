using System.Web.Mvc;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;

namespace ITSPublicApp.Web.Configurations
{
    public class RegisterGlobalFilters : ITask
    {
        private readonly GlobalFilterCollection filters;
        
        public RegisterGlobalFilters()
        {
            filters = GlobalFilters.Filters;
        }
        
        public void Run()
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}