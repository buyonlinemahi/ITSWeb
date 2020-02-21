
namespace ITS.Domain.Models.ReferrerProjectTreatmentModel
{
    public class ProjectTreatmentSLAName
    {

        public int ProjectTreatmentSLAID { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int ServiceLevelAgreementID { get; set; }
        public int NumberOfDays { get; set; }
        public bool Enabled { get; set; }
        public string  ServiceLevelAgreementName { get; set; }
    }
}
