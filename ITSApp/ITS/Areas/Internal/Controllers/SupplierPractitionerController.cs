using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierPractitionerController : Controller
    {
        //
        // GET: /Internal/SupplierPractitioner/
        private readonly ITSService.PractitionerService.IPractitionerService _pratitionerService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;


        //
        // GET: /Internal/SupplierSetupComplaint/
        public SupplierPractitionerController(ITSService.PractitionerService.IPractitionerService pratitionerService, ITSService.SupplierService.ISupplierService supplierService, ITSService.ReferrerService.IReferrerService ReferrerService)
        {
            _pratitionerService = pratitionerService;
            _supplierService = supplierService;
            _referrerService = ReferrerService;
        }

        [HttpPost]
        public JsonResult PractitionerAutoCompleteByName(string searchKey)
        {
            IEnumerable<PractitionerTreatmentRegistration> practitioner = Mapper.Map<IEnumerable<PractitionerTreatmentRegistration>>(_supplierService.GetPractitionerLikePractitionerNameForSupplier(searchKey));


            return Json(practitioner);

        }
        [HttpPost]
        public JsonResult PractitionerAutoCompleteByTreatmentCategoryName(string searchKey)
        {
           // IEnumerable<PractitionerTreatmentRegistration> practitioner = Mapper.Map<IEnumerable<PractitionerTreatmentRegistration>>(_supplierService.GetPractitionerLikeTreatmentCategoryName(searchKey));
            return Json(null);
        }
        [HttpPost]
        public JsonResult GetAllTreatmentCategories()
        {
            IEnumerable<TreatmentCategories> treatmentCategories = Mapper.Map<IEnumerable<TreatmentCategories>>(_pratitionerService.GetAllTreatmentCatagories());
            return Json(treatmentCategories);
        }
        [HttpPost]

        //TODO 
        public JsonResult AddPratictioner(Practitioner pratitioner, IEnumerable<PractitionerExperties> practitionerAreaOFExperties, IEnumerable<PractitionerRegistration> Registrations)
        {
            int PractitionerID = _pratitionerService.AddPractitioner(Mapper.Map<ITSService.PractitionerService.Practitioner>(pratitioner));

            if (PractitionerID > 0)
            {
                foreach (PractitionerRegistration practoitionerRegistrations in Registrations)
                {
                    practoitionerRegistrations.PractitionerID = PractitionerID;
                    _pratitionerService.AddPractitionerRegistration(Mapper.Map<ITSService.PractitionerService.PractitionerRegistration>(practoitionerRegistrations));
                }

                foreach (PractitionerExperties practitionerExperise in practitionerAreaOFExperties)
                {
                    practitionerExperise.PractitionerID = PractitionerID;
                    _pratitionerService.AddPractitionerExpertise(Mapper.Map<ITSService.PractitionerService.PractitionerExpertise>(practitionerExperise));
                }

            }
            return Json(PractitionerID);
        }
        [HttpPost]
        public JsonResult GetRegistrationTypes(int treatmentCategoryID)
        {
            IEnumerable<TreatmentCategoriesRegistrationTypes> registrationTypes = Mapper.Map<IEnumerable<TreatmentCategoriesRegistrationTypes>>(_supplierService.GetTreatmentCategoriesRegistrationTypeByTreatmentCategoryID(treatmentCategoryID));
            return Json(registrationTypes);
        }


        public JsonResult GetAllAreaOfExperties()
        {
            IEnumerable<AreasofExpertise> areaOfExperties = Mapper.Map<IEnumerable<AreasofExpertise>>(_pratitionerService.GetAllAreasofExpertise());
            return Json(areaOfExperties);
        }

        [HttpPost]
        public JsonResult AddPractitionerAreaOfExperties(PractitionerExperties practitionerExerties)
        {
            int id = _pratitionerService.AddPractitionerExpertise(Mapper.Map<ITSService.PractitionerService.PractitionerExpertise>(practitionerExerties));
            return Json(id);
        }


        [HttpPost]
        public JsonResult GetPractitionerAreaOfExperties(int practitionerID)
        {
            IEnumerable<PractitionerExperties> practitionerAreaOfExperties = Mapper.Map<IEnumerable<PractitionerExperties>>(_pratitionerService.GetPractitionerExpertiseByPractitionerID(practitionerID));

            IEnumerable<AreasofExpertise> experties = Mapper.Map<IEnumerable<AreasofExpertise>>(_pratitionerService.GetAllAreasofExpertise());
            foreach (PractitionerExperties item in practitionerAreaOfExperties)
            {
                AreasofExpertise areaOfExperties = experties.Where(i => i.AreasofExpertiseID == item.AreaofExpertiseID).SingleOrDefault();
                item.AreaofExpertiseName = areaOfExperties.AreasofExpertiseName;
            }

            return Json(practitionerAreaOfExperties);
        }

        [HttpPost]
        public JsonResult AddSupplierPractitioner(SupplierPractitioner supplierPractitioner)
        {
            //    supplierPractitioner.PractitionerID = 0;
            int id = _supplierService.AddSupplierPractitionerRegistration(Mapper.Map<ITSService.SupplierService.SupplierPractitioner>(supplierPractitioner));
            return Json(supplierPractitioner);
        }

        [HttpPost]
        public JsonResult GetSupplierPractitionerTreatmentRegistrationsBySupplierID(int supplierID)
        {

            IEnumerable<SupplierPractitionerTreatmentRegistration> practitioners = Mapper.Map<IEnumerable<SupplierPractitionerTreatmentRegistration>>(_supplierService.GetSupplierPractitionerTreatmentRegistrationsBySupplierID(supplierID));
            return Json(practitioners);
        }
        [HttpPost]
        public JsonResult DeleteSupplierPractitionerBySupplierPractitionerID(int supplierPractitionerID)
        {
            int id = _supplierService.DeleteSupplierPractitionerBySupplierPractitionerID(supplierPractitionerID);
            return Json(id);
        }

    }
}
