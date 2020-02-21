using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using InternalTasksViewModels = ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Domain.Models.InternalTaskModel;
using ITSWebApp.ITSService.AssessmentService;
using ITSWebApp.ITSService.ReferrerService;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Domain.ViewModels.InternalTasksViewModel;

using TreatmentCategoriesBespokeService = ITS.Domain.Models.TreatmentCategoriesBespokeService;

using System;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class InternalTasksController : BaseController
    {
        //
        // GET: /InternalTask/

        private readonly ITSService.CaseService.ICaseService _caseService;


        public InternalTasksController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;

        }


        public ActionResult Index()
        {
            var caseCounts = new InternalTaskIndexViewModel()
            {
                caseWorkflowCount = Mapper.Map<IEnumerable<CaseWorkflowCount>>(_caseService.GetCaseCounts()).OrderBy(order => order.Ordinal),
                TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All
            };
            return View(caseCounts);
        }
        [HttpPost]
        public ActionResult GetCaseCountByTreatmentCategoryID(int TreatmentCategoryID)
        {
            var viewModel = new InternalTaskIndexViewModel();

            if (TreatmentCategoryID != (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel.caseWorkflowCount = Mapper.Map<IEnumerable<CaseWorkflowCount>>(_caseService.GetCaseCountByTreatmentCategoryID(TreatmentCategoryID)).OrderBy(o => o.Ordinal);
                viewModel.TreatmentCategoryID = TreatmentCategoryID;
            }
            else
            {
                viewModel.caseWorkflowCount = Mapper.Map<IEnumerable<CaseWorkflowCount>>(_caseService.GetCaseCounts()).OrderBy(order => order.Ordinal);
                viewModel.TreatmentCategoryID = TreatmentCategoryID;
            }

            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }

        public ActionResult SelectedInternalTask(string selectedType)
        {
            switch (selectedType)
            {
                case GlobalConst.ReferralReceivedTypes.Referrals:
                    return RedirectToAction(GlobalConst.Actions.ReferralController.Index, GlobalConst.Controllers.Referral);
                case GlobalConst.ReferralReceivedTypes.TriageAssessmentQA:
                    return RedirectToAction(GlobalConst.Actions.TriageAssessmentController.Index, GlobalConst.Controllers.TriageAssessment);
                case GlobalConst.ReferralReceivedTypes.InitialAssessmentQA:
                    return RedirectToAction(GlobalConst.Actions.InitialAssessmentController.Index, GlobalConst.Controllers.InitialAssessment);
                case GlobalConst.ReferralReceivedTypes.Authorisation:
                    return RedirectToAction(GlobalConst.Actions.AuthorisationController.Index, GlobalConst.Controllers.Authorisation);
                case GlobalConst.ReferralReceivedTypes.ReviewAssessmentQA:
                    return RedirectToAction(GlobalConst.Actions.ReviewAssessmentController.Index, GlobalConst.Controllers.ReviewAssessment);
                case GlobalConst.ReferralReceivedTypes.FinalAssessmentQA:
                    return RedirectToAction(GlobalConst.Actions.FinalAssessmentController.Index, GlobalConst.Controllers.FinalAssessment);
                case GlobalConst.ReferralReceivedTypes.CaseStopped:
                    return RedirectToAction(GlobalConst.Actions.CaseStopController.Index, GlobalConst.Controllers.CaseStop);
                case GlobalConst.ReferralReceivedTypes.CaseCompleted:
                    return RedirectToAction(GlobalConst.Actions.CaseCompleteController.Index, GlobalConst.Controllers.CaseComplete);
                case GlobalConst.ReferralReceivedTypes.ReferralTasksDueToday:
                    return RedirectToAction("", "");
                default:
                    return RedirectToAction(GlobalConst.Actions.InternalTasksController.Index, GlobalConst.Controllers.InternalTasks);
            }
        }



    }
}
