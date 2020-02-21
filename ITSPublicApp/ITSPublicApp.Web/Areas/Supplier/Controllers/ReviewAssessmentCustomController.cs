using AutoMapper;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class ReviewAssessmentCustomController : BaseController
    {
        //
        // GET: /Supplier/ReviewAssessmentCustom/

        private readonly IPatientService _patientService;
        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IReferrerService _referrerService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IReferrerStorage _referrerReferrerStorage;
        private readonly SupplierDocument _supplierDocument;
        private readonly ISupplierService _supplierService;
        private readonly EmailService _emailService;
        private readonly IEncryption _encryptionService; 

        public ReviewAssessmentCustomController(
            ICaseService caseService,
            IPatientService patientService,
            ISupplierService supplierService,
            IAssessmentService assessmentService,
            IReferrerService referrerService,
            ISupplierStorage supplierStorageService,
            IReferrerStorage referrerReferrerStorage,
            SupplierDocument supplierDocument,
            EmailService emailService,
            IEncryption encryptionService
            )
        {
            _caseService = caseService;
            _patientService = patientService;
            _assessmentService = assessmentService;
            _referrerService = referrerService;
            _referrerReferrerStorage = referrerReferrerStorage;
            _supplierStorageService = supplierStorageService;
            _supplierDocument = supplierDocument;
            _supplierService = supplierService;
            _emailService = emailService;
            _encryptionService = encryptionService;
        }
         
        [HttpGet]
        public ActionResult Index(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            ReviewAssessmentCustomViewModel reviewAssessmentCustomViewModel = new ReviewAssessmentCustomViewModel();
            reviewAssessmentCustomViewModel.casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            reviewAssessmentCustomViewModel.casePatientTreatment.CaseID = cid;
            reviewAssessmentCustomViewModel.CaseService = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(cid));
            reviewAssessmentCustomViewModel.CaseService.EncryptedCaseID = id;
            reviewAssessmentCustomViewModel.CaseBespokeServicePricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingByCaseIDAndInComplete(cid));
            reviewAssessmentCustomViewModel.CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(cid));
            return View(reviewAssessmentCustomViewModel);
        }

        public FileResult Download(string _caseID)
        {

            int cid = Convert.ToInt32(DecryptString(_caseID));
            ReviewAssessmentCustomViewModel reviewAssessmentCustomViewModel = new ReviewAssessmentCustomViewModel();
            reviewAssessmentCustomViewModel.caseAssessmentCustom = Mapper.Map<Model.CaseAssessmentCustom>(_assessmentService.GetCaseAssessmentCustomByCaseID(cid));
            if (reviewAssessmentCustomViewModel.caseAssessmentCustom.IsFurtherTreatment == true)
                reviewAssessmentCustomViewModel.ReferrerDocument = Mapper.Map<List<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(cid, (int)GlobalConst.ReferrerDocumentTypeID.ReturnAssessmentCustom));
            else
                reviewAssessmentCustomViewModel.ReferrerDocument = Mapper.Map<List<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(cid, (int)GlobalConst.ReferrerDocumentTypeID.FinalAssessmentCustom));

            reviewAssessmentCustomViewModel.AssessmentFileCustomURLPath = Server.MapPath(_referrerReferrerStorage.GetReferralUploadFolderStoragePath(reviewAssessmentCustomViewModel.ReferrerDocument[0].ReferrerID, (int)reviewAssessmentCustomViewModel.ReferrerDocument[0].ReferrerProjectTreatmentID, reviewAssessmentCustomViewModel.ReferrerDocument[0].UploadPath,
                ConfigurationManager.AppSettings["StoragePath"]));

            return File(reviewAssessmentCustomViewModel.AssessmentFileCustomURLPath, "application/word", reviewAssessmentCustomViewModel.ReferrerDocument[0].UploadPath);
        }


        [HttpPost]
        public ActionResult saveCaseTreatmentPricing(ReviewAssessmentCustomViewModel viewModel)
        {
            //int CaseID = 0;
            if (viewModel.CaseTreatmentPricings != null)
            {
                foreach (var caseTreatmentPricing in viewModel.CaseTreatmentPricings)
                {
                    _caseService.UpdateCaseTreatmentPricingByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(caseTreatmentPricing));
                    //CaseID = caseTreatmentPricing.CaseID;
                }
            }
            return Json("Saved Sucessfully", "text/html");
        }

        [HttpPost]
        public ActionResult AddReviewAssessmentCustom(ReviewAssessmentCustomViewModel viewModel)
        {
            int userID = ITSUser.UserID;
            int CaseID = viewModel.CaseService.CaseID;
            int ReferrerProjectTreatmentID = viewModel.CaseService.ReferrerProjectTreatmentID;

            if (viewModel.CaseBespokeServicePricings != null)
            {
                foreach (var CaseBespokeServicePricing in viewModel.CaseBespokeServicePricings)
                {
                    _caseService.UpdateCaseBespokeServicePricingByCaseBespokeServiceID(Mapper.Map<ITSService.CaseService.CaseBespokeServicePricing>(CaseBespokeServicePricing));
                }
            }
            int DidNotShowCnt = 0;
            if (viewModel.CaseTreatmentPricings != null)
            {
                foreach (var _caseTreatmentPricing in viewModel.CaseTreatmentPricings)
                {
                    if (_caseTreatmentPricing.PatientDidNotAttend != null)
                        if (_caseTreatmentPricing.PatientDidNotAttend.Value)
                            DidNotShowCnt++;

                    _caseService.UpdateCaseTreatmentPricingByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(_caseTreatmentPricing));
                }
            }
            // After click Save in Review Assessment / Final Assecssment  if only one "Did Not Show" checkbox is checked, send email to: a. support@innovatehmg.co.uk

            if (DidNotShowCnt == 1)
            {
                var _resCase = _caseService.GetCaseByCaseID(CaseID);
                var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);
                
                IntialAssessmentReportDetail _IntialReport = new IntialAssessmentReportDetail();
                _IntialReport = Mapper.Map<IntialAssessmentReportDetail>(_caseService.GetIntialAssessmentReportDetailsByCaseID(CaseID));
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/TreatmentMatrixDidNotAttend.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                bool result = _emailService.SendGeneralEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "First Did Not Attend - " + _IntialReport.PatientFullName, MailText);
            }

            string filestoragePath = "";
            string UploaedDocmentName = "";
            int WorkFlowID = 0;
            if (viewModel.AssessmentCustomUploadFile != null)
            {
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                _supplierDocument.SupplierID = (int)viewModel.CaseService.SupplierID.Value; ;
                _supplierDocument.ReferrerProjectTreatmentID = (int?)ReferrerProjectTreatmentID;
                _supplierDocument.UserID = userID;
                _supplierDocument.UploadDate = DateTime.Now;
                _supplierDocument.CaseId = CaseID;
                if (!viewModel.RequiredTreatment)
                {
                    UploaedDocmentName = (string)GlobalConst.SupplierDocumentType.ReviewAssessmentSupplierCustom.ToString() + "_" + DateTime.Now.ToString("MMddyyhhmmss") + Path.GetExtension(viewModel.AssessmentCustomUploadFile.FileName).ToString();
                    _supplierDocument.DocumentName = (string)GlobalConst.SupplierDocumentType.ReviewAssessmentSupplierCustom;
                    _supplierDocument.DocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.ReviewAssessmentSupplierCustom;
                    WorkFlowID = (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoInnovateCustom;
                }
                else
                {
                    UploaedDocmentName = (string)GlobalConst.SupplierDocumentType.FinalAssessmentSupplierCustom.ToString() + "_" + DateTime.Now.ToString("MMddyyhhmmss") + Path.GetExtension(viewModel.AssessmentCustomUploadFile.FileName).ToString();
                    _supplierDocument.DocumentName = (string)GlobalConst.SupplierDocumentType.FinalAssessmentSupplierCustom;
                    _supplierDocument.DocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.FinalAssessmentSupplierCustom;
                    WorkFlowID = (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoInnovateCustom;
                }
                _supplierDocument.UploadPath = UploaedDocmentName.Trim();
                filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewModel.CaseService.SupplierID.Value, ReferrerProjectTreatmentID, CaseID, UploaedDocmentName.Trim(), storagePath);
                viewModel.AssessmentCustomUploadFile.SaveAs(filestoragePath);
                int inerted = _supplierService.AddSupplierDocumentCustom(_supplierDocument);
                _caseService.UpdateCaseWorkflowCustomByCaseID(CaseID, ITSUser.UserID, WorkFlowID);
                viewModel.AssessmentCustomUploadFile = null;
            }
            return Json("Sucessfully Submitted", "text/html");
        }

    }
}
