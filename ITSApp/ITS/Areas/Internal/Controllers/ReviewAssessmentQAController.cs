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
    public class ReviewAssessmentQAController : Controller
    {
        //
        // GET: /Internal/ReviewAssessmentQA/

        private readonly ITSService.CaseService.ICaseService _caseService;

        public ReviewAssessmentQAController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;
        }
        public ActionResult Index()
        {
            //IEnumerable<CaseWorkflowPatientReferrerProject> caseWorkflowPatientReferrerProjectsForReviewAssessment =
            //    Mapper.Map<IEnumerable<CaseWorkflowPatientReferrerProject>>(_caseService.GetReviewAssessmentQACaseWorkflowPatientProjects());
            //foreach (var caseObj in caseWorkflowPatientReferrerProjectsForReviewAssessment)
            //{
            //    caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
            //                                              GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            //}
            //return View("~/Areas/Internal/Views/Shared/MainScreen/_ReviewAssessmentPartial.cshtml", caseWorkflowPatientReferrerProjectsForReviewAssessment);
            return View("~/Areas/Internal/Views/Shared/MainScreen/_ReviewAssessmentPartial.cshtml");
        }

        [HttpPost]
        public JsonResult GetReviewAssessmentQACaseWorkflowPatientProjects(int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForReviewAssessment =
                Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReviewAssessmentQACaseWorkflowPatientProjects(skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForReviewAssessment);
        }

        [HttpPost]
        public JsonResult GetReviewAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForReviewAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReviewAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForReviewAssessment);
        }

    }
}
