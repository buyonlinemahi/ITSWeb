using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Domain.Models.ReferrerModel;
using AutoMapper;
using System.Configuration;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.Global;
using ITS.Domain.Models.TreatmentCategoryModel;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferrerSharedController : BaseController
    {

        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IEncryption _iEncryptionService;

        public ReferrerSharedController(ITSService.ReferrerService.IReferrerService referrerService, ITS.Infrastructure.ApplicationServices.ReferrerStorageService referrerStorage,
            ITS.Infrastructure.ApplicationServices.Contracts.IEncryption iEncryptionService)
        {
            _referrerService = referrerService;
            _iEncryptionService = iEncryptionService;
        }

        [HttpPost]
        public JsonResult GetAllReferrer()
        {
            IEnumerable<Referrer> referrer = Mapper.Map<IEnumerable<Referrer>>(_referrerService.GetAllReferrer());
            return Json(referrer);
        }

        [HttpPost]
        public JsonResult ReferrerAutoComplete(string searchKey)
        {
            IEnumerable<Referrer> referrer = Mapper.Map<IEnumerable<Referrer>>(_referrerService.GetReferrersLikeReferrerName(searchKey));
            return Json(referrer);
        }

        [HttpPost]
        public JsonResult GetReferrer(int referrerID)
        {
            Referrer referrer = Mapper.Map<Referrer>(_referrerService.GetReferrerDetailsByReferrerID(referrerID));
            return Json(referrer);
        }

        [HttpPost]
        public JsonResult UpdateReferrer(Referrer referrer, string objIsPolicyDetail, string objIsEmploymentDetail, string objIsEmployeeDetail, string objIsDrugandAlcoholTest, string objIsRepresentation, string objIsAdditionalQuestion, string objIsJobDemand, string objIsPolicyDetailOpenOrDropdowns)
        {

            if (objIsPolicyDetail != null)
            {
                if (objIsPolicyDetail == "1")
                    referrer.IsPolicyDetail = true;
                else
                    referrer.IsPolicyDetail = false;
            }
            else
            {
                referrer.IsPolicyDetail = null;
            }
            if (objIsEmploymentDetail == "1")
                referrer.IsEmploymentDetail = true;
            else
                referrer.IsEmploymentDetail = false;
            if (objIsEmployeeDetail == "1")
                referrer.IsEmploeeDetail = true;
            else
                referrer.IsEmploeeDetail = false;
            if (objIsDrugandAlcoholTest == "1")
                referrer.IsDrugandAlcoholTest = true;
            else
                referrer.IsDrugandAlcoholTest = false;
            if (objIsRepresentation == "1")
                referrer.IsRepresentation = true;
            else
                referrer.IsRepresentation = false;
            if (objIsAdditionalQuestion == "1")
                referrer.IsAdditionalQuestion = true;
            else
                referrer.IsAdditionalQuestion = false;
            if (objIsJobDemand == "1")
                referrer.IsJobDemand = true;
            else
                referrer.IsJobDemand = false;
            referrer.IsPolicyDetailOpenOrDropdowns = objIsPolicyDetailOpenOrDropdowns == null ? "" : objIsPolicyDetailOpenOrDropdowns;
            referrer.IsNewPolicyTypes = referrer.IsNewPolicyTypes == null ? "" : referrer.IsNewPolicyTypes;
            referrer.ReferrerID = Convert.ToInt32(DecryptString(referrer.EncryptedReferrerID));
            _referrerService.UpdateReferrer(Mapper.Map<ITSService.ReferrerService.Referrer>(referrer));
            return Json(referrer, "text/html");
        }

        //[HttpPost]
        //public JsonResult GetReferrerMainOffice(int referrerID)
        //{
        //    ReferrerLocation referrerLocation = Mapper.Map<ReferrerLocation>(_referrerService.GetReferrerMainLocation(referrerID));
        //    return Json(referrerLocation);
        //}

        //[HttpPost]
        //public JsonResult GetReferrerLocationsByReferrerID(int referrerID)
        //{
        //    IEnumerable<ReferrerLocation> referrerLocation = Mapper.Map<IEnumerable<ReferrerLocation>>(_referrerService.GetReferrerLocationsByReferrerID(referrerID));
        //    return Json(referrerLocation);
        //}


        [HttpPost]
        public JsonResult DeleteByReferrerLocationID(int ReferrerLocationID)
        {
            _referrerService.DeleteByReferrerLocationID(ReferrerLocationID);
            return Json(ReferrerLocationID);
        }

        [HttpPost]
        public JsonResult AddReferrerLocation(Location referrerLocation)
        {
            referrerLocation.ReferrerLocationID = _referrerService.InsertReferrerLocations(Mapper.Map<ITSService.ReferrerService.ReferrerLocation>(referrerLocation));
            return Json(referrerLocation, "text/html");
        }

        [HttpPost]
        public JsonResult UpdateReferrerLocation(Location referrerLocation)
        {
            _referrerService.UpdateReferrerLocations(Mapper.Map<ITSService.ReferrerService.ReferrerLocation>(referrerLocation));
            return Json(referrerLocation, "text/html");
        }

        [HttpPost]
        public ActionResult ReferrerSearch(ReferrerSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
        {
            switch (searchModel.ReferrerSearch.SearchCriteria)
            {
                case (int)GlobalConst.ReferrerSearchCriteria.ReferrerName:
                    var byNameResults = _referrerService.GetReferrerLocationReferrerLikeReferrerName(searchModel.ReferrerSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerModel.ReferrerSearchResult>>
                        (byNameResults.ReferrerLocationReferrers);
                    searchModel.TotalCount = byNameResults.ReferrerLocationReferrerCount;
                    break;

            }
            foreach (var objResult in searchModel.Referrers)
            {
                objResult.EncryptedReferrerID =EncryptString(objResult.ReferrerID.ToString());
            }
            return Json(searchModel, "text/html");
        }

        //marked for delete
        [HttpPost]
        public JsonResult GetPricingTypesByTreatmentCategoryID(int treatmentCategoryID, int referrerProjectTreatmentID)
        {
            IEnumerable<ITS.Domain.ViewModels.ReferrerPricingValue> referrerPricingValue =
                Mapper.Map<IEnumerable<ITS.Domain.ViewModels.ReferrerPricingValue>>(
                _referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentIDAndTreatmentCategoryID(referrerProjectTreatmentID, treatmentCategoryID));

            return Json(referrerPricingValue);
        }

        [HttpPost]
        public JsonResult GetTreatmentCategoriesPricingTypesByTreatmentCategoryID(int treatmentCategoryID)
        {
            return Json(Mapper.Map<IEnumerable<TreatmentCategoriesPricingTypes>>(_referrerService.GetPricingTypesByTreatmentCategoryID(treatmentCategoryID)));
        }

        [HttpPost]
        public JsonResult AddProjectTreatmentCategory(ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryPricingViewModel treatment)
        {
            int referrerProjectTreatmentID = _referrerService.AddReferrerProjectTreatment(
                Mapper.Map<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatment>(treatment));

            if (referrerProjectTreatmentID == -1)
                return Json(-1, "text/html");

            treatment.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
            
            //add
            foreach (var treatmentPricing in treatment.Pricing)
            {
                treatmentPricing.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
                treatmentPricing.PricingID = _referrerService.AddReferrerProjectTreatmentPricing(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
            }

            //return all possible pricing
            treatment.Pricing = Mapper.Map<IList<ITS.Domain.Models.ReferrerModel.TreatmentPricing>>
                        (_referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentIDAndTreatmentCategoryID(treatment.ReferrerProjectTreatmentID, treatment.TreatmentCategoryID));

            return Json(treatment, "text/html");
            
        }

        [HttpPost]
        public JsonResult UpdateTreatmentCategoryPricing(IEnumerable<ITS.Domain.Models.ReferrerModel.TreatmentPricing> pricing)
        {

            foreach(ITS.Domain.Models.ReferrerModel.TreatmentPricing treatmentPricing in pricing)
            {
                if(treatmentPricing.PricingID == 0)
                    treatmentPricing.PricingID = _referrerService.AddReferrerProjectTreatmentPricing(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
                else
                _referrerService.UpdateReferrerProjectTreatmentPricingByPricingID(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
            }

            return Json(pricing, "text/html");
        }
    }
}
