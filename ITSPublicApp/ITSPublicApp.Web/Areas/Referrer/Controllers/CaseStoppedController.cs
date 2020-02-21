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
 
namespace ITSPublicApp.Web.Areas.Referrer.Controllers
{
    [AuthorizedUserCheck("Referrer")]
    public class CaseStoppedController : BaseController
    {
         private readonly IAssessmentService _assessmentService;
        private readonly  ICaseService _caseService;
        private readonly ISupplierService _supplierService;
        private readonly ISupplierStorage _supplierStorageService;
        public readonly IEncryption _encryptionService;

        public CaseStoppedController(IAssessmentService assessmentService, ICaseService caseService, ISupplierService supplierService, ISupplierStorage supplierStorageService,IEncryption encryptionService)
        {
            _assessmentService = assessmentService;
            _caseService = caseService;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            _encryptionService = encryptionService;
 
        }

        public ActionResult Index(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            CaseStoppedViewModel viewModel = new CaseStoppedViewModel();
            viewModel.caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(cid));
            if (viewModel.caseObj.IsCustom)
            {
                var AssessmentServiceId = _caseService.GetCaseAssessmentCustomsByCaseID(cid);
                int SupplierrDocumentTypeID;
                string storagePath = ConfigurationManager.AppSettings["StoragePath"];
                if (AssessmentServiceId == 1)
                    SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.InitialAssessmentFinalCustom;
                else
                    SupplierrDocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.ReviewAssessmentSupplierCustom;
                IEnumerable<ITSPublicApp.Domain.Models.SupplierDocument> supLLierDocumentData = Mapper.Map<IEnumerable<Model.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(cid, SupplierrDocumentTypeID));
                foreach (var daattaa in supLLierDocumentData)
                    viewModel.DownloadPath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)viewModel.caseObj.SupplierID, viewModel.caseObj.ReferrerProjectTreatmentID, cid, daattaa.UploadPath, storagePath);
            }
            viewModel.caseObj.EncryptedCaseID = EncryptString(cid.ToString());
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult UpdateCaseAssessmentAuthorisationToClosedByCaseID(string caseID, string authorisationDeniedMsg)
        {
            int cid = Convert.ToInt32(DecryptString(caseID));
            _assessmentService.UpdateCaseAssessmentAuthorisationToDeniedByCaseID(cid, authorisationDeniedMsg);
            var caseObj = _caseService.GetCaseByCaseID(cid);
            _caseService.DeleteCaseTreatmentPricingByCaseIDCaseStopped(cid);
            if (caseObj.IsCustom)
                _caseService.UpdateCaseWorkflowCustomByCaseID(cid, ITSUser.UserID, (int)GlobalConst.WorkFlows.CaseStopped);
            else
                _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            return Json("Submit successfuly");
        }
    }
}
