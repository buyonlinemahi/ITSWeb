using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
 
namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class AuthorisationController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IPatientService _patientService;
        private readonly IEncryption _encryptionService;
        ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;

        public AuthorisationController(ISupplierService supplierService, IAssessmentService assessmentService, ICaseService caseService, IPatientService patientService,
            ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage, IEncryption encryptionService)
        {
            _supplierService = supplierService;
            _assessmentService = assessmentService;
            _caseService = caseService;
            _patientService = patientService;
            _referrerStorage = referrerStorage;
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult GetAuthorisations()
        {
            int supplierID = ITSUser.SupplierID.Value;
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();
            _authorisationViewModel.supplierCasePatient = (Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierAuthorisations(supplierID)));
            _authorisationViewModel.notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(supplierID));

            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
                ITSPublicApp.Domain.Models.Case caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
                supplierCasePatientObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, supplierCasePatientObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                supplierCasePatientObj.EncryptedCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());
            }
            return Json(_authorisationViewModel);
        }
        [HttpGet]
        public ActionResult AuthorityAcceptanceScreen(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            var caseObj = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(cid));
            var ViewModel = new AuthorisationAcceptanceViewModel
            {
                CaseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentQA(cid)),
                CasePatientTreatment = casePatientTreatment,
                CaseTreatmentPricings = Mapper.Map<IEnumerable<Model.CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(cid)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<Model.CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(cid)),
                TreatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<Model.TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricing(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<Model.TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
            };
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AcceptAuthorisation(int CaseID)
        {
            int DocumentSetupTypeID = 0;
            int AssessmentType = _caseService.GetCaseAssessmentCustomsByCaseID(CaseID);
            // Need to set the Workflow level 107 i.e. Patient in Treatment Custom
            if (AssessmentType == 1)
            {
                // Need to set the Workflow level 102 i.e. Authorisation Sent to Innovate Custom
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(CaseID, GlobalConst.AssessmentService.InitialAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(CaseID, ITSUser.UserID);
            }
            else if (AssessmentType == 2)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(CaseID, GlobalConst.AssessmentService.ReviewAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(CaseID, ITSUser.UserID);
            }
            else if (AssessmentType == 3)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(CaseID, GlobalConst.AssessmentService.FinalAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
                else
                    _caseService.UpdateCaseWorkFlow(CaseID, ITSUser.UserID);
            }

            return Json("Case Authoristion Successfully Accepted", "text/html");
        }
    }
}