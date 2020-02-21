using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;
/*
 * Latest Version 1.0
 * 
 * Updated By   : Harpreet Singh
 * Date         : 26-Dec-2012
 * Purpose      : Created Pricing Tab Methods.
 * Version      : 1.0
 * */
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierSetupController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;

        public SupplierSetupController(ITSService.ReferrerService.IReferrerService referrerService, ITSService.SupplierService.ISupplierService supplierService)
        {
            _referrerService = referrerService;
            _supplierService = supplierService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]

        public JsonResult GetPricingTypesBySupplierTreatmentCategoryID(IEnumerable<SupplierTreatment> treatments)
        {
            var SupplierTreatmentsPricings = new List<SupplierTreatmentPricing>();
            if (treatments != null)
            {
                foreach (SupplierTreatment item in treatments)
                {
                    var supplierPricings = new List<SupplierTreatmentPricing>();

                    IEnumerable<TreatmentCategoriesPricingTypes> treatmentPricingTypes = Mapper.Map<IEnumerable<TreatmentCategoriesPricingTypes>>(_referrerService.GetPricingTypesByTreatmentCategoryID(item.TreatmentCategoryID));

                    supplierPricings = Mapper.Map<List<SupplierTreatmentPricing>>(_supplierService.GetSupplierTreatmentPricingBySupplierTreatmentId(item.SupplierTreatmentID));

                    IEnumerable<SupplierTreatment> supplierTreatments = Mapper.Map<IEnumerable<SupplierTreatment>>(_supplierService.GetSupplierTreatmentBySupplierID(item.SupplierID));

                    foreach (TreatmentCategoriesPricingTypes treatmentPricingType in treatmentPricingTypes)
                    {
                        SupplierTreatment supplierTreatment = supplierTreatments.Single(pricing => pricing.TreatmentCategoryID == treatmentPricingType.TreatmentCategoryID);

                        supplierPricings.SingleOrDefault(pricing => pricing.PricingTypeID == treatmentPricingType.PricingTypeID).IsEnabled = supplierTreatment.Enabled;

                        supplierPricings.SingleOrDefault(pricing => pricing.PricingTypeID == treatmentPricingType.PricingTypeID).TreatmentCategoryID = treatmentPricingType.TreatmentCategoryID;

                        supplierPricings.SingleOrDefault(pricing => pricing.PricingTypeID == treatmentPricingType.PricingTypeID).PricingTypeName = treatmentPricingType.PricingTypeName;
                    }

                    SupplierTreatmentsPricings.AddRange(supplierPricings);

                }
            }

            return Json(SupplierTreatmentsPricings);
        }

        [HttpPost]
        public JsonResult GetAllPricingTypes(int count)
        {
            List<SupplierTreatmentPricing> SupplierTreatmentsPricings = new List<SupplierTreatmentPricing>();
            for (int treatmentCategory = 1; treatmentCategory <= count; treatmentCategory++)
            {
                List<SupplierTreatmentPricing> pricings = new List<SupplierTreatmentPricing>();
                IEnumerable<TreatmentCategoriesPricingTypes> pricingTypes = Mapper.Map<IEnumerable<TreatmentCategoriesPricingTypes>>(_referrerService.GetPricingTypesByTreatmentCategoryID(treatmentCategory));
                foreach (TreatmentCategoriesPricingTypes pricingType in pricingTypes)
                    pricings.Add(new SupplierTreatmentPricing { PricingID = 0, PricingTypeID = pricingType.PricingTypeID, PricingTypeName = pricingType.PricingTypeName, SupplierTreatmentID = 0, TreatmentCategoryID = treatmentCategory, IsEnabled = false });

                SupplierTreatmentsPricings.AddRange(pricings);
            }

            return Json(SupplierTreatmentsPricings);
        }


        [HttpPost]
        public JsonResult AddSupplierPricing(IEnumerable<SupplierTreatmentPricing> supplierTreatmentPricings)
        {
            if (supplierTreatmentPricings != null)
            {
                foreach (SupplierTreatmentPricing suppliertreatmentPricing in supplierTreatmentPricings)
                {
                    if (suppliertreatmentPricing.IsEnabled == false)
                        suppliertreatmentPricing.Price = 0;

                    _supplierService.AddSupplierTreatmentPricing(Mapper.Map<ITSService.SupplierService.SupplierTreatmentPricing>(suppliertreatmentPricing));
                }
            }
            return Json(supplierTreatmentPricings.GroupBy(supliertreatmentid => supliertreatmentid.SupplierTreatmentID).Select(pricing => pricing.First()).ToList());
        }
        [HttpPost]
        public JsonResult UpdateSupplierTreatmentPricing(IEnumerable<SupplierTreatmentPricing> supplierTreatmentPricing)
        {

            foreach (SupplierTreatmentPricing supllierTreatmentPricing in supplierTreatmentPricing)
            {
                if (supllierTreatmentPricing.IsEnabled == false)
                    supllierTreatmentPricing.Price = 0;
                _supplierService.UpdateSupplierTreatmentPricingByPricingID(Mapper.Map<ITSService.SupplierService.SupplierTreatmentPricing>(supllierTreatmentPricing));

            }

            return Json(supplierTreatmentPricing.GroupBy(supliertreatmentid => supliertreatmentid.SupplierTreatmentID).Select(pricing => pricing.First()).ToList());
        }

        [HttpPost]
        public JsonResult GetAllTreatmentCategories()
        {
            IEnumerable<TreatmentCategories> treatmentcategories = Mapper.Map<IEnumerable<TreatmentCategories>>(_referrerService.GetAllTreatmentCategory());
            return Json(treatmentcategories);
        }
    }
}
