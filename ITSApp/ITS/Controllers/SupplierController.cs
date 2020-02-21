using System.Collections.Generic;
using System.Web.Mvc;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;
using ITS.Domain.Models.SupplierModel;
using AutoMapper;
using System.Linq;
using System.Configuration;
using System.Web;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;
using System;
using ITSWebApp.ITSService.UserService;
using Supplier = ITS.Domain.Models.SupplierModel.Supplier;
using SupplierSearch = ITS.Domain.Models.SupplierModel.SupplierSearch;
using ITS.Infrastructure.ApplicationFilters;

#region Comments
/*
 * Latest Version   : 1.2
 *
 * Modified By : Harpreet Singh
 * Date        : 18-Dec-2012
 * Description : Added the Methods AddSupplier
 * Description : Added the Methods SupplierAutoCompleteByName
 * Description : Added the Methods UpdateSupplier
 * Description : Added the Methods SupplierAutoCompleteByPostalCode
 * Description : Added the Methods GetSupplierBySupplierId
 * Version     : 1.0
 * 
 * 
   Modified By : Harpreet Singh
 * Date        : 21-Dec-2012
 * Description : modified the addsupplierandtreatment method
 * Version     : 1.1

 * Modified By : Pardeep Kumar
 * Date        : 21-Dec-2012
 * Version     : 1.2
 * Purposes    : Updated method AddSupplierAndTreatmentTypes to Create Supplier folders when a new Supplier is created
 */
