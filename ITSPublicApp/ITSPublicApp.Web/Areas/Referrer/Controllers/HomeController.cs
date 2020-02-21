using AutoMapper;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Referrer.Controllers
{
    [AuthorizedUserCheck("Referrer")]
    public class HomeController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.PractitionerService.IPractitionerService _practitionerService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly ITSPublicApp.Domain.Models.SupplierDocument _supplierDocument;
        private readonly ISupplierService _supplierService;
        private readonly IEncryption _encryptionService;
        public HomeController(ITSService.CaseService.ICaseService caseService, IAssessmentService assessmentService, IPatientService patientService, ITSService.LookUpService.ILookUpService lookUpService, ITSService.PractitionerService.IPractitionerService practitionerService,
            ISupplierStorage supplierStorageService, ITSPublicApp.Domain.Models.SupplierDocument supplierDocument, ISupplierService supplierService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _lookUpService = lookUpService;
            _practitionerService = practitionerService;
            _supplierStorageService = supplierStorageService;
            _supplierDocument = supplierDocument;
            _supplierService = supplierService;
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int referrerID = ITSUser.ReferrerID.Value;
            int userID = ITSUser.UserID;
            var referrerAuthorizations = Mapper.Map<PagedReferrerAuthorisations>(_caseService.GetReferrerAuthorisationsByReferrerID(referrerID, userID, 0, 5));
            foreach (var referrerAuthorizationsObj in (IEnumerable<ReferrerAuthorisations>)referrerAuthorizations.ReferrerAuthorisations)
            {
                referrerAuthorizationsObj.EncryptedCaseID = EncryptString(referrerAuthorizationsObj.CaseID.ToString());
                string path = GetFinalUploadByCaseID(referrerAuthorizationsObj.CaseID, referrerAuthorizationsObj.AssessmentServiceID);
                if (path != "")
                {
                    referrerAuthorizationsObj.UrlPath = path;
                    referrerAuthorizationsObj.IsFinalVersionUpload = true;
                }
            }
            return View(referrerAuthorizations);
        }

        public FileResult Download(string _caseID, int ASID)
        {

            int caseID=int.Parse(DecryptString(_caseID).ToString());

            string FileName = ""; 
            string DownloadPath = "#";
            int SupplierrDocumentTypeID = 0;
            var _resCase = _caseService.GetCaseByCaseID(caseID);

            if (ASID == 1)
                SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.InitialAssessmentFinalCustom;
            else
                SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.ReviewAssessmentFinalCustom;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            IEnumerable<ITSPublicApp.Domain.Models.SupplierDocument> supLLierDocumentData = Mapper.Map<IEnumerable<Model.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(caseID, SupplierrDocumentTypeID));
            foreach (var daattaa in supLLierDocumentData)
            {
                DownloadPath = Server.MapPath(_supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)_resCase.SupplierID, _resCase.ReferrerProjectTreatmentID, caseID, daattaa.UploadPath, storagePath));
                FileName = daattaa.UploadPath;
            }
            return File(client.DownloadData(DownloadPath), "application/word", FileName);
        }

        public string GetFinalUploadByCaseID(int caseID, int assessmentServiceID)
        {
            int documentTypeID = 0;
            switch (assessmentServiceID)
            {
                case 1: documentTypeID = GlobalConst.CaseDocumentTypeId.InitialAssessment;
                    break;
                case 2: documentTypeID = GlobalConst.CaseDocumentTypeId.ReviewAssessment;
                    break;
                case 3: documentTypeID = GlobalConst.CaseDocumentTypeId.FinalAssessment;
                    break;
            }
            var caseDocument = _caseService.GetFinalUploadByCaseID(caseID, documentTypeID);
            if (caseDocument != null)
            {
                string path = GlobalConst.CaseDocumentDownloadPath + caseDocument.UploadPath;
                return path;
            }
            else
                return "";
        }

        [HttpPost]
        public JsonResult GetReferrerAuthorisations(int skip, int take)
        {
            int referrerID = ITSUser.ReferrerID.Value;
            int userID = ITSUser.UserID;
            var referrerAuthorizations = Mapper.Map<PagedReferrerAuthorisations>(_caseService.GetReferrerAuthorisationsByReferrerID(referrerID, userID, skip, take));

            foreach (var referrerAuthorizationsObj in (IEnumerable<ReferrerAuthorisations>)referrerAuthorizations.ReferrerAuthorisations)
            {
                 string path = GetFinalUploadByCaseID(referrerAuthorizationsObj.CaseID, referrerAuthorizationsObj.AssessmentServiceID);
                 if (path != "")
                 {
                     referrerAuthorizationsObj.UrlPath = path;
                     referrerAuthorizationsObj.IsFinalVersionUpload = true;
                 }
                 else
                 {
                     if (referrerAuthorizationsObj.IsCustom)
                     {
                         string DownloadPath = "#";
                         int SupplierrDocumentTypeID = 0;
                         if (referrerAuthorizationsObj.AssessmentServiceID == 1)
                             SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.InitialAssessmentFinalCustom;
                         else
                             SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.ReviewAssessmentFinalCustom;
                         string storagePath = ConfigurationManager.AppSettings["StoragePath"];
                         IEnumerable<ITSPublicApp.Domain.Models.SupplierDocument> supLLierDocumentData = Mapper.Map<IEnumerable<Model.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(referrerAuthorizationsObj.CaseID, SupplierrDocumentTypeID));
                         foreach (var daattaa in supLLierDocumentData)
                             DownloadPath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)referrerAuthorizationsObj.SupplierID, referrerAuthorizationsObj.ReferrerProjectTreatmentID, referrerAuthorizationsObj.CaseID, daattaa.UploadPath, storagePath);
                         referrerAuthorizationsObj.UrlPath = DownloadPath;
                     }
                 }
            }
            foreach (var objResult in (IEnumerable<ReferrerAuthorisations>)referrerAuthorizations.ReferrerAuthorisations)
                objResult.EncryptedCaseID = EncryptString(objResult.CaseID.ToString());

            return Json(referrerAuthorizations);
        }

        [HttpPost]
        public JsonResult ApproveAuthorisationtoInnovate(string  CaseID)
        {
            int cid = Convert.ToInt16(DecryptString(CaseID.ToString()));
            _assessmentService.UpdateCaseAssessmentAuthorisationToApprovedByCaseID(cid);
            
            int DocumentSetupTypeID = 0;
            int AssessmentType = _caseService.GetCaseAssessmentCustomsByCaseID(cid);
            // Need to set the Workflow level 107 i.e. Patient in Treatment Custom
            if (AssessmentType == 1)
            {
                // Need to set the Workflow level 102 i.e. Authorisation Sent to Innovate Custom
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(cid, GlobalConst.AssessmentService.InitialAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(cid, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoInnovateCustom);
                else
                    _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            }
            else if (AssessmentType == 2)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(cid, GlobalConst.AssessmentService.ReviewAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(cid, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoInnovateCustom);
                else
                    _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            }
            else if (AssessmentType == 3)
            {
                DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(cid, GlobalConst.AssessmentService.FinalAssessment);
                if (DocumentSetupTypeID == 2)
                    _caseService.UpdateCaseWorkflowCustomByCaseID(cid, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoInnovateCustom);
                else
                    _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            }
            _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(cid);
            return Json("Updated Sucessfully");
        }


        public ActionResult ViewAssessment(string id, string data)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            var caseObj = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(cid));

            if (data == "Initial Assessment Report Submitted")
                ViewBag.pageTitle = "Initial";
            else
                ViewBag.pageTitle = "Review";

            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(cid));
            var caseAppointmentDate = Mapper.Map<Model.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(cid));
            var viewModel = new PrintViewAsssessmentViewModel()
            {
                Patient = new Model.Patient
                {
                    FirstName = casePatientTreatment.FirstName,
                    LastName = casePatientTreatment.LastName,
                    BirthDate = casePatientTreatment.BirthDate,
                    InjuryDate = casePatientTreatment.InjuryDate,
                    PostCode = casePatientTreatment.PostCode,
                },
                Case = new Model.Case
                {
                    CaseSubmittedDate = casePatientTreatment.CaseSubmittedDate,
                    CaseReferrerReferenceNumber = casePatientTreatment.CaseReferrerReferenceNumber,
                    CaseID = cid,
                },
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>( _assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>( _assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                CaseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentQA(cid)),
                PatientRole = Mapper.Map<IEnumerable<Model.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Durations = Mapper.Map<IEnumerable<Model.Duration>>(_lookUpService.GetAllDuration()),
            };
            viewModel.CaseAssessment.CaseAssessmentDetail.AssessmentDate = caseAppointmentDate.CaseBookIADate;
            return View(viewModel);

        }

    }
}
