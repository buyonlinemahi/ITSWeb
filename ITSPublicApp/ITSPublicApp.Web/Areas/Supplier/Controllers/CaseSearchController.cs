using AutoMapper;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
using System;
using System.Net;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class CaseSearchController : BaseController
    {

        private readonly ICaseService _caseService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IReferrerStorage _referrerStorageService;
        private readonly ITSPublicApp.Web.ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly IEncryption _encryptionService;

        public CaseSearchController(
                ICaseService caseService,
                IPatientService patientService,
                ISupplierService supplierService,
                ISupplierStorage supplierStorageService,
                IAssessmentService assessmentService,
            IReferrerStorage referrerStorageService,
            IEncryption encryptionService
        )
        {
            _caseService = caseService;
            _patientService = patientService;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            _assessmentService = assessmentService;
            _referrerStorageService = referrerStorageService;
            _encryptionService = encryptionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetNotificationBubbleCountBySupplierID()
        {
            ITSPublicApp.Domain.Models.NotificationBubble notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(ITSUser.SupplierID.Value));
            return Json(notificationBubble,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(ITSPublicApp.Domain.ViewModels.CaseSearchCriteria caseSearchCriteria, int skip, int take)
        {
            int supplierID = ITSUser.SupplierID.Value;
            int userID = ITSUser.UserID;
            ITSPublicApp.Domain.ViewModels.PagedReferrerSupplierCaseSearch viewModel = new Domain.ViewModels.PagedReferrerSupplierCaseSearch();
            switch ((int)caseSearchCriteria.SearchCriteria)
            {
                case (int)GlobalConst.SupplierCaseSearchCriteria.PatientName:
                    var byPatientName = _caseService.GetReferrerSupplierCaseLikePatientNameAndSupplierID(caseSearchCriteria.SearchText, supplierID, userID, skip, take);
                    viewModel.Cases = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerSupplierCases>>(byPatientName.Cases);
                    viewModel.Cases = viewModel.Cases.GroupBy(x => x.CaseID).Select(y => y.First()).Distinct();
                    viewModel.TotalCount = byPatientName.TotalCount;
                    break;
                case (int)GlobalConst.SupplierCaseSearchCriteria.CaseNumber:
                    var byCaseNumber = _caseService.GetReferrerSupplierCaseLikeCaseNumberAndSupplierID(caseSearchCriteria.SearchText, supplierID, userID, skip, take);
                    viewModel.Cases = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerSupplierCases>>(byCaseNumber.Cases);
                    viewModel.Cases = viewModel.Cases.GroupBy(x => x.CaseID).Select(y => y.First()).Distinct();
                    viewModel.TotalCount = byCaseNumber.TotalCount;
                    break;
            }
            foreach (var objResult in viewModel.Cases)
            {
                objResult.EncryptCaseID = EncryptString(objResult.CaseID.ToString());
            }
            viewModel.CaseSearchCriteria = caseSearchCriteria;
            return Json(viewModel);
        }

        public ActionResult CaseDetail(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            ITSPublicApp.Domain.ViewModels.CaseDetailViewModel viewModel = new ITSPublicApp.Domain.ViewModels.CaseDetailViewModel();
            Model.Case caseObj = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(cid));
            viewModel.CasePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(cid));
            viewModel.CasePatientTreatment.IsCustom = caseObj.IsCustom;
            ViewBag.CaseID = id;
            if (caseObj.IsCustom)
            {
                var _caseAssessmentReportsCustom = Mapper.Map<IEnumerable<Model.SupplierDocument>>(from rk in _supplierService.GetSupplierDocumentByCaseId(cid).ToList()
                                                                                                   where rk.DocumentTypeID == GlobalConst.DocumentTypes.InitialAssessmentCustomFinal ||
                                                                                                   rk.DocumentTypeID == GlobalConst.DocumentTypes.ReviewAssessmentFinalCustom ||
                                                                                                   rk.DocumentTypeID == GlobalConst.DocumentTypes.FinalAssessmentFinalCustom
                                                                                                   select rk);
                string storagePath = ConfigurationManager.AppSettings["StoragePath"];
                foreach (var rk in _caseAssessmentReportsCustom)
                {
                    string strPath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)rk.SupplierID, (int)rk.ReferrerProjectTreatmentID, (int)rk.CaseId, rk.UploadPath, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]));
                    if (System.IO.File.Exists(strPath))
                        rk.FullPath = rk.UploadPath;                        
                }
                viewModel.caseAssessmentReportsCustom = _caseAssessmentReportsCustom;
            }
            else
            {
                viewModel.CaseAssessmentDetails = Mapper.Map<IEnumerable<Model.CaseAssessmentDetail>>(_assessmentService.GetQASubmitedCaseAssessmentDetailsByCaseID(cid));

                foreach (Model.CaseAssessmentDetail ca in viewModel.CaseAssessmentDetails)
                {
                    ca.EncryptCaseID = EncryptString(ca.CaseID.ToString());
                    ca.EncryptCaseAssessmentDetailID = EncryptString(ca.CaseAssessmentDetailID.ToString());
                    ca.EncryptAssessmentServiceID = EncryptString(ca.AssessmentServiceID.ToString());
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CaseUploads(string id)
        {            
            ViewBag.CaseID = id;
            int cid = Convert.ToInt32(DecryptString(id));
            IEnumerable<ITSPublicApp.Domain.Models.CaseDocumentUser> CaseDocumentUsers = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseDocumentUser>>(_caseService.GetCaseDocumentForSupplierUserByCaseID(cid));
            foreach (var objResult in CaseDocumentUsers)
            {
                objResult.EncryptedCaseID = EncryptString(objResult.CaseID.ToString());
            }
            return View(CaseDocumentUsers);
        }

        [HttpPost] ////// This method is used in partials
        public ActionResult CaseUploadsDocumentUser(string id)
        {
            // This is the case of when user get atleast of case uploads dcoument either from referrer, supplier or internal.
            ViewBag.CaseID = id;
            int cid = Convert.ToInt32(DecryptString(id));
            IEnumerable<ITSPublicApp.Domain.Models.CaseDocumentUser> CaseDocumentUsers = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseDocumentUser>>(_caseService.GetCaseDocumentUserByCaseID(cid));
            foreach (var objResult in CaseDocumentUsers)
            {
                objResult.EncryptedCaseID = EncryptString(objResult.CaseID.ToString());
            }
            return Json(CaseDocumentUsers);
        }

        [HttpGet]
        public ActionResult ViewCaseUploads(string UploadPath, string CaseId)
        {
            int cid = Convert.ToInt32(DecryptString(CaseId));
            var _case = _caseService.GetCaseByCaseID(cid);
            var filePath = "";
            string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
            filePath = _referrerStorageService.GetProjectTreatmentReferralCaseUploadFolderStoragePath(_case.ReferrerID, _case.ReferrerProjectTreatmentID, _case.CaseID, UploadPath, storagePath);

            if (Path.GetExtension(filePath.ToLower()) == ".doc" || Path.GetExtension(filePath.ToLower()) == ".docx")
                return File(filePath, "application/msword", UploadPath);
            else if (Path.GetExtension(filePath.ToLower()) == ".jpg" || Path.GetExtension(filePath.ToLower()) == ".jpeg")
                return File(filePath, "image/jpeg", UploadPath);
            else
                return File(filePath, "application/pdf", UploadPath);
        }
        
        [HttpPost]
        public JsonResult GetFinalUploadByCaseID(string caseID, string assessmentServiceID)
        {
            int _caseID = Convert.ToInt32(DecryptString(caseID));
            int _assessmentServiceID = Convert.ToInt32(DecryptString(assessmentServiceID));            
            int documentTypeID = 0;
            switch (_assessmentServiceID)
            {
                case 1: documentTypeID = GlobalConst.CaseDocumentTypeId.InitialAssessment;
                    break;
                case 2: documentTypeID = GlobalConst.CaseDocumentTypeId.ReviewAssessment;
                    break;
                case 3: documentTypeID = GlobalConst.CaseDocumentTypeId.FinalAssessment;
                    break;
            }
            var caseDocument = _caseService.GetFinalUploadByCaseID(_caseID, documentTypeID);
            if (caseDocument != null)
            {
                string path = GlobalConst.CaseDocumentDownloadPath + caseDocument.UploadPath;
                return Json(path);
            }
            else
            {
                return Json("");
            }
        }

        public FileResult Download(string _caseID, string fullPath)
        {
            int caseID = int.Parse(DecryptString(_caseID).ToString());
            string FileName = "";
            string DownloadPath = "#";
            var _resCase = _caseService.GetCaseByCaseID(caseID);
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            DownloadPath = Server.MapPath(_supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)_resCase.SupplierID, (int)_resCase.ReferrerProjectTreatmentID, (int)_resCase.CaseID, fullPath, storagePath));
            return File(client.DownloadData(DownloadPath), "application/pdf", FileName);
        }
    }
}
