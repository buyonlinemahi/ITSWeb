using System.Web.Mvc;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.Base;
using System.Linq;
using ITS.Domain.Models.CaseAssessmentModel;
using System.Collections.Generic;


namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class AuthorisationsController : BaseController
    {
        //
        // GET: /Authorisation/
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.PatientService.IPatientService _patientService;

        public AuthorisationsController(ITSService.CaseService.ICaseService caseService, ITSService.AssessmentService.IAssessmentService assessmentService, ITSService.PatientService.IPatientService patientService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
        }
        public ActionResult Index()
        {
            var pagedCaseWorkflowPatientAuthorisation = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
                 (_caseService.GetAuthorisationCaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));

            pagedCaseWorkflowPatientAuthorisation.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
            {
                assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.AuthorisationController.AuthorisationQA, GlobalConst.Controllers.Authorisation, new
                {
                    id = assessmentObj.CaseID
                });

            });

            pagedCaseWorkflowPatientAuthorisation.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return View(pagedCaseWorkflowPatientAuthorisation);
        }

        #region Authorisation Post Method Section
        [HttpPost]
        public ActionResult Authorisation(int treatmentCategoryID, int skip, int take)
        {

            PagedCaseWorkflowPatientReferrerProject viewModel;

            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetAuthorisationCaseWorkflowPatientProjects(skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
                {
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.AuthorisationController.AuthorisationQA, GlobalConst.Controllers.Authorisation, new
                    {
                        id = assessmentObj.CaseID
                    });

                });
                viewModel.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            }
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetAuthorisationCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
                {
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.AuthorisationController.AuthorisationQA, GlobalConst.Controllers.Authorisation, new
                    {
                        id = assessmentObj.CaseID
                    });

                });
                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }

        public ActionResult AuthorisationQA(int id)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            casePatientTreatment.CaseID = id;
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));
            var ViewModel = new AuthorisationQAViewModel
            {
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentQA(id)),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                TreatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                ReferrerCaseAssessmentModification = Mapper.Map<IEnumerable<ReferrerCaseAssessmentModification>>(_assessmentService.GetReferrerCaseAssessmentModificationsbyCaseID(id)),
            };
            
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAuthorisationQA(AuthorisationQAViewModel viewModel)
        {
            if (viewModel.CaseTreatmentPricings != null)
            {
                _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings).Where(caseTreatmentPricingId => caseTreatmentPricingId.CaseTreatmentPricingID == 0).ToArray());
            }

            if (viewModel.CaseBespokeServicePricings != null)
            {
                _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).Where(caseBespokeServiceid => caseBespokeServiceid.CaseBespokeServiceID == 0).ToArray());
            }
            _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(viewModel.CaseAssessment.CaseID); 

            int DocumentSetupTypeID = 0;
            int AssessmentType = _caseService.GetCaseAssessmentCustomsByCaseID(viewModel.CaseAssessment.CaseID);
            // Need to set the Workflow level 107 i.e. Patient in Treatment Custom
            if (AssessmentType == 1)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(viewModel.CaseAssessment.CaseID, GlobalConst.AssessmentService.InitialAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.CaseAssessment.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.PatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            }
            else if (AssessmentType == 2)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(viewModel.CaseAssessment.CaseID, GlobalConst.AssessmentService.ReviewAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.CaseAssessment.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.PatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            }
            else if (AssessmentType == 3)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(viewModel.CaseAssessment.CaseID, GlobalConst.AssessmentService.FinalAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.CaseAssessment.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.PatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            }
            return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
        }

        #endregion


    }
}
