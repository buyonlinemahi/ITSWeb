
namespace ITS.Domain.Models
{
    public class ReferrerProjectTreatmentAuthorisation
    {
        public int ReferrerProjectTreatmentAuthorisationID { get; set; }
        //public int DelegatedAuthorisationID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int DelegatedAuthorisationTypeID { get; set; }
        public double Amount { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
    }
}
