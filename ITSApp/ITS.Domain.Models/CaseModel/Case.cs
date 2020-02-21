using System;
namespace ITS.Domain.Models.CaseModel
{
    public class Case
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
        public string ReferrerName { get; set; }
        public string PatientName { get; set; }
        public int? ReferrerAssignedUser { get; set; }
        
    }
}
