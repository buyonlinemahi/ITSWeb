using System.Web.Mvc;
using ITS.Infrastructure.Base;


namespace ITSWebApp.Areas.Internal.Controllers
{
    [HandleError]
    public class ManagementController : BaseController
    {

        private readonly ITSService.UserService.IUserService _userService;

        //TODO: Implement IoC later.
        public ManagementController(ITSService.UserService.IUserService userService)
        {
            _userService = userService;
        }

        public ManagementController()
        {
            _userService = new ITSService.UserService.UserServiceClient();
        }
        public ActionResult Index()
        {
            return View();
        }
        //contains all related partial views related to management page.

        public PartialViewResult Management()
        {
            return PartialView();
        }

        public PartialViewResult ManagementUserControl()
        {
            return PartialView();
        }

        public PartialViewResult ManagementGroupControl()
        {
            return PartialView();
        }

        public PartialViewResult ManagementReports()
        {
            return PartialView();
        }

        public PartialViewResult EmailControl()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult GerPartial(int id)
        {
            if (id == 1)
            {return PartialView("Management/_InternalSecurityPartial"); }
            else if(id==2){return PartialView("Management/_ReferrerSecurityPartial");}
            else if(id==3){return PartialView("Management/_SupplierSecurityPartial");}

            return PartialView("Management/_InternalSecurityPartial");
        }
    }
}
