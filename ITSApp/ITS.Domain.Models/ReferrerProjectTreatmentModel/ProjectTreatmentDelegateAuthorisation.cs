
namespace ITS.Domain.Models.ReferrerProjectTreatmentModel
{
    public class ProjectTreatmentDelegateAuthorisation
    {
        public int ReferrerProjectTreatmentAuthorisationID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int DelegatedAuthorisationTypeID { get; set; }
        public decimal? Amount { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int? Quantity { get; set; }
        public bool? Enabled { get; set; }
        public string Name { get; set; }
    }
}
