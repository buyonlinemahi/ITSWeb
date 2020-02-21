using ITS.Domain.Models;


namespace ITS.Domain.ViewModels
{
    class ReferrerProjectCategoryViewModel
    {
        public ReferrerProjectTreatment Referrerprojecttreatment { get; set; }
        public ReferrerProjectTreatmentPricing ReferrerProjectTreatmentPricing { get; set; }
        public ProjectTreatmentSLA ProjectTreatmentSLA  { get; set; }
    }
}
