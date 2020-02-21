using System.Web.Mvc;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.ApplicationFilters;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToRoute(new { controller = "NewPatients", action = "Index" });
        }
    }
}
