using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationFilters;
namespace ITSWebApp.Configurations
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