using AutoMapper;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.PatientService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
using ITSPublicApp.Web.ITSService.UserService;

namespace ITSPublicApp.Web.Areas.Referrer.Controllers
{
    [AuthorizedUserCheck("Referrer")]
    public class CaseSearchController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSPublicApp.Web.ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSPublicApp.Web.ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;
        private readonly ITSPublicApp.Web.ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IReferrerStorage _referrerStorageService;
        private readonly IEncryption _encrytionService;
        private readonly ITSService.UserService.IUserService _userService;

        public CaseSearchController(ITSService.CaseService.ICaseService caseService, IPatientService patientService, ITSPublicApp.Web.ITSService.AssessmentService.IAssessmentService assessmentService, ITSPublicApp.Web.ITSService.LookUpService.ILookUpService lookUpService, ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage, ITSPublicApp.Web.ITSService.ReferrerService.IReferrerService referrerService,
        ITSService.SupplierService.ISupplierService supplierService, ISupplierStorage supplierStorageService, IReferrerStorage referrerStorageService, IEncryption encrytonService,
           IUserService userService)
        {
            _caseService = caseService;
            _patientService = patientService;
            _assessmentService = assessmentService;
            _lookUpService = lookUpService;
            _referrerStorage = referrerStorage;
            _referrerService = referrerService;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            _referrerStorageService = referrerStorageService;
            _encrytionService = encrytonService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var data = _userService.GetUserById(ITSUser.UserID);
            if(data.ReferrerTypes == "Referrer Admin")
            {
                ViewBag.Message = "Referrer Admin";
            }
            return View();
        }
        [HttpPost]
        public JsonResult Search(ITSPublicApp.Domain.ViewModels.CaseSearchCriteria caseSearchCriteria, int skip, int take)
        {
            int referrerID = ITSUser.ReferrerID.Value;
            int userID = ITSUser.UserID;
            ITSPublicApp.Domain.ViewModels.PagedReferrerSupplierCaseSearch viewModel = new Domain.ViewModels.PagedReferrerSupplierCaseSearch();

            switch ((int)caseSearchCriteria.SearchCriteria)
            {
                case (int)GlobalConst.ReferrerCaseSearchCriteria.PatientName:
                    var byPatientName = _caseService.GetReferrerSupplierCaseLikePatientNameAndReferrerID(caseSearchCriteria.AdditionalParam, caseSearchCriteria.SearchText, referrerID, userID, skip, take);
                    viewModel.Cases = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerSupplierCases>>(byPatientName.Cases);
                    viewModel.Cases = viewModel.Cases.GroupBy(x => x.CaseID).Select(y => y.First()).Distinct();
                    viewModel.TotalCount = byPatientName.TotalCount;
                    break;
                case (int)GlobalConst.ReferrerCaseSearchCriteria.ReferrerReferernce:
                    var byReferrerReferernce = _caseService.GetReferrerSupplierCaseLikeReferrerReferenceNumberAndReferrerID(caseSearchCriteria.AdditionalParam, caseSearchCriteria.SearchText, referrerID, userID, skip, take);
                    viewModel.Cases = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerSupplierCases>>(byReferrerReferernce.Cases);
                    viewModel.Cases = viewModel.Cases.GroupBy(x => x.CaseID).Select(y => y.First()).Distinct();
                    viewModel.TotalCount = byReferrerReferernce.TotalCount;
                    break;
                case (int)GlobalConst.GroupNameCaseSearchCriteria.GroupName:
                    var ByGroupdata = _referrerService.GetReferrerGroupUsersCasesByReferrerID(caseSearchCriteria.GroupName, referrerID, skip, take);
                    viewModel.Cases = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerSupplierCases>>(ByGroupdata.Cases);
                    viewModel.TotalCount = ByGroupdata.TotalCount;
                    break;
            }           
                if (viewModel.Cases.Count() != 0)
                {
                    foreach (var referrerCaseSearchObj in viewModel.Cases)
                    {
                        Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(referrerCaseSearchObj.CaseID));
                        referrerCaseSearchObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, referrerCaseSearchObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                        referrerCaseSearchObj.EncryptCaseID = EncryptString(referrerCaseSearchObj.CaseID.ToString());
                        referrerCaseSearchObj.EncryptInitialCaseAssessmentDetailID = EncryptString(referrerCaseSearchObj.InitialCaseAssessmentDetailID.ToString());
                        referrerCaseSearchObj.EncryptFinalCaseAssessmentDetailID = EncryptString(referrerCaseSearchObj.FinalCaseAssessmentDetailID.ToString());
                    }
                }                       
            viewModel.CaseSearchCriteria = caseSearchCriteria;
            return Json(viewModel);
        }

        public ActionResult CaseDetail(string id)
        {
            int Id = Convert.ToInt16(DecryptString(id.ToString()));
            ITSPublicApp.Domain.ViewModels.CaseDetailViewModel viewModel = new ITSPublicApp.Domain.ViewModels.CaseDetailViewModel();
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(Id));
            viewModel.CasePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(Id));
            viewModel.CasePatientTreatment.IsCustom = caseObj.IsCustom;
            viewModel.CasePatientTreatment.EncryptCaseID = id;
            ViewBag.CaseID = id;
            if (caseObj.IsCustom)
            {
                var _caseAssessmentReportsCustom = Mapper.Map<IEnumerable<SupplierDocument>>(from hp in _supplierService.GetSupplierDocumentByCaseId(Id).ToList()
                                                                                             where hp.DocumentTypeID == GlobalConst.DocumentTypes.InitialAssessmentCustomFinal ||
                                                                                             hp.DocumentTypeID == GlobalConst.DocumentTypes.ReviewAssessmentFinalCustom ||
                                                                                             hp.DocumentTypeID == GlobalConst.DocumentTypes.FinalAssessmentFinalCustom
                                                                                             select hp);
                string storagePath = ConfigurationManager.AppSettings["StoragePath"];
                foreach (var hp in _caseAssessmentReportsCustom)
                {
                    if (System.IO.File.Exists(_supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)hp.SupplierID, (int)hp.ReferrerProjectTreatmentID, (int)hp.CaseId, hp.UploadPath, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]))))
                        hp.FullPath = hp.UploadPath;
                }
                viewModel.caseAssessmentReportsCustom = _caseAssessmentReportsCustom;
            }
            else
                viewModel.CaseAssessmentDetails = Mapper.Map<IEnumerable<CaseAssessmentDetail>>(_assessmentService.GetQASubmitedCaseAssessmentDetailsByCaseID(Id));
            //******************** # 3498 *********************// 
            if (viewModel.CaseAssessmentDetails != null)
            {
                foreach (var referrerCaseSearchObj in viewModel.CaseAssessmentDetails)
                {
                    referrerCaseSearchObj.EncryptCaseID = EncryptString(referrerCaseSearchObj.CaseID.ToString());
                    referrerCaseSearchObj.EncryptAssessmentServiceID = EncryptString(referrerCaseSearchObj.AssessmentServiceID.ToString());
                    referrerCaseSearchObj.EncryptCaseAssessmentDetailID = EncryptString(referrerCaseSearchObj.CaseAssessmentDetailID.ToString());
                }
            }
            //******************** *********************//
            return View(viewModel);
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

            if (Path.GetExtension(fullPath.ToLower()) == ".doc" || Path.GetExtension(fullPath.ToLower()) == ".docx")
                return File(client.DownloadData(DownloadPath), "application/msword", FileName);
            else
            return File(client.DownloadData(DownloadPath), "application/pdf", FileName);
        }

        [HttpGet]
        public ActionResult CaseUploads(string id)
        {
            ViewBag.CaseID = id;
            int ID = Convert.ToInt16(DecryptString(id.ToString()));
            CaseDocumentUserViewModel caseDocumentUserViewModel = new CaseDocumentUserViewModel();
            caseDocumentUserViewModel.CaseDocumentUsers = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseDocumentUser>>(_caseService.GetCaseDocumentForReferrerUserByCaseID(ID));
            if (caseDocumentUserViewModel.CaseDocumentUsers != null)
            {
                foreach (var referrerCaseSearchObj in caseDocumentUserViewModel.CaseDocumentUsers)
                    referrerCaseSearchObj.EncryptedCaseID = EncryptString(referrerCaseSearchObj.CaseID.ToString());
            }
            caseDocumentUserViewModel.ReferrerDocumentTypes = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerDocumentType>, IEnumerable<ITSPublicApp.Domain.Models.ReferrerDocumentType>>(_referrerService.GetReferrerDocumentType());
            return View(caseDocumentUserViewModel);
        }

        [HttpGet]
        public ActionResult CaseTreatment(string id)
        {
            ViewBag.CaseID = id;
            int ID = Convert.ToInt16(DecryptString(id.ToString()));
            IEnumerable<ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModificationAuthority> ObjReferrerCaseAssessmentModificationAuthority = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModificationAuthority>>(_assessmentService.GetCaseTreatmentPricingByCaseID(ID));
            return View(ObjReferrerCaseAssessmentModificationAuthority);
        }
        
        [HttpGet]
        public ActionResult ViewCaseUploads(string UploadPath, string CaseId)
        {
            ViewBag.CaseID = CaseId;
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
            int _caseID = Convert.ToInt16(DecryptString(caseID.ToString()));
            int _assessmentServiceID = Convert.ToInt16(DecryptString(assessmentServiceID.ToString()));
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
                return Json("");
        }
       

        [HttpGet]
        public JsonResult GetReferrerGroupsByReferrerID()
        {
            IEnumerable<ReferrerGroup> _refGroups = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerGroup>, IEnumerable<ReferrerGroup>>(_referrerService.GetReferrerGroupUsersByreferrerID(ITSUser.ReferrerID.Value));
            return Json(_refGroups, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
    }
}