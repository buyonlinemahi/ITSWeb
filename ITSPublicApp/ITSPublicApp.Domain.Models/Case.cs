using System;

namespace ITSPublicApp.Domain.Models
{
    public class Case
    {
        public int CaseID { get; set; }
        public string EncryptDecryptCaseID { get; set; }
        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int ReferrerID { get; set; }
        public int ReferrerProjectID { get; set; }
        public string CaseNumber { get; set; }
        public DateTime? CaseDateOfInquiry { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public int TreatmentTypeID { get; set; }
        public string TreatmentTypeName { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string CaseSpecialInstruction { get; set; }
        public DateTime? CaseReferrerDueDate { get; set; }
        public DateTime? CaseSubmittedDate { get; set; }
        public int? SupplierID { get; set; }
        public int WorkflowID { get; set; }
        public DateTime? PatientContactDate { get; set; }
        public bool IsTriage { get; set; }
        public string InjuryType { get; set; }
        public bool IsCustom { get; set; }
        public string SendInvoiceTo { get; set; }
        public string SendInvoiceName { get; set; }
        public string SendInvoiceEmail { get; set; }
        public int ReferrerAssignedUser { get; set; }
        public int? SupplierAssignedUser { get; set; }
        public string InnovateNote { get; set; }
        public int? OfficeLocationID { get; set; }
        public string EncryptedOfficeLocationID { get; set; }
        public int? EmployeeDetailID { get; set; }
        public int? DrugTestID { get; set; }
        public string EncryptedCaseID { get; set; }
        public string EncryptReferrerAssignedUser { get; set; }
        public int? IsNewPolicyTypeID { get; set; }
        public string NewPolicyReferenceNumber { get; set; }
    }
}
