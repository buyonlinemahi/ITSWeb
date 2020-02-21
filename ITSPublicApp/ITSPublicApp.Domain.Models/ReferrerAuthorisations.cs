using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ITSPublicApp.Domain.Models
{
    public class ReferrerAuthorisations
    {
        public DateTime CaseReferrerDueDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string CaseNumber { get; set; }
        public string TreatmentTypeName { get; set; }
        public int WorkflowID { get; set; }
        public int ReferrerID { get; set; }
        public int PatientID { get; set; }
        public int SupplierID { get; set; }
        public int CaseID { get; set; }
        public bool IsCustom { get; set; }
        public bool IsFinalVersionUpload { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int AssessmentServiceID { get; set; }
        public string UrlPath { get; set; }
        public string EncryptedCaseID { get; set; }
    }
}
