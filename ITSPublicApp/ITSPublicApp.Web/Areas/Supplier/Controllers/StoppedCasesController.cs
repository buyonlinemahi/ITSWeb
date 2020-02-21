using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class StoppedCasesController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly IAssessmentService _assessmentService;
        private readonly ICaseService _caseService;
        private readonly IEncryption _encryptionService;
        ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;

        public StoppedCasesController(ISupplierService supplierService, IAssessmentService assessmentService, ICaseService caseService, ITSPublicApp.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage, IEncryption encryptionService)
        {
            _supplierService = supplierService;
            _assessmentService = assessmentService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
           _encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStoppedCases()
        {
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();
            _authorisationViewModel.supplierCasePatient = (Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierStoppedCases(ITSUser.SupplierID.Value)));
            _authorisationViewModel.notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(ITSUser.SupplierID.Value));
            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
                Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
                supplierCasePatientObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, supplierCasePatientObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                supplierCasePatientObj.EncryptedCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());

            }
            return Json(_authorisationViewModel);
        }

        [HttpGet]
        public ActionResult StoppedCaseScreen(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            var SupplierSoppedCaseViewModel = new SupplierStoppedCaseViewModel()
            {

                AuthorisationDetail = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(cid)).AuthorisationDetail,
                CaseID = cid
            };
            return View(SupplierSoppedCaseViewModel);
        }

        [HttpPost]
        public ActionResult UpdateCaseStopped(int caseID)
        {
            _caseService.UpdateCaseWorkFlow(caseID, ITSUser.UserID);
            return Json("CaseStopped Accepted Sucessfully", "text/html");
        }


    }
}