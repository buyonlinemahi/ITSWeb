using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Domain.Models;
using System.Configuration;
using AutoMapper;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using System.IO;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Infrastructure.ApplicationFilters;
using System.Net;
//using ITSWebApp.ITSService.CaseService;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    public class FileController : BaseController
    {
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly IReferrerStorage _referrerStorage;
        private readonly ILetterGeneration _letterGeneration;
        private readonly ISupplierStorage _supplierStorageService;
        public FileController(ITSService.PatientService.IPatientService patientService, ITSService.CaseService.ICaseService caseService, IReferrerStorage referrerStorage, ILetterGeneration letterGeneration, ISupplierStorage supplierStorageService)
        {
            _patientService = patientService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
            _letterGeneration = letterGeneration;
            _supplierStorageService = supplierStorageService;
        }
        //
        // GET: /File/

        public FileController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        //change this to post later
        [HttpGet]
        public ActionResult ViewReferral(int caseID)
        {
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(caseID));
            var fileName = caseObj.CaseNumber.ToString();
            var filePath = string.Format("{0}.{1}",
                _referrerStorage.GetReferrerReferralsStoragePath(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, caseID, fileName, ConfigurationManager.AppSettings["StoragePath"])
                , "pdf");
            return File(filePath, "application/pdf", caseObj.CaseNumber.ToString());
        }


        [HttpGet]
        public ActionResult ViewSupplierTriageAssessment(int caseID)
        {
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(caseID));
            ITSWebApp.ITSService.CaseService.CaseDocument caseDocument = Mapper.Map<ITSWebApp.ITSService.CaseService.CaseDocument>(_caseService.GetTriageCaseDocumentByCaseID(caseID));
            var fileName = caseDocument.DocumentName.ToString();
            var filePath = string.Format("{0}",
                _supplierStorageService.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), caseObj.SupplierID.Value, caseDocument.UploadPath,
                GlobalConst.SupplierDocumentType.Triage)
                );
            if (Path.GetExtension(filePath) == ".doc" || Path.GetExtension(filePath) == ".docx")
                return File(filePath, "application/msword", fileName);
            else
                return File(filePath, "application/pdf", fileName);
        }


        [HttpGet]
        public ActionResult ViewCaseUploads(string UploadPath, string DocumentName)
        {
            var filePath = "";

            if (UploadPath.Contains("|"))
            {
                string[] strArr = null;
                char[] splitchar = { '|' };
                strArr = UploadPath.Split(splitchar);

                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);

                filePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath(int.Parse(strArr[1].ToString()), int.Parse(strArr[2].ToString()), int.Parse(strArr[3].ToString()), strArr[0].ToString(),storagePath);
            }
            else
            {
                filePath = Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, UploadPath));
            }
            if (Path.GetExtension(filePath) == ".doc" || Path.GetExtension(filePath) == ".docx")
                return File(filePath, "application/msword", DocumentName);
            else
                return File(filePath, "application/pdf", DocumentName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratedPatientReferrerToSupplierLetter(int caseID)
        {
            CasePatientReferrerSupplier caseInfo = Mapper.Map<CasePatientReferrerSupplier>(_caseService.GetCasePatientReferrerSupplierByCaseID(caseID));
            string fileName = string.Format("{0}.pdf", Guid.NewGuid().ToString());
            return File(_letterGeneration.GeneratePatientReferrerToSupplier(caseInfo, ITSUser), "application/pdf", fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneratedInvoiceLetter(int caseID, IEnumerable<CaseTreatmentPricingType> CaseTreatmentPricing, IEnumerable<CaseBespokeServicePricingType> CaseBespokePricing)
        {
            ITS.Domain.Models.CaseModel.CasePatientReferrer casePatientReferrer = Mapper.Map<ITS.Domain.Models.CaseModel.CasePatientReferrer>(_caseService.GetCasePatientReferrerByCaseID(caseID));
            decimal vat = _caseService.GetCaseVATByCaseID(caseID);
            string fileName = string.Format("{0}.pdf", Guid.NewGuid().ToString());
            decimal totalAmountOut;
            string invoiceNumberOut;
            return File(_letterGeneration.GenerateInvoice(casePatientReferrer, CaseTreatmentPricing, CaseBespokePricing, vat, Server.MapPath("~/Content/Images/innovate-logo.jpg"), out totalAmountOut, out invoiceNumberOut), "application/pdf", fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetGeneratedInitialAssessmentReport(int rptCaseID, int rptCaseAssessmentDetailID)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings["InitialAssessmentReportUrl"], rptCaseID, GlobalConst.ReportFormat.Word);
            return File(client.DownloadData(reportURL), "application/msword", string.Format("InitialAssessment-{0}.doc", rptCaseID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetGeneratedReviewAssessmentReport(int rptCaseID)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID,"Review Assessment",GlobalConst.ReportFormat.Word);
            return File(client.DownloadData(reportURL), "application/msword", string.Format("ReviewAssessment-{0}.doc", rptCaseID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetGeneratedFinalAssessmentReport(int rptCaseID)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID, "Final Assessment", GlobalConst.ReportFormat.Word);
            return File(client.DownloadData(reportURL), "application/msword", string.Format("FinalAssessment-{0}.doc", rptCaseID));
        }
    }
}