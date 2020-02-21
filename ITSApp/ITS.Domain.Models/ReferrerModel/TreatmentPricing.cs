
namespace ITS.Domain.Models.ReferrerModel
{
    public class TreatmentPricing
    {
        public int PricingID { get; set; }
        public int PricingTypeID { get; set; }
        public decimal? Price { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public string PricingTypeName { get; set; }
    }
}
