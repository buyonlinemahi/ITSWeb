
using System.Web;
namespace ITS.Domain.Models
{
   public class ReferrerProjectTreatmentDocumentSetUp
    {
        public int ReferrerProjectTreatmentDocumentSetupID { get; set; }
        public int AssessmentServiceID { get; set; }
        public int DocumentSetupTypeID { get; set; }
        public string AssessmentServiceName { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public HttpPostedFileBase AssesmentFile { get; set; }
        public string UploadedFileName { get; set; }
        public string UploadedFilePath { get; set; }

    }
}
