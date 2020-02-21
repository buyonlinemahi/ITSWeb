using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    public class ReferrerProjectSharedController : Controller
    {

        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public ReferrerProjectSharedController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }

        [HttpPost]
        public ActionResult UpdateReferrerProjectStatus(int referrerProjectID, bool isTriage)
        {
            return Json(_referrerService.UpdateProjectStatus(referrerProjectID, isTriage));
        }

    }
}
