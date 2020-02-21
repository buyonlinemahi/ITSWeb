using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class NewPatientsController : BaseController
    { 
        private readonly ISupplierService _supplierService;
        private readonly IReferrerStorage _referrerStorage;
        private readonly ICaseService _caseService;
        private readonly IEncryption _encryptionService;
        public NewPatientsController(ISupplierService supplierService, IReferrerStorage referrerStorage, ICaseService caseService, IEncryption encryptionService)
        {
            _supplierService = supplierService;
            _referrerStorage = referrerStorage;
            _caseService = caseService;
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetNewPatients()
        {
            AuthorisationViewModel _authorisationViewModel = new AuthorisationViewModel();
            _authorisationViewModel.supplierCasePatient = Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierNewPatientCases(ITSUser.SupplierID.Value));
            _authorisationViewModel.notificationBubble = Mapper.Map<Model.NotificationBubble>(_caseService.GetNotificationBubbleCountBySupplierID(ITSUser.SupplierID.Value));
            foreach (var supplierCasePatientObj in _authorisationViewModel.supplierCasePatient)
            {
                Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierCasePatientObj.CaseID));
                supplierCasePatientObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, supplierCasePatientObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                string _encryptCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());
                
                if ((supplierCasePatientObj.WorkflowID == (int)GlobalConst.WorkFlows.ReferredtoSupplier) || (supplierCasePatientObj.WorkflowID == (int)GlobalConst.WorkFlows.InitialAssessmentCustom))
                    supplierCasePatientObj.Url = Url.Action(GlobalConst.Actions.Area.Supplier.BookIA.Index, GlobalConst.Controllers.Area.Supplier.BookIA, new { id = _encryptCaseID });
                else
                    supplierCasePatientObj.Url = Url.Action(GlobalConst.Actions.Area.Supplier.TriageAssessment.Index, GlobalConst.Controllers.Area.Supplier.TriageAssessment, new { id = _encryptCaseID });

                supplierCasePatientObj.EncryptedCaseID = EncryptString(supplierCasePatientObj.CaseID.ToString());
            }
           
            return Json(_authorisationViewModel);
        }

    }
}
