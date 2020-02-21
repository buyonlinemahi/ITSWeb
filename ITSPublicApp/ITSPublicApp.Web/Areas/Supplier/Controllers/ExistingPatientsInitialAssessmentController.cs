using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
 
namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
      [AuthorizedUserCheck("Supplier")]
    public class ExistingPatientsInitialAssessmentController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ICaseService _caseService;
        private readonly IReferrerService _referrerService;
        private readonly IEncryption _encryptionService;
        ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;


        public ExistingPatientsInitialAssessmentController(ISupplierService supplierService, ICaseService caseService, IReferrerService referrerService, ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage,IEncryption encryptionService)
        {
            _supplierService = supplierService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
            _referrerService = referrerService;
            _encryptionService= encryptionService;

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetInitialAssessments()
        {
            var supplierID = ITSUser.SupplierID.Value;
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();
            _authorisationViewModel.supplierCasePatient = (Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierExistingPatientsInitialAssessments(supplierID)));
            _authorisationViewModel.notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(supplierID));
            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
                Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
                supplierCasePatientObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, supplierCasePatientObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                supplierCasePatientObj.IsCustom = caseObj.IsCustom;
                supplierCasePatientObj.strInitialAssessmentDate = supplierCasePatientObj.InitialAssessmentDate.ToString("MM/dd/yyyy");
                supplierCasePatientObj.EncryptedCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());
            }
            return Json(_authorisationViewModel);
        }
        public FileResult Download(string _caseID)
        {
            int caseID = int.Parse(DecryptString(_caseID).ToString());
            var _resCase = _caseService.GetCaseByCaseID(caseID);
            string strStoragePath = ConfigurationManager.AppSettings["StoragePath"];
            List<Model.ReferrerDocuments> ReferrerDocument = Mapper.Map<List<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(caseID, (int)GlobalConst.ReferrerDocumentTypeID.InitialAssessmentCustom));
            string filestoragePath = Server.MapPath(_referrerStorage.GetReferralUploadFolderStoragePath(_resCase.ReferrerID, _resCase.ReferrerProjectTreatmentID, ReferrerDocument[0].UploadPath, strStoragePath.Trim()));
            WebClient client = new WebClient();
            return File(client.DownloadData(filestoragePath), "application/pdf", ReferrerDocument[0].UploadPath);
        }
    }
}
