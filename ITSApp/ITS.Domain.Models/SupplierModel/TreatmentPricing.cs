
namespace ITS.Domain.Models.SupplierModel
{
    public class TreatmentPricing
    {
        public int PricingID { get; set; }
        public int PricingTypeID { get; set; }
        public double Price { get; set; }
        public int SupplierTreatmentID { get; set; }
        public string PricingTypeName { get; set; }
    }
}
