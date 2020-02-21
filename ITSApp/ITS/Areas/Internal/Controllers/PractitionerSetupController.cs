using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Global;

/*
 * Latest Version 1.0
 * 1.0 – 07-March-2013  Robin Singh
 * Purpose: Created Practitioner Setup Controller and Their Methods.
 * */

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class PractitionerSetupController : Controller
    {
        //
        // GET: /Internal/PractitionerSetup/
        private readonly ITSService.PractitionerService.IPractitionerService _pratitionerService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public ActionResult Index()
        {
            return View();
        }

        public PractitionerSetupController(ITSService.PractitionerService.IPractitionerService pratitionerService, ITSService.SupplierService.ISupplierService supplierService, ITSService.ReferrerService.IReferrerService referrerService)
        {
            _pratitionerService = pratitionerService;
            _supplierService = supplierService;
            _referrerService = referrerService;
        }

        [HttpPost]
        public JsonResult PractitionerAutoCompleteByName(string searchKey)
        {
            IEnumerable<Practitioner> practitioner = Mapper.Map<IEnumerable<Practitioner>>(_pratitionerService.GetPractitionerLikePractitionerName(searchKey));
            return Json(practitioner);
        }

        [HttpPost]
        public JsonResult AddPratictioner(Practitioner pratitioner, IEnumerable<PractitionerRegistration> practitionerRegistrations, IEnumerable<PractitionerExperties> selectedPractitionetrExperties)
        {
            int PractitionerID = _pratitionerService.AddPractitioner(Mapper.Map<ITSService.PractitionerService.Practitioner>(pratitioner));
            if (PractitionerID > 0)
            {
                foreach (PractitionerRegistration practitionerRegistration in practitionerRegistrations)
                {
                    practitionerRegistration.PractitionerID = PractitionerID;
                    _pratitionerService.AddPractitionerRegistration(Mapper.Map<ITSService.PractitionerService.PractitionerRegistration>(practitionerRegistration));
                }
                foreach (PractitionerExperties practitionerExperise in selectedPractitionetrExperties)
                {
                    practitionerExperise.PractitionerID = PractitionerID;
                    _pratitionerService.AddPractitionerExpertise(Mapper.Map<ITSService.PractitionerService.PractitionerExpertise>(practitionerExperise));
                }
            }
            return Json(PractitionerID);
        }

        [HttpPost]
        public JsonResult UpdatePratictioner(Practitioner pratitioner, IEnumerable<PractitionerRegistration> practitionerRegistrations, List<PractitionerExperties> selectedPractitionetrExperties)
        {
            int result = _pratitionerService.UpdatePractitionerByPractitionerID(Mapper.Map<ITSService.PractitionerService.Practitioner>(pratitioner));

            IEnumerable<PractitionerExperties> practitionerAreaOfExperties = Mapper.Map<IEnumerable<PractitionerExperties>>(_pratitionerService.GetPractitionerExpertiseByPractitionerID(pratitioner.PractitionerID));

            foreach (PractitionerRegistration practitionerRegistration in practitionerRegistrations)
            {
                _pratitionerService.UpdatePractitionerRegistrationByPractitionerRegistrationID(Mapper.Map<ITSService.PractitionerService.PractitionerRegistration>(practitionerRegistration));
            }
            _pratitionerService.UpdatePractitionerExpertise(Mapper.Map<List<ITSService.PractitionerService.PractitionerExpertise>>(selectedPractitionetrExperties).ToArray());
            //_pratitionerService.UpdatePractitionerExpertiseByPractitionerExpertiseID(Mapper.Map<List<ITSService.PractitionerService.PractitionerExpertise>>(selectedPractitionetrExperties).ToArray());
             
              return Json(GlobalResource.UpdatedSuccessfully);
        }

        [HttpPost]
        public JsonResult GetAllTreatmentCategories()
        {
            IEnumerable<TreatmentCategories> treatmentCategories = Mapper.Map<IEnumerable<TreatmentCategories>>(_pratitionerService.GetAllTreatmentCatagories());
            return Json(treatmentCategories);
        }

        public JsonResult GetAreaOfExperties()
        {
            IEnumerable<AreasofExpertise> areaOfExperties = Mapper.Map<IEnumerable<AreasofExpertise>>(_pratitionerService.GetAllAreasofExpertise());
            return Json(areaOfExperties);
        }
        [HttpPost]
        public JsonResult GetPractitionerByPractitionerId(int practitionerID)
        {
            Practitioner practitioner = Mapper.Map<Practitioner>(_pratitionerService.GetPractitionerByPractitionerID(practitionerID));
            return Json(practitioner);
        }

        [HttpPost]
        public JsonResult GetRegistrationTypes(int treatmentCategoryID)
        {
            IEnumerable<TreatmentCategoriesRegistrationTypes> registrationTypes = Mapper.Map<IEnumerable<TreatmentCategoriesRegistrationTypes>>(_supplierService.GetTreatmentCategoriesRegistrationTypeByTreatmentCategoryID(treatmentCategoryID));
            return Json(registrationTypes);
        }

        [HttpPost]
        public JsonResult GetPractitionerAreaOfExperties(int practitionerID)
        {
            IEnumerable<PractitionerExperties> practitionerAreaOfExperties = Mapper.Map<IEnumerable<PractitionerExperties>>(_pratitionerService.GetPractitionerExpertiseByPractitionerID(practitionerID));
            return Json(practitionerAreaOfExperties);
        }

        [HttpPost]
        public JsonResult GetPractitionerRegistrationByPractitionerId(int practitionerID)
        {
            IEnumerable<PractitionerRegistration> practitionerRegistrations = Mapper.Map<IEnumerable<PractitionerRegistration>>(_pratitionerService.GetPractitionerRegistrationsByPractitionerID(practitionerID));
            IEnumerable<TreatmentCategories> treatmentCategories = Mapper.Map<IEnumerable<TreatmentCategories>>(_referrerService.GetAllTreatmentCategory());
            IEnumerable<RegistrationTypes> registrationTypes = Mapper.Map<IEnumerable<RegistrationTypes>>(_supplierService.GetAllRegistrationType());

            foreach (PractitionerRegistration practitionerRegistration in practitionerRegistrations)
            {
                TreatmentCategories category = treatmentCategories.Where(i => i.TreatmentCategoryID == practitionerRegistration.TreatmentCategoryID).SingleOrDefault();
                practitionerRegistration.TreatmentCategoryName = category.TreatmentCategoryName;

                RegistrationTypes registrationType = registrationTypes.Where(i => i.RegistrationTypeID == practitionerRegistration.RegistrationTypeID).SingleOrDefault();
                practitionerRegistration.RegistrationTypeName = registrationType.RegistrationTypeName;
            }

            return Json(practitionerRegistrations);
        }

    }
}
