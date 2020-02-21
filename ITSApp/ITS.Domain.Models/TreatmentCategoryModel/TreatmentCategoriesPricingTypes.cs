
namespace ITS.Domain.Models.TreatmentCategoryModel
{
    public class TreatmentCategoriesPricingTypes
    {
        public int TreatmentCategoryPricingTypeID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int PricingTypeID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string PricingTypeName { get; set; }
    }
}
