using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.Global;
using ViewModel = ITS.Domain.ViewModels;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferralsReceivedController : Controller
    {
        //
        // GET: /Internal/ReferralReceived/
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly IReferrerStorage _referrerStorage;

        public ReferralsReceivedController(ITSService.CaseService.ICaseService caseService, IReferrerStorage referrerStorage)
        {
            _caseService = caseService;
            _referrerStorage = referrerStorage;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_ReferralsReceivedPartial.cshtml");
        }


        public ActionResult GetReferralCases(int skip, int take)
        {
            var pagedCaseWorkflowPatientReferrerProject = Mapper.Map<ViewModel.PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReferralCases(skip, take));

            foreach (var caseObj in pagedCaseWorkflowPatientReferrerProject.CaseWorkflowPatientReferrerProjects)
            {
                if (caseObj.IsTriage && !caseObj.IsTreatmentRequired)
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.Area.Internal.AcceptAndReferTriageAssessmentController.Index, GlobalConst.Controllers.Area.Internal.AcceptAndReferTriageAssessment, new { id = caseObj.CaseID });
                else
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.Area.Internal.AcceptAndReferController.Index, GlobalConst.Controllers.Area.Internal.AcceptAndRefer, new { id = caseObj.CaseID });
                caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
                                                          GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            }

            return Json(pagedCaseWorkflowPatientReferrerProject);
        }

        [HttpPost]
        public JsonResult GetReferralCasesByTreatmentCategoryID(int treatmentCatagoryID, int skip, int take)
        {
            ViewModel.PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForRefReceived = Mapper.Map<ViewModel.PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReferralCasesByTreatmentCategoryID(treatmentCatagoryID, skip, take));
            foreach (var caseObj in caseWorkflowPatientReferrerProjectsForRefReceived.CaseWorkflowPatientReferrerProjects)
            {
                if (caseObj.IsTriage)
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.Area.Internal.AcceptAndReferTriageAssessmentController.Index, GlobalConst.Controllers.Area.Internal.AcceptAndReferTriageAssessment, new { id = caseObj.CaseID });
                else
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.Area.Internal.AcceptAndReferController.Index, GlobalConst.Controllers.Area.Internal.AcceptAndRefer, new { id = caseObj.CaseID });
                caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
                                                          GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            }
            return Json(caseWorkflowPatientReferrerProjectsForRefReceived);
        }

    }
}
