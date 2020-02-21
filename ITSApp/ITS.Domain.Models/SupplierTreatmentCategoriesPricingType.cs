
namespace ITS.Domain.Models
{
    public class SupplierTreatmentCategoriesPricingType
    {
        public int TreatmentCategoryPricingTypeID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int PricingTypeID { get; set; }
        //public double Price { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string PricingTypeName { get; set; }
       
    }
}
