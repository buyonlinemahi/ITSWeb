
namespace ITS.Domain.Models
{
    public class SupplierTreatmentPricing
    {
        public int PricingID { get; set; }
        public int PricingTypeID { get; set; }
        public double Price { get; set; }
        public int SupplierID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string PricingTypeName { get; set; }
        public int SupplierTreatmentID { get; set; }
        public bool IsEnabled { get; set; }
        public string Status { get; set; }
    }
}
