
namespace ITS.Domain.Models
{
    public class ReferrerProjectTreatmentPricing
    {
        public int PricingID { get; set; }
        public int PricingTypeID { get; set; }
        public double Price { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public string PricingTypeName { get; set; }
        public int ReferrerProjectID { get; set; }
        public bool IsTriage { get; set; }
    }
}
