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
    public class ModifyAuthorityController : BaseController
    {
        //
        // GET: /Referrer/ModifyAuthority/
        private readonly IAssessmentService _assessmentService;
        private readonly ICaseService _caseService;
        private readonly ISupplierService _supplierService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IEncryption _encryptionService;
        public ModifyAuthorityController(IAssessmentService assessmentService, ICaseService caseService,ISupplierService supplierService, ISupplierStorage supplierStorageService,IEncryption encryptionService)
        {
            _assessmentService = assessmentService;
            _caseService = caseService;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            _encryptionService = encryptionService;

        }
        public ActionResult Index(string id)
        {

            ModifyAuthorityViewModel modifyAuthorityViewModel = new ModifyAuthorityViewModel();
            int cid =Convert.ToInt32(DecryptString(id));
            modifyAuthorityViewModel.CaseID = cid;
            modifyAuthorityViewModel.CaseNumber = _caseService.GetCaseByCaseID(cid).CaseNumber;
            modifyAuthorityViewModel.cases = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(cid));
            if (modifyAuthorityViewModel.cases.IsCustom)
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
                    modifyAuthorityViewModel.DownloadPath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath2((int)modifyAuthorityViewModel.cases.SupplierID, modifyAuthorityViewModel.cases.ReferrerProjectTreatmentID, cid, daattaa.UploadPath, storagePath);

            }
            modifyAuthorityViewModel.EncryptedCaseID = EncryptString(modifyAuthorityViewModel.CaseID.ToString());
            
            return View(modifyAuthorityViewModel);
       
        }

        [HttpPost]
        public JsonResult UpdateCaseAssessmentAuthorisationToModifiedByCaseID(int caseID, string authorisationModification, int treatmentSession, int assessmentServiceID)
        {
            _assessmentService.UpdateCaseAssessmentAuthorisationToModifiedByCaseID(caseID, authorisationModification);
            Model.ReferrerCaseAssessmentModification objReferrerCaseAssessmentModification = new Model.ReferrerCaseAssessmentModification();

            objReferrerCaseAssessmentModification.CaseID = caseID;
            objReferrerCaseAssessmentModification.TreatmentSession = treatmentSession;
            objReferrerCaseAssessmentModification.AssessmentServiceID = assessmentServiceID;

            _assessmentService.AddReferrerCaseAssessmentModification(Mapper.Map<Model.ReferrerCaseAssessmentModification, ITSService.AssessmentService.ReferrerCaseAssessmentModification>(objReferrerCaseAssessmentModification));

            ModifyAuthorityViewModel modifyAuthorityViewModel = new ModifyAuthorityViewModel();
            modifyAuthorityViewModel.cases = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(caseID));
            if(!modifyAuthorityViewModel.cases.IsCustom)
                _caseService.UpdateCaseWorkFlow(caseID, ITSUser.UserID);
            else
                _caseService.UpdateCaseWorkflowCustomByCaseID(caseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoInnovateCustom);
            return Json("Submit successfuly");
        }

        //Case Modifiy treatment Pricing Grid Method
        [HttpPost]
        public ActionResult GetReferrerCaseModificatinTreatments(string CaseID)
        {
            int cid = Convert.ToInt32(DecryptString(CaseID));
            var CaseAssessmentModifications = Mapper.Map<IEnumerable<ReferrerCaseAssessmentModificationAuthority>>(_assessmentService.GetCaseTreatmentPricingByCaseID(cid));
            return Json(CaseAssessmentModifications);
        }

    }
}
