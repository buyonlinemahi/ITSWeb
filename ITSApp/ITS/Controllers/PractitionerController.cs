using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PractitionerModel = ITS.Domain.Models.PractitionerModel;
using ITS.Domain.ViewModels;
using AutoMapper;
using System.Linq;
using ITS.Infrastructure.Global;
using PractitionerService =ITSWebApp.ITSService.PractitionerService;
using SupplierService = ITSWebApp.ITSService.SupplierService;
using ITS.Domain.Models.PractitionerModel;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.Base;



/*
 * Latest Version   : 1.0
 *
 * 1.0 - Created By - Robin Singh
 * Created Date: 05-March-2013
 * Description : Created Practitioner Controller 

 */

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class PractitionerController : BaseController
    {
        private readonly PractitionerService.IPractitionerService _practitionerService;
        private readonly SupplierService.ISupplierService _supplierService;
        //
        // GET: /Practitioner/

        public PractitionerController(PractitionerService.IPractitionerService practitionerService,
            SupplierService.ISupplierService supplierService)
        {
            _practitionerService = practitionerService;
            _supplierService = supplierService;
        }

        public ActionResult Index()
        {

            var viewModel = new PractitionerViewModel
                {
                    Practitioners =
                        Mapper.Map<IEnumerable<PractitionerModel.Practitioner>>(_practitionerService.GetPractitionersRecentlyAdded())
                };
            return View(viewModel);
        }

        public ActionResult Add()
        {
            var viewModel = new PractitionerViewModel
                {
                    AreasofExpertise =
                        Mapper.Map<IEnumerable<PractitionerModel.AreasofExpertise>>(_practitionerService.GetAllAreasofExpertise())
                };
            return View(viewModel);
        }

        public ActionResult Detail(int id)
        {
            var suppliers = Mapper.Map<IEnumerable<PractitionerModel.Supplier>>(_supplierService.GetAllSupplier());

            IEnumerable<PractitionerModel.Registration> registrations = Mapper.Map<IEnumerable<PractitionerModel.Registration>>(_practitionerService.GetPractitionerRegistrationsByPractitionerID(id));

            var practitioner = Mapper.Map<PractitionerModel.Practitioner>(_practitionerService.GetPractitionerByPractitionerID(id));
            var expertises = Mapper.Map<IEnumerable<PractitionerModel.Expertise>>(_practitionerService.GetPractitionerExpertiseByPractitionerID(id));
            practitioner.AreaofExpertiseIDs = expertises.Select(expertiseID => expertiseID.AreaofExpertiseID).ToList();

            var areasofExpertises = Mapper.Map<IEnumerable<PractitionerModel.AreasofExpertise>>(_practitionerService.GetAllAreasofExpertise());
            var registrationTypes = Mapper.Map<IEnumerable<PractitionerModel.RegistrationType>>(_supplierService.GetAllRegistrationType());
            var treatmentCategories = Mapper.Map<IEnumerable<PractitionerModel.TreatmentCategory>>(_practitionerService.GetAllTreatmentCatagories());
            var supplierPractitioners = Mapper.Map<IEnumerable<PractitionerModel.SupplierPractitioner>>(_practitionerService.GetSupplierPractitionerSupplierByPractitionerID(id));

            var viewModel = new PractitionerViewModel
                {
                    Practitioner = practitioner,
                    Registrations = registrations,
                    Suppliers = suppliers,
                    SupplierPractitioners = supplierPractitioners,
                    AreasofExpertise = areasofExpertises,
                    RegistrationTypes = registrationTypes,
                    TreatmentCategories = treatmentCategories
                };

            return View(viewModel);
        }



        [HttpPost]
        public ActionResult Save(PractitionerModel.Practitioner practitioner)
        {
            practitioner.PractitionerID = _practitionerService.AddPractitioner(Mapper.Map<PractitionerService.Practitioner>(practitioner));
            foreach (var expertise in practitioner.AreaofExpertiseIDs.Select(areaOfExpertiseID => new PractitionerModel.Expertise()
            {
                AreaofExpertiseID = areaOfExpertiseID,
                PractitionerID = practitioner.PractitionerID
            }))
            {
                _practitionerService.AddPractitionerExpertise(Mapper.Map<PractitionerService.PractitionerExpertise>(expertise));
            }

            return RedirectToAction(GlobalConst.Actions.PractitionerController.Detail, new { id = practitioner.PractitionerID });
        }
      
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(PractitionerSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
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

            return View(searchModel);
        }
    }
}
