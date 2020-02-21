
namespace ITS.Domain.Models.ReferrerProjectTreatmentModel
{
   public class ProjectTreatmentDocumentSetup
    {
        public int ReferrerProjectTreatmentDocumentSetupID { get; set; }
        public int AssessmentServiceID { get; set; }
        public int DocumentSetupTypeID { get; set; }
        public string AssessmentServiceName { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public string DocumentSetupTypeName { get; set; }
        public string UploadedFileName { get; set; }
        public string UploadedFilePath { get; set; }
    }
}
