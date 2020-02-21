using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Infrastructure.Global;
using AutoMapper;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.ViewModels.CaseStoppedViewModel;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class CaseStopController : BaseController
    {
        //
        // GET: /CaseStop/

        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        public CaseStopController(ITSService.CaseService.ICaseService caseService, ITSService.AssessmentService.IAssessmentService assessmentService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;

        }
        public ActionResult Index()
        {
            return View(GetCaseStopCaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }

        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetCaseStopCaseWorkflowPatientProjects(int skip, int take)
        {
            var pagedCaseWorkflowPatientCaseStop = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
                (_caseService.GetCaseStoppedCaseWorkflowPatientProjects(skip, take));
            pagedCaseWorkflowPatientCaseStop.CaseWorkflowPatientReferrerProjects.ToList().ForEach(obj =>
            {
                obj.ActionUrl = Url.Action(GlobalConst.Actions.CaseStopController.CaseStoppedScreen, GlobalConst.Controllers.CaseStop, new { id = obj.CaseID });
            });
            pagedCaseWorkflowPatientCaseStop.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return (pagedCaseWorkflowPatientCaseStop);
        }

        [HttpPost]
        public ActionResult GetCaseStopped(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject viewModel;
            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel = GetCaseStopCaseWorkflowPatientProjects(skip, take);
            }
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(obj =>
                {
                    obj.ActionUrl = Url.Action(GlobalConst.Actions.CaseStopController.CaseStoppedScreen, GlobalConst.Controllers.CaseStop, new { id = obj.CaseID });
                });

                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }

        public ActionResult CaseStoppedScreen(int id)
        {
            var caseAssessment = _assessmentService.GetCaseAssessmentByCaseID(id);
            var viewModel = new CaseStoppedViewModel()
            {
                CaseID = id,
                CaseStopReasons = Mapper.Map<IEnumerable<ITS.Domain.Models.CaseModel.CaseStopReason>>(_caseService.GetAllCaseStopReason()),                
                AuthorisationDetail = (caseAssessment != null) ? caseAssessment.AuthorisationDetail : null
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SubmitCaseStopped(int CaseID)
        {
            _caseService.UpdateCaseWorkFlow(CaseID, ITSUser.UserID);
            return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
        }


        [HttpGet]
        public JsonResult CaseStoppedCaseScreen(int caseID)
        {
           var t = _caseService.UpdateCaseWorkFlowStoppedCase(caseID, ITSUser.UserID);
            var message = "Stopped successfuly";
            return Json(message, "text/html", JsonRequestBehavior.AllowGet);
        }


    }
}
