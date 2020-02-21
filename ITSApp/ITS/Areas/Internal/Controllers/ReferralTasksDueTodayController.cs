using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferralTasksDueTodayController : BaseController
    {
        //
        // GET: /Internal/ReferralTasksDueToday/

        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_ReferralTasksDueTodayPartial.cshtml");
        }

    }
}
