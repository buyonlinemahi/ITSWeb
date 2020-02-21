using System.Web.Mvc;
using ITS.Infrastructure.Base;
using System.Collections;
using ITS.Domain.Models;
using AutoMapper;
using System.Collections.Generic;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class InternalTaskController : BaseController
    {
        //
        // GET: /Internal/InternalTask/

        private readonly ITSService.CaseService.ICaseService _caseService;

        public InternalTaskController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCaseCounts()
        {
            IEnumerable<CaseWorkflowCount> cases = Mapper.Map<IEnumerable<CaseWorkflowCount>>(_caseService.GetCaseCounts());
            return Json(cases);
        }

        [HttpPost]
        public JsonResult GetCaseCountByTreatmentCategoryID(int treatmentCategoryID)
        {

            IEnumerable<CaseWorkflowCount> cases = Mapper.Map<IEnumerable<CaseWorkflowCount>>(_caseService.GetCaseCountByTreatmentCategoryID(treatmentCategoryID));
            return Json(cases);
        }


    }
}
