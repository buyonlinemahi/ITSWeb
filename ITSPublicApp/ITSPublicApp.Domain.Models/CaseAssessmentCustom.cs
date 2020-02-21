
namespace ITSPublicApp.Domain.Models
{
    public class CaseAssessmentCustom
    {
        public int CaseAssessmentID { get; set; }
        public int CaseID { get; set; }
        public string Message { get; set; }
        public bool IsFurtherTreatment { get; set; }
        public bool isAccepted { get; set; }
    }
}
