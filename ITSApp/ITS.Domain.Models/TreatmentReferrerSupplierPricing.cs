using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class TreatmentReferrerSupplierPricing
    {
        public int ReferrerPricingID { get; set; }
        public decimal ReferrerPrice { get; set; }
        public int SupplierPriceID { get; set; }
        public decimal SupplierPrice { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int PricingTypeID { get; set; }
        public string PricingTypeName { get; set; }
        public int SupplierTreatmentID { get; set; }
    }
}