#endregion
namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class SupplierController : BaseController
    {
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        public readonly ITSService.CaseService.ICaseService _caseService;
        private readonly IUserService _userService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.ISupplierStorage _supplierStorageService;

        public SupplierController(ITSService.SupplierService.ISupplierService supplierService, ITS.Infrastructure.ApplicationServices.Contracts.ISupplierStorage supplierStorage,
            ITSService.ReferrerService.IReferrerService referrerService, ITSService.CaseService.ICaseService caseService, ITSService.UserService.IUserService userService)
        {
            _supplierService = supplierService;
            _supplierStorageService = supplierStorage;
            _referrerService = referrerService;
            _caseService = caseService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel()
                {
                    Suppliers = Mapper.Map<IEnumerable<Supplier>>(_supplierService.GetSuppliersRecentlyAdded())
                };
            
            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SupplierSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
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
            
            return View(searchModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //for saving a new supplier
        [HttpPost]
        public ActionResult Save(Supplier supplier)
        {
            supplier.SupplierID = _supplierService.AddSupplier(Mapper.Map<ITSService.SupplierService.Supplier>(supplier));
            bool supplierFolderCreated = _supplierStorageService.CreateSupplierFolder(supplier.SupplierID, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]));
            return RedirectToAction(GlobalConst.Actions.SupplierController.Detail, new { supplierID = supplier.SupplierID });
        }

        //for updating a supplier
        [HttpPost]
        public ActionResult Update(Supplier supplier)
        {
            return null;
        }


        [HttpGet]
        public ActionResult Detail(int? supplierID /*made it nullable temporarily for prototyping*/)
        {
            if (!supplierID.HasValue)
                return RedirectToAction(GlobalConst.Actions.SupplierController.Index);

            SupplierViewModel viewModel = new SupplierViewModel();//populate supplier view model
            viewModel.Supplier = Mapper.Map<Supplier>(_supplierService.GetSupplierBySupplierID(supplierID.Value));
            double ranking = (_supplierService.GetSupplierRankingBySupplierID(supplierID.Value));
            viewModel.Supplier.Ranking = (System.Double.IsNaN(ranking) ? "N/A" : ranking.ToString());

            viewModel.SupplierPracitioners = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierModel.PractitionerTreatmentRegistration>>(_supplierService.GetSupplierPractitionerTreatmentRegistrationsBySupplierID(supplierID.Value));
            
            viewModel.SupplierClinicalAudits = Mapper.Map<IEnumerable<ClinicalAudit>>(_supplierService.GetSupplierClinicalAuditSupplierDocumentBySupplierID(supplierID.Value));
            viewModel.SupplierClinicalAudits.ToList().ForEach(clinicalAudit => clinicalAudit.DocumentUrl = Url.Action(GlobalConst.Actions.SupplierSharedController.GetDocumentFile, GlobalConst.Controllers.SupplierShared, new { supplierID = clinicalAudit.SupplierID, fileName = clinicalAudit.UploadPath, supplierDocumentType = GlobalConst.SupplierDocumentType.SupplierClinicalAudit }));

            viewModel.SupplierSiteAudits = Mapper.Map<IEnumerable<SiteAudit>>(_supplierService.GetSupplierSiteAuditSupplierDocumentBySupplierID(supplierID.Value));
            viewModel.SupplierSiteAudits.ToList().ForEach(siteAudit => siteAudit.DocumentUrl = Url.Action(GlobalConst.Actions.SupplierSharedController.GetDocumentFile, GlobalConst.Controllers.SupplierShared, new { supplierID = siteAudit.SupplierID, fileName = siteAudit.UploadPath, supplierDocumentType = GlobalConst.SupplierDocumentType.SupplierAudit }));

            viewModel.SupplierInsurances = Mapper.Map<IEnumerable<Insurance>>(_supplierService.GetSupplierInsuranceSupplierDocumentBySupplierID(supplierID.Value));
            viewModel.SupplierInsurances.ToList().ForEach(supplierInsurance => supplierInsurance.DocumentUrl = Url.Action(GlobalConst.Actions.SupplierSharedController.GetDocumentFile, GlobalConst.Controllers.SupplierShared, new { supplierID = supplierInsurance.SupplierID, fileName = supplierInsurance.UploadPath, supplierDocumentType = GlobalConst.SupplierDocumentType.Insurance }));

            viewModel.SupplierRegistrations = Mapper.Map<IEnumerable<Registration>>(_supplierService.GetSupplierRegistrationSupplierDocumentBySupplierID(supplierID.Value));
            viewModel.SupplierRegistrations.ToList().ForEach(supplierRegistration => supplierRegistration.DocumentUrl = Url.Action(GlobalConst.Actions.SupplierSharedController.GetDocumentFile, GlobalConst.Controllers.SupplierShared, new { supplierID = supplierRegistration.SupplierID, fileName = supplierRegistration.UploadPath, supplierDocumentType = GlobalConst.SupplierDocumentType.Registration }));

            viewModel.Users = Mapper.Map<IEnumerable<ITSUser>>(_userService.GetUserByUserTypeID((int)GlobalConst.UserType.Internal));

            viewModel.SupplierComplaints = Mapper.Map<IEnumerable<Complaint>>(_supplierService.GetSupplierComplaintAndStatusAndTypesBySupplierID(supplierID.Value));

            viewModel.TreatmentCategories = Mapper.Map<IEnumerable<TreatmentCategories>>(_referrerService.GetAllTreatmentCategory());

            viewModel.SupplierTreatmentCategoryPricing = Mapper.Map<IEnumerable<SupplierTreatmentCategoryPricingViewModel>>(_supplierService.GetSupplierTreatmentBySupplierID(supplierID.Value));
            foreach (var treatment in viewModel.SupplierTreatmentCategoryPricing)
            {
                var result = _supplierService.GetSupplierTreatmentPricingBySupplierTreatmentIDAndTreatmentCategoryID(treatment.SupplierTreatmentID, treatment.TreatmentCategoryID);
                result.ToList().ForEach(p => p.SupplierTreatmentID = treatment.SupplierTreatmentID);
                treatment.Pricing = Mapper.Map<IList<TreatmentPricing>>(result);
                treatment.TreatmentCategoryName = result.FirstOrDefault(pricing => pricing.TreatmentCategoryID == treatment.TreatmentCategoryID).TreatmentCategoryName;

            }

            return View(viewModel);
        }
    }
}
