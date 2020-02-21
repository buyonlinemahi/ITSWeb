using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.SupplierService;
using Model = ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.Global;
using System.Configuration;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using System;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class ExistingPatientsNextAssessmentController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IReferrerService _referrerService;
        private readonly IEncryption _encryptionService; 
        ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;

        public ExistingPatientsNextAssessmentController(IAssessmentService assessmentService, ISupplierService supplierService, IReferrerService referrerService, ICaseService caseService, ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage, IEncryption encryptionService)
        {
            _supplierService = supplierService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
            _assessmentService = assessmentService;
            _referrerService = referrerService;
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetNextAssessments()
        {
            var supplierID = ITSUser.SupplierID.Value;
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();

            _authorisationViewModel.supplierCasePatient = (Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierExistingPatientsNextAssessments(supplierID)));
            _authorisationViewModel.notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(supplierID));

            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
               Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
               supplierCasePatientObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, supplierCasePatientObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
               supplierCasePatientObj.EncryptedCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());
            }
            return Json(_authorisationViewModel);
        }


        public FileResult Download(string _caseID)
        {
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();
            List<Model.ReferrerDocuments> _referrerDocument = new List<Model.ReferrerDocuments>();

            var DownloadPath = "";
            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
                Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
                supplierCasePatientObj.IsCustom = caseObj.IsCustom;
                if (caseObj.IsCustom)
                {
                    Model.CaseAssessmentCustom caseAssessmentCustom = Mapper.Map<Model.CaseAssessmentCustom>(_assessmentService.GetCaseAssessmentCustomByCaseID(supplierCasePatientObj.CaseID));
                   

                    if (caseAssessmentCustom.IsFurtherTreatment == true)
                        _referrerDocument = Mapper.Map<List<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(supplierCasePatientObj.CaseID, (int)GlobalConst.ReferrerDocumentTypeID.ReturnAssessmentCustom));
                    else
                        _referrerDocument = Mapper.Map<List<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(supplierCasePatientObj.CaseID, (int)GlobalConst.ReferrerDocumentTypeID.FinalAssessmentCustom));
                    supplierCasePatientObj.ReferralDownloadPathCustom = Server.MapPath(_referrerStorage.GetReferralUploadFolderStoragePath(_referrerDocument[0].ReferrerID, (int)_referrerDocument[0].ReferrerProjectTreatmentID, _referrerDocument[0].UploadPath, ConfigurationManager.AppSettings["StoragePath"]));
                    DownloadPath = supplierCasePatientObj.ReferralDownloadPathCustom;
                }

            }

            return File(DownloadPath, "application/word", _referrerDocument[0].UploadPath);
        }


        [HttpPost]
        public ActionResult GetEPNATreatmentSessionDetail(string _caseID)
        {

            int cid = Convert.ToInt32(DecryptString(_caseID));
            Model.EPNATreatmentSession _ePNATreatmentSession = Mapper.Map<Model.EPNATreatmentSession>(_caseService.GetEPNATreatmentSessionDetail(cid));
            return Json(_ePNATreatmentSession, JsonRequestBehavior.AllowGet);
        }
    }
}