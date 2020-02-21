using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using AutoMapper;
using ITS.Domain.Models;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class FinalAssessmentController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        public FinalAssessmentController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_FinalAssessmentPartial.cshtml");
        }

        [HttpPost]
        public JsonResult GetFinalAssessmentCaseWorkflowPatientProjects(int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjects = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetFinalAssessmentCaseWorkflowPatientProjects(skip, take));
            return Json(caseWorkflowPatientReferrerProjects);

        }
        [HttpPost]
        public JsonResult GetFinalAssessmentCaseWorkflowPatientProjectsByTreatmentCategoryID(int treatmentID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjects = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetFinalAssessmentCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentID, skip, take));
            return Json(caseWorkflowPatientReferrerProjects);

        }

    }
}
