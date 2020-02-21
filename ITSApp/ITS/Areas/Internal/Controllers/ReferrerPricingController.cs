using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;

/*
================================================
Latest  : Version 1.0
Author  : Harpreet Singh
Created On : 29-Jan-2013
Purpous :created saprate controller for pricing  tab and added the methods from referrersetupcontroller to pricing controller  
Version : 1.0
================================================
*/
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferrerPricingController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public ReferrerPricingController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }
        
        #region Pricing Types

        [HttpPost]
        public JsonResult GetPricingTypesByTreatmentCategoryID(int treatmentCategoryID, int referrerProjectTreatmentID)
        {
            IEnumerable<TreatmentCategoriesPricingTypes> pricingTypes = Mapper.Map<IEnumerable<TreatmentCategoriesPricingTypes>>(_referrerService.GetPricingTypesByTreatmentCategoryID(treatmentCategoryID));
            //get values for the pricetypes as well.
            List<ReferrerProjectTreatmentPricing> pricings = new List<ReferrerProjectTreatmentPricing>();
            pricings = Mapper.Map<List<ReferrerProjectTreatmentPricing>>(_referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentID(referrerProjectTreatmentID));
            if (pricings.Count() == 0)
            {
                foreach (TreatmentCategoriesPricingTypes pricingType in pricingTypes)
                    pricings.Add(new ReferrerProjectTreatmentPricing { Price = 0, PricingID = 0, PricingTypeID = pricingType.PricingTypeID, PricingTypeName = pricingType.PricingTypeName, ReferrerProjectTreatmentID = referrerProjectTreatmentID });
            }
            else
            {
                foreach (TreatmentCategoriesPricingTypes pricingType in pricingTypes)
                    pricings.Single(pricing => pricing.PricingTypeID == pricingType.PricingTypeID).PricingTypeName = pricingType.PricingTypeName;
            }
            return Json(pricings);
        }

        [HttpPost]
        public JsonResult GetProjectTreatmentSLAsByReferrerProjectTreatmentID(int referrerProjectTreatmentID)
        {
            IEnumerable<ProjectTreatmentSLA> projectTreatmentSLAs = Mapper.Map<IEnumerable<ProjectTreatmentSLA>>(_referrerService.GetProjectTreatmentSLAsByReferrerProjectTreatmentID(referrerProjectTreatmentID));
            IEnumerable<ServiceLevelAgreement> serviceLevelAgreements = Mapper.Map<IEnumerable<ServiceLevelAgreement>>(_referrerService.GetAllServiceLevelAgreement());

            foreach (ProjectTreatmentSLA projectTreatmentSLA in projectTreatmentSLAs)
            {
                projectTreatmentSLA.ServiceLevelAgreementName = serviceLevelAgreements.Single<ServiceLevelAgreement>(serviceLvelAgreement => serviceLvelAgreement.ServiceLevelAgreementID == projectTreatmentSLA.ServiceLevelAgreementID).ServiceLevelAgreementName;
            }
            return Json(projectTreatmentSLAs);
        }

        [HttpPost]
        public JsonResult GetReferrerProjectTreatmentByReferrerProjectTreatmentID(int referrerProjectID, int treatmentCategoryID)
        {
            ReferrerProjectTreatment projectTreatment = Mapper.Map<ReferrerProjectTreatment>(_referrerService.GetReferrerProjectTreatmentsByReferrerProjectID(referrerProjectID).SingleOrDefault(treatmentcategory => treatmentcategory.TreatmentCategoryID == treatmentCategoryID));


            return Json(projectTreatment);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatment(ReferrerProjectTreatment projectTreatment)
        {
            _referrerService.UpdateReferrerProjectTreatment(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatment>(projectTreatment));
            return Json(GlobalResource.UpdatedSuccessfully);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatmentPricingByPricingID(IEnumerable<ReferrerProjectTreatmentPricing> referrerProjectTreatmentPricings)
        {
            int result = 0;

            foreach (ReferrerProjectTreatmentPricing treatmentPricing in referrerProjectTreatmentPricings)
            {
                if (treatmentPricing.PricingID == 0)
                    result = _referrerService.AddReferrerProjectTreatmentPricing(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
                else
                    result = _referrerService.UpdateReferrerProjectTreatmentPricingByPricingID(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
            }
            if (result > 0)
            {
                var isTriage = referrerProjectTreatmentPricings.FirstOrDefault().IsTriage;
                _referrerService.UpdateProjectStatus(referrerProjectTreatmentPricings.FirstOrDefault().ReferrerProjectID, isTriage);
            }
            return Json(GlobalResource.UpdatedSuccessfully);
        }

        [HttpPost]
        public JsonResult UpdateProjectTreatmentSLA(IEnumerable<ProjectTreatmentSLA> projectTreatmentSLAs)
        {
            foreach (ProjectTreatmentSLA treatmentSLA in projectTreatmentSLAs)
            {
                _referrerService.UpdateProjectTreatmentSLAsByProjectTreatmentSLAID(Mapper.Map<ITSService.ReferrerService.ProjectTreatmentSLA>(treatmentSLA));
            }
            return Json(GlobalResource.UpdatedSuccessfully);
        }
        #endregion

    }
}
