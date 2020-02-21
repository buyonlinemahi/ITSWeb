using System;

namespace ITS.Domain.Models
{
    public class CaseWorkflowPatientReferrerProject
    {
        public int CaseID { get; set; }
        public int PatientID { get; set; }
        public int ReferrerID { get; set; }
        public string CaseNumber { get; set; }
        public DateTime CaseDateOfInquiry { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int TreatmentTypeID { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string CaseSpecialInstruction { get; set; }
        public DateTime CaseReferrerDueDate { get; set; }
        public DateTime CaseSubmittedDate { get; set; }
        public int? SupplierID { get; set; }
        public int WorkflowID { get; set; }
        public string TreatmentTypeName { get; set; }
        public int ReferrerProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ReferrerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AssessmentAuthorisationName { get; set; }
        public string ReferralDownloadPath { get; set; }
        public bool IsTriage { get; set; }
        public bool IsTreatmentRequired { get; set; }
        public string ActionUrl { get; set; }
   
 
    }
}
