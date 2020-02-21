using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class CaseStoppedController : Controller
    {
        //
        // GET: /Internal/CaseStopped/
        private readonly ITSService.CaseService.ICaseService _caseService;
        public CaseStoppedController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;
           
        }
        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_CaseStoppedPartial.cshtml");
        }

        [HttpPost]
        public JsonResult GetCaseStoppedCaseWorkflowPatientProjects(int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForCaseStopped = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetCaseStoppedCaseWorkflowPatientProjects(skip, take));


            return Json(caseWorkflowPatientReferrerProjectsForCaseStopped);
        }

        [HttpPost]
        public JsonResult GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID(int treatmentCatagoryID, int skip, int take)
        {
          PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForCaseStopped = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCatagoryID, skip, take));

            return Json(caseWorkflowPatientReferrerProjectsForCaseStopped);
        }

    }
}
