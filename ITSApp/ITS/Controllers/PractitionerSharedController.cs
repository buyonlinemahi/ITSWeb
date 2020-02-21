using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PractitionerModel = ITS.Domain.Models.PractitionerModel;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using PractitionerService = ITSWebApp.ITSService.PractitionerService;
using SupplierService = ITSWebApp.ITSService.SupplierService;
using ITS.Domain.Models.PractitionerModel;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class PractitionerSharedController : BaseController
    {
        private readonly PractitionerService.IPractitionerService _practitionerService;
        private readonly SupplierService.ISupplierService _supplierService;

        public PractitionerSharedController(PractitionerService.IPractitionerService practitionerService, SupplierService.ISupplierService supplierService)
        {
            _practitionerService = practitionerService;
            _supplierService = supplierService;
        }

        [HttpPost]
        public JsonResult UpdatePractitioner(PractitionerModel.Practitioner practitioner)
        {
            _practitionerService.UpdatePractitionerByPractitionerID(Mapper.Map<ITSService.PractitionerService.Practitioner>(practitioner));

            if (practitioner.AreaofExpertiseIDs != null)
            {
                var expertises = practitioner.AreaofExpertiseIDs.Select(areaOfExpertiseID => new PractitionerModel.Expertise()
                    {
                        AreaofExpertiseID = areaOfExpertiseID,
                        PractitionerID = practitioner.PractitionerID
                    }).ToList();
                _practitionerService.UpdatePractitionerExpertise(Mapper.Map<IEnumerable<PractitionerService.PractitionerExpertise>>(expertises).ToArray());
            }
            return Json(practitioner);
        }

        [HttpPost]
        public JsonResult DeletePractitioner(int practitionerID)
        {
            _practitionerService.DeletePractitionerByPractitionerID(practitionerID);
            return Json(practitionerID);
        }

        [HttpPost]
        public ActionResult AddRegistration(PractitionerModel.Registration registration)
        {
            registration.PractitionerRegistrationID = _practitionerService.AddPractitionerRegistration(Mapper.Map<PractitionerService.PractitionerRegistration>(registration));
            if (registration.PractitionerRegistrationID == -1)
                return Json(-1, "text/html");
            return Json(registration,"text/html");
        }

        [HttpPost]
        public ActionResult UpdateRegistration(PractitionerModel.Registration registration)
        {
            int PractitionerRegistrationID = _practitionerService.UpdatePractitionerRegistrationByPractitionerRegistrationID(Mapper.Map<PractitionerService.PractitionerRegistration>(registration));
            if (PractitionerRegistrationID == -1)
                return Json(-1, "text/html");
            return Json(registration,"text/html");
        }

        [HttpPost]
        public ActionResult DeleteRegistration(int practitionerRegistrationID)
        {
            _practitionerService.DeletePractitionerRegistrationByPractitionerRegistrationID(practitionerRegistrationID);
            return Json(GlobalResource.DeletedSuccessfully);
        }

        [HttpPost]
        public ActionResult AddSupplierPractitioner(PractitionerModel.SupplierPractitioner supplierPractitioner)
        {
            int SupplierPractitionerID = _supplierService.AddSupplierPractitionerRegistration(Mapper.Map<SupplierService.SupplierPractitioner>(supplierPractitioner));
            if (SupplierPractitionerID == -1)
                return Json(-1, "text/html");
            return Json(supplierPractitioner,"text/html");
        }

        [HttpPost]
        public ActionResult UpdateSupplierPractitioner(PractitionerModel.SupplierPractitioner supplierPractitioner)
        {
            int SupplierPractitionerID = _supplierService.UpdateSupplierPractitioner(Mapper.Map<SupplierService.SupplierPractitioner>(supplierPractitioner));
            if (SupplierPractitionerID == -1)
                return Json(-1, "text/html");
            return Json(supplierPractitioner, "text/html");
        }

        [HttpPost]
        public JsonResult DeleteSupplierPractitioner(int supplierPractitionerID)
        {
            _supplierService.DeleteSupplierPractitionerBySupplierPractitionerID(supplierPractitionerID);
            return Json(GlobalResource.DeletedSuccessfully);
        }

        //TODO Need to update Method with Skip Take

        [HttpPost]
        public ActionResult PractitionerSearch(ITS.Domain.ViewModels.PractitionerSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
        {

            switch (searchModel.PractitionerSearch.SearchCriteria)
            {
                case (int)GlobalConst.PractitionerSearchCriteria.PractitionerName:
                    var byNameResults =
                        _practitionerService.GetPractitionersLikePractitionerName(
                            searchModel.PractitionerSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Practitioners = Mapper.Map<IEnumerable<PractitionerSearchResult>>(byNameResults.PractitionerTreatmentRegistrations);
                    searchModel.TotalCount = byNameResults.SearchTotalCount;
                    break;

                case (int)GlobalConst.PractitionerSearchCriteria.TreatmentCategory:
                    var byTreatmentResults = _practitionerService.GetPractitionersLikeTreatmentCategoryName(searchModel.PractitionerSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Practitioners = Mapper.Map<IEnumerable<PractitionerSearchResult>>(byTreatmentResults.PractitionerTreatmentRegistrations);
                    searchModel.TotalCount = byTreatmentResults.SearchTotalCount;
                    break;
            }

            return Json(searchModel,"text/html");
        }

        [HttpPost]
        public JsonResult GetRegistrationTypes(int treatmentCategoryID)
        {
            IEnumerable<ITS.Domain.Models.TreatmentCategoriesRegistrationTypes> registrationTypes = 
                Mapper.Map<IEnumerable<ITS.Domain.Models.TreatmentCategoriesRegistrationTypes>>(_supplierService.GetTreatmentCategoriesRegistrationTypeByTreatmentCategoryID(treatmentCategoryID));
            return Json(registrationTypes);
        }
    }
}
