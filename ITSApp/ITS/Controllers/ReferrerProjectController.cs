using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferrerProjectController : BaseController
    {
        //
        // GET: /ReferrerProject/
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _ireferrerStorageService;

        public ReferrerProjectController(ITSService.ReferrerService.IReferrerService referrerService, ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage)
        {
            _referrerService = referrerService;
            _ireferrerStorageService = referrerStorage;
        }
        public ActionResult Index(int? referrerProjectID)
        {
            if (!referrerProjectID.HasValue)
                return RedirectToAction(GlobalConst.Actions.ReferrerController.Index, GlobalConst.Controllers.Referrer);

            return View();
        }

        [HttpPost]
        public JsonResult AddReferrerProject(ITS.Domain.Models.ReferrerModel.Project referrerproject)
        {
            referrerproject.ReferrerProjectID = _referrerService.AddReferrerProject(Mapper.Map<ITSService.ReferrerService.ReferrerProject>(referrerproject));

            return Json(referrerproject, "text/html");
        }
        [HttpPost]
        public JsonResult UpdateReferrerProject(ITS.Domain.Models.ReferrerModel.Project referrerproject)
        {
            _referrerService.UpdateReferrerProject(Mapper.Map<ITSService.ReferrerService.ReferrerProject>(referrerproject));

            return Json(referrerproject, "text/html");
        }

    }
}
