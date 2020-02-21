using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    public class AuthorisationsSharedController : BaseController
    {
        //
        // GET: /AuthorisationsShared/
        private readonly ITSService.CaseService.ICaseService _caseService;

        public AuthorisationsSharedController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;
        }
        [HttpPost]
        public ActionResult DeleteCaseTreatmentPricingByCaseTreatmentPricingID(int caseTreatmentPricingID)
        {
            _caseService.DeleteCaseTreatmentPricingByCaseTreatmentPricingID(caseTreatmentPricingID);
            return Json("Successfully Deleted");
        }
        [HttpPost]
        public ActionResult DeleteCaseBespokeServiceByCaseBespokeServiceID(int caseBespokeServiceID)
        {
            _caseService.DeleteCaseBespokeServiceByCaseBespokeServiceID(caseBespokeServiceID);
            return Json("Successfully Deleted");
        }

    }
}
