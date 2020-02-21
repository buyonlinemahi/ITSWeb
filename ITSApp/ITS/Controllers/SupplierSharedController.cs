using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;
using ITS.Domain.Models.SupplierModel;
using AutoMapper;
using System.Configuration;
using ITS.Infrastructure.Global;
using System.IO;
using Case = ITS.Domain.Models.CaseModel.Case;
using TreatmentCategoriesPricingTypes = ITS.Domain.Models.TreatmentCategoryModel.TreatmentCategoriesPricingTypes;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class SupplierSharedController : BaseController
    {
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.ISupplierStorage _supplierStorageService;
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IEmail _emailService;

        public SupplierSharedController(ITSService.SupplierService.ISupplierService supplierService, ITS.Infrastructure.ApplicationServices.Contracts.ISupplierStorage supplierStorage, ITSService.ReferrerService.IReferrerService referrerService, ITSService.CaseService.ICaseService caseService, ITS.Infrastructure.ApplicationServices.Contracts.IEmail emailService)
        {
            _supplierService = supplierService;
            _supplierStorageService = supplierStorage;
            _referrerService = referrerService;
            _caseService = caseService;
            _emailService = emailService;
        }

        [HttpPost]
        public JsonResult GetAllSupplier()
        {
            IEnumerable<ITS.Domain.Models.SupplierModel.Supplier> supplier = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierModel.Supplier>>(_supplierService.GetAllSupplier());
            return Json(supplier);
        }

        [HttpPost]
        public JsonResult UpdateSupplierStatus(int supplierID, bool isTriage)
        {
            return Json(_supplierService.UpdateSupplierStatus(supplierID, isTriage));
        }

        [HttpPost]
        public JsonResult UpdateSupplier(ITS.Domain.Models.SupplierModel.Supplier supplier)
        {
            try
            {               
                _supplierService.UpdateSupplierBySupplierID(Mapper.Map<ITSService.SupplierService.Supplier>(supplier));
                supplier.StatusID = _supplierService.UpdateSupplierStatus(supplier.SupplierID, supplier.IsTriage);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("duplicate"))//Duplicate email check
                supplier.Message = "Email already exists"; 
                else
                    _emailService.CreateErrorLog(e.Message, e.StackTrace);
            }
            return Json(supplier);
        }

        [HttpPost]
        public JsonResult SupplierExists(string value, string field)
        {
            return Json(new { valid = !_supplierService.GetSupplierExistsByName(value), message = "Supplier already exists", value = value });
        }

        [HttpPost]
        public JsonResult SupplierEmailExists(string value, string field)
        {
            return Json(new { valid = !_supplierService.GetSupplierExistsByEmail(value), message = "Email already exists", value = value });
        }

        [HttpPost]
        public JsonResult GetCaseLikeCaseNumber(string caseNumber)
        {
            IEnumerable<Case> @case = Mapper.Map<IEnumerable<Case>>(_caseService.GetCaseByLikeCaseNumber(caseNumber));
            return Json(@case);
        }

        [HttpPost]
        public JsonResult GetCasePatientLikeCaseNumber(string caseNumber)
        {
            IEnumerable<CasePatientSearch> @case = Mapper.Map<IEnumerable<CasePatientSearch>>(_caseService.GetCasePatientLikeCaseNumber(caseNumber));
            return Json(@case);
        }

        [HttpPost]
        public JsonResult AddTreatmentCategory(ITS.Domain.ViewModels.SupplierTreatmentCategoryPricingViewModel treatmentCategoryPricing)
        {
            treatmentCategoryPricing.Enabled = true;
            int result = _supplierService.AddSupplierTreatment(Mapper.Map<ITSService.SupplierService.SupplierTreatment>(treatmentCategoryPricing));
            if (result == -1)
                return Json(-1, "text/html");

            treatmentCategoryPricing.SupplierTreatmentID = result;
            treatmentCategoryPricing.Pricing.ToList().ForEach(price =>
            {
                price.SupplierTreatmentID = treatmentCategoryPricing.SupplierTreatmentID;
                price.PricingID = _supplierService.AddSupplierTreatmentPricing(Mapper.Map<ITSService.SupplierService.SupplierTreatmentPricing>(price));
            });

            treatmentCategoryPricing.Pricing = Mapper.Map<IList<TreatmentPricing>>(_supplierService.GetSupplierTreatmentPricingBySupplierTreatmentIDAndTreatmentCategoryID(treatmentCategoryPricing.SupplierTreatmentID, treatmentCategoryPricing.TreatmentCategoryID));

            return Json(treatmentCategoryPricing, "text/html");
        }

        [HttpPost]
        public JsonResult UpdateTreatmentCategoryPricing(List<TreatmentPricing> pricings)
        {
            pricings.ToList().ForEach(price =>
            {
                if (price.PricingID == 0)
                    price.PricingID = _supplierService.AddSupplierTreatmentPricing(Mapper.Map<ITSService.SupplierService.SupplierTreatmentPricing>(price));
                else
                    _supplierService.UpdateSupplierTreatmentPricingByPricingID(Mapper.Map<ITSService.SupplierService.SupplierTreatmentPricing>(price));
            });
            return Json(pricings, "text/html");
        }

        [HttpPost]
        public JsonResult GetTreatmentCategoriesPricingTypesByTreatmentCategoryID(int treatmentCategoryID)
        {
            return Json(Mapper.Map<IEnumerable<TreatmentCategoriesPricingTypes>>(_referrerService.GetPricingTypesByTreatmentCategoryID(treatmentCategoryID)));
        }

        [HttpPost]
        public PartialViewResult GetSupplierTreatmentCategories(int supplierID)
        {
            return PartialView("Supplier/_TreatmentCategoryGridPartial");
        }

        #region SupplierSiteAudit Model
        [HttpPost]
        public JsonResult AddSiteAudit(SiteAudit siteAudit)
        {
            const string supplierDocumentTypeName = GlobalConst.SupplierDocumentType.SupplierAudit;

            if (siteAudit.FileUploadSiteAudit != null)
            {
                siteAudit.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.SupplierAudit;
                siteAudit.UploadPath = AddDocumentFile(siteAudit.FileUploadSiteAudit, siteAudit.SupplierID, supplierDocumentTypeName);
                siteAudit.UploadDate = DateTime.Now;
                siteAudit.SupplierDocumentID = _supplierService.AddSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(siteAudit));
            }
            siteAudit.SupplierSiteAuditID = _supplierService.AddSupplierSiteAudit(Mapper.Map<ITSService.SupplierService.SupplierSiteAudit>(siteAudit));
            siteAudit.FileUploadSiteAudit = null;
            siteAudit.DocumentUrl = this.GetDocumentUrl(siteAudit, supplierDocumentTypeName);

            return Json(siteAudit, "text/html");
        }

        [HttpPost]
        public JsonResult UpdateSiteAudit(SiteAudit siteAudit)
        {
            const string supplierDocumentTypeName = GlobalConst.SupplierDocumentType.SupplierAudit;

            if (siteAudit.FileUploadSiteAudit != null)
            {
                siteAudit.AuditDate = DateTime.Now;
                siteAudit.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.SupplierAudit;
                siteAudit.UploadDate = DateTime.Now.Date;
                siteAudit.UserID = ITSUser.UserID;
                this.DeleteFileDocument(siteAudit, supplierDocumentTypeName);
                siteAudit.UploadPath = AddDocumentFile(siteAudit.FileUploadSiteAudit, siteAudit.SupplierID, supplierDocumentTypeName);

                _supplierService.UpdateSupplierSiteAuditBySupplierSiteAuditID(Mapper.Map<ITSService.SupplierService.SupplierSiteAudit>(siteAudit));
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(siteAudit));

                siteAudit.DocumentUrl = this.GetDocumentUrl(siteAudit, supplierDocumentTypeName);

                siteAudit.FileUploadSiteAudit = null;
            }
            else
            {
                siteAudit.UploadPath = null;
                _supplierService.UpdateSupplierSiteAuditBySupplierSiteAuditID(Mapper.Map<ITSService.SupplierService.SupplierSiteAudit>(siteAudit));
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(siteAudit));
            }
            return Json(siteAudit, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteSupplierSiteAudit(SiteAudit siteAudit)
        {
            this.DeleteFileDocument(siteAudit, GlobalConst.SupplierDocumentType.SupplierAudit);
            _supplierService.DeleteSupplierSiteAuditBySupplierSiteAuditID(siteAudit.SupplierSiteAuditID);
            _supplierService.DeleteSupplierDocumentBySupplierDocumentID(siteAudit.SupplierDocumentID);
            return Json(GlobalResource.DeletedSuccessfully);
        }
        #endregion

        #region Registration Documents
        [HttpPost]
        public JsonResult AddSupplierRegistrationDocument(ITS.Domain.Models.SupplierModel.Registration registration)
        {
            if (registration.RegistrationDocumentFileUpload != null &&
                GlobalConst.FileExtensions.PDF.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                (GlobalConst.FileExtensions.DOC.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                GlobalConst.FileExtensions.DOCX.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                GlobalConst.FileExtensions.JPEG.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                GlobalConst.FileExtensions.JPG.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                GlobalConst.FileExtensions.TIF.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower()) ||
                GlobalConst.FileExtensions.TIFF.ToLower() == Path.GetExtension(registration.RegistrationDocumentFileUpload.FileName.ToLower())))
            {
                registration.UploadDate = DateTime.Now;
                registration.SupplierID = registration.SupplierID;
                registration.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.Registration;
                registration.UploadPath = AddDocumentFile(registration.RegistrationDocumentFileUpload,
                    registration.SupplierID,
                    GlobalConst.SupplierDocumentType.Registration);
                registration.UserID = ITSUser.UserID;
                registration.UserName = ITSUser.UserName;
                registration.SupplierDocumentID = _supplierService.AddSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(registration));
                registration.DocumentUrl = Url.Action(GlobalConst.Actions.SupplierSharedController.GetDocumentFile,
                    GlobalConst.Controllers.SupplierShared,
                    new
                    {
                        supplierID = registration.SupplierID,
                        fileName = registration.UploadPath,
                        supplierDocumentType = GlobalConst.SupplierDocumentType.Registration
                    });
                registration.RegistrationDocumentFileUpload = null;
            }

            return Json(registration, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateSupplierRegistrationDocument(ITS.Domain.Models.SupplierModel.Registration registration)
        {
            const string supplierDocumentTypeName = GlobalConst.SupplierDocumentType.Registration;
            if (registration.RegistrationDocumentFileUpload != null)
            {
                this.DeleteFileDocument(registration, GlobalConst.SupplierDocumentType.Registration);
                registration.UploadPath = AddDocumentFile(registration.RegistrationDocumentFileUpload, registration.SupplierID, supplierDocumentTypeName);
                registration.UploadDate = DateTime.Now.Date;
                registration.UserID = ITSUser.UserID;
                registration.UserName = ITSUser.UserName;
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(registration));

                registration.DocumentUrl = this.GetDocumentUrl(registration, supplierDocumentTypeName);

                registration.RegistrationDocumentFileUpload = null;
            }
            else
            {
                registration.UploadPath = null;
                registration.UserID = ITSUser.UserID;
                registration.UserName = ITSUser.UserName;
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(registration));
            }
            return Json(registration, "text/html");
        }


        [HttpPost]
        public JsonResult DeleteRegistrationDocument(ITS.Domain.Models.SupplierModel.Registration supplierDocument)
        {
            if (supplierDocument.SupplierDocumentID > 0)
            {
                this.DeleteFileDocument(supplierDocument, GlobalConst.SupplierDocumentType.Registration);
                _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierDocument.SupplierDocumentID);
            }

            return Json(GlobalResource.DeletedSuccessfully);
        }
        #endregion

        #region Complaints
        [HttpPost]
        public ActionResult AddSupplierComplaint(Complaint complaint)
        {
            complaint.SupplierComplaintID = _supplierService.AddSupplierComplaint(Mapper.Map<ITSService.SupplierService.SupplierComplaint>(complaint));
            return Json(complaint, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateSupplierComplaint(Complaint complaint)
        {
            _supplierService.UpdateSupplierComplaintBySupplierComplaintID(Mapper.Map<ITSService.SupplierService.SupplierComplaint>(complaint));
            return Json(complaint, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteSupplierComplaint(int supplierComplaintID)
        {
            int returnValue = _supplierService.DeleteSupplierComplaintBySupplierComplaintID(supplierComplaintID);
            return Json(returnValue == 1 ? GlobalResource.DeletedSuccessfully : GlobalResource.UnableToCompleteAction);
        }
        #endregion

        #region ClinicalAudit
        [HttpPost]
        public ActionResult AddSupplierClinicalAudit(ClinicalAudit clinicalAudit)
        {
            const string supplierDocumentTypeName = GlobalConst.SupplierDocumentType.SupplierClinicalAudit;

            if (clinicalAudit.ClinicalAuditDocumentFileUpload != null)
            {
                clinicalAudit.UploadDate = DateTime.Now.Date;
                clinicalAudit.UploadPath = AddDocumentFile(
                    clinicalAudit.ClinicalAuditDocumentFileUpload,
                    clinicalAudit.SupplierID,
                    supplierDocumentTypeName);
                clinicalAudit.DocumentName = clinicalAudit.DocumentName;
                clinicalAudit.SupplierID = clinicalAudit.SupplierID;
                clinicalAudit.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.SupplierClinicalAudit;
                clinicalAudit.UserID = clinicalAudit.UserID;
                clinicalAudit.SupplierDocumentID = _supplierService.AddSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(clinicalAudit));
                clinicalAudit.SupplierClinicalAuditID = _supplierService.AddSupplierClinicalAudit(Mapper.Map<ITSService.SupplierService.SupplierClinicalAudit>(clinicalAudit));

                clinicalAudit.DocumentUrl = this.GetDocumentUrl(clinicalAudit, supplierDocumentTypeName);
                clinicalAudit.ClinicalAuditDocumentFileUpload = null;
            }

            return Json(clinicalAudit, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateSupplierClinicalAudit(ClinicalAudit clinicalAudit)
        {
            const string supplierDocumentTypeName = GlobalConst.SupplierDocumentType.SupplierClinicalAudit;

            if (clinicalAudit.ClinicalAuditDocumentFileUpload != null)
            {
                clinicalAudit.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.SupplierClinicalAudit;
                clinicalAudit.UploadDate = DateTime.Now.Date;
                this.DeleteFileDocument(clinicalAudit, supplierDocumentTypeName);
                clinicalAudit.UploadPath = AddDocumentFile(
                    clinicalAudit.ClinicalAuditDocumentFileUpload, clinicalAudit.SupplierID,
                    supplierDocumentTypeName);

                _supplierService.UpdateSupplierClinicalAuditBySupplierClinicalAuditID(
                    Mapper.Map<ITSService.SupplierService.SupplierClinicalAudit>(clinicalAudit));
                _supplierService.UpdateSupplierDocument(
                    Mapper.Map<ITSService.SupplierService.SupplierDocument>(clinicalAudit));

                clinicalAudit.DocumentUrl = this.GetDocumentUrl(clinicalAudit, supplierDocumentTypeName);
                clinicalAudit.ClinicalAuditDocumentFileUpload = null;
            }
            else
            {
                clinicalAudit.UploadPath = null;
                _supplierService.UpdateSupplierClinicalAuditBySupplierClinicalAuditID(
                    Mapper.Map<ITSService.SupplierService.SupplierClinicalAudit>(clinicalAudit));
                _supplierService.UpdateSupplierDocument(
                    Mapper.Map<ITSService.SupplierService.SupplierDocument>(clinicalAudit));

            }
            return Json(clinicalAudit, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteClinicalAudit(ITS.Domain.Models.SupplierModel.ClinicalAudit clinicalAudit)
        {
            this.DeleteFileDocument(clinicalAudit, GlobalConst.SupplierDocumentType.SupplierClinicalAudit);
            _supplierService.DeleteSupplierClinicalAuditBySupplierClinicalAuditID(clinicalAudit.SupplierClinicalAuditID);
            _supplierService.DeleteSupplierDocumentBySupplierDocumentID(clinicalAudit.SupplierDocumentID);
            return Json(GlobalResource.DeletedSuccessfully);
        }
        #endregion

        #region Insurance methods
        [HttpPost]
        public ActionResult AddSupplierInsurance(Insurance insurance)
        {
            if (insurance.InsuredDocumentFile != null)
            {
                insurance.UserID = ITSUser.UserID;
                insurance.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.Insurance;
                insurance.DocumentName = insurance.DocumentName;
                insurance.SupplierID = insurance.SupplierID;
                insurance.UploadDate = DateTime.Now.Date;
                insurance.UploadPath = AddDocumentFile(insurance.InsuredDocumentFile, insurance.SupplierID, GlobalConst.SupplierDocumentType.Insurance);
                insurance.SupplierDocumentID = _supplierService.AddSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(insurance));
                insurance.SupplierInsuredID = _supplierService.AddSupplierInsurance(Mapper.Map<ITSService.SupplierService.SupplierInsurance>(insurance));
                insurance.DocumentUrl = this.GetDocumentUrl(insurance, GlobalConst.SupplierDocumentType.Insurance);
                insurance.InsuredDocumentFile = null;
            }

            return Json(insurance, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateSupplierInsurance(Insurance insurance)
        {
            const int documentTypeID = GlobalConst.SupplierDocumentTypeId.Insurance;
            const string documentTypeName = GlobalConst.SupplierDocumentType.Insurance;

            if (insurance.InsuredDocumentFile != null)
            {
                insurance.DocumentTypeID = documentTypeID;
                insurance.DocumentName = insurance.DocumentName;
                insurance.SupplierID = insurance.SupplierID;
                insurance.UploadDate = DateTime.Now.Date;
                insurance.UserID = ITSUser.UserID;
                this.DeleteFileDocument(insurance, documentTypeName);
                insurance.UploadPath = AddDocumentFile(insurance.InsuredDocumentFile, insurance.SupplierID, documentTypeName);

                _supplierService.UpdateSupplierInsurance(Mapper.Map<ITSService.SupplierService.SupplierInsurance>(insurance));
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(insurance));

                insurance.DocumentUrl = this.GetDocumentUrl(insurance, GlobalConst.SupplierDocumentType.Insurance);

                insurance.InsuredDocumentFile = null;
            }
            else
            {
                insurance.UploadPath = null;
                _supplierService.UpdateSupplierInsurance(Mapper.Map<ITSService.SupplierService.SupplierInsurance>(insurance));
                _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(insurance));
            }
            return Json(insurance, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteInsurance(Insurance supplierInsurance)
        {
            this.DeleteFileDocument(supplierInsurance, GlobalConst.SupplierDocumentType.Insurance);
            _supplierService.DeleteSupplierInsuranceBySupplierInsuredID(supplierInsurance.SupplierInsuredID);
            _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierInsurance.SupplierDocumentID);
            return Json(GlobalResource.DeletedSuccessfully);
        }
        #endregion


        public FileResult GetDocumentFile(int supplierID, string fileName, string supplierDocumentType)
        {
            string uploadPath = _supplierStorageService.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, fileName, supplierDocumentType);
            return File(uploadPath, GlobalConst.ContentTypes.PDF, fileName);
        }

        [NonAction]
        private string AddDocumentFile(HttpPostedFileBase file, int supplierID, string supplierDocumentTypeName)
        {
            var path = _supplierStorageService.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, supplierDocumentTypeName);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }

        [NonAction]
        private void DeleteFileDocument(Document document, string supplierDocumentType)
        {
            System.IO.File.Delete(
                _supplierStorageService.GetSupplierStoragePath(
                    Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]),
                    document.SupplierID,
                    document.UploadPath,
                    supplierDocumentType));
        }

        [NonAction]
        private string GetDocumentUrl(Document document, string supplierDocumentTypeName)
        {
            return Url.Action(
                GlobalConst.Actions.SupplierSharedController.GetDocumentFile,
                GlobalConst.Controllers.SupplierShared,
                new
                {
                    supplierID = document.SupplierID,
                    fileName = document.UploadPath,
                    supplierDocumentType = supplierDocumentTypeName
                });
        }

        [HttpPost]
        public ActionResult SearchSupplier(ITS.Domain.ViewModels.SupplierSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
        {

            switch (searchModel.SupplierSearch.SearchCriteria)
            {
                case (int)GlobalConst.SupplierSearchCriteria.SupplierName:
                    var byNameResults = _supplierService.GetSuppliersLikeSupplierName(searchModel.SupplierSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Suppliers = Mapper.Map<IEnumerable<SupplierSearchResult>>(byNameResults.SupplierSearchs);
                    searchModel.TotalCount = byNameResults.SupplierSearchTotalCount;
                    break;
                case (int)GlobalConst.SupplierSearchCriteria.PostCode:
                    var byPostCodeResults = _supplierService.GetSuppliersLikePostCode(searchModel.SupplierSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Suppliers = Mapper.Map<IEnumerable<SupplierSearchResult>>(byPostCodeResults.SupplierSearchs);
                    searchModel.TotalCount = byPostCodeResults.SupplierSearchTotalCount;
                    break;
                case (int)GlobalConst.SupplierSearchCriteria.TreatmentCategory:
                    var byTreatmentResults = _supplierService.GetSupplierLikeTreatmentCategoryType(searchModel.SupplierSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Suppliers = Mapper.Map<IEnumerable<SupplierSearchResult>>(byTreatmentResults.SupplierSearchs);
                    searchModel.TotalCount = byTreatmentResults.SupplierSearchTotalCount;
                    break;
            }
            return Json(searchModel, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteSupplierPractitioner(SupplierPractitioner supplierPractitioner)
        {
            _supplierService.DeleteSupplierPractitionerBySupplierPractitionerID(supplierPractitioner.SupplierPractitionerID);
            return Json(GlobalResource.DeletedSuccessfully);
        }
    }
}
