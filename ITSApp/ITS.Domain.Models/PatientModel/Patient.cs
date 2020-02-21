using System;

namespace ITS.Domain.Models.PatientModel
{
    public class Patient
    {
        public int PatientID { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public DateTime InjuryDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public int GenderID { get; set; }
        public string Email { get; set; }
        public string HasLegalRep { get; set; }
        public int? SolicitorID { get; set; }
        public string CaseNumber { get; set; }
        public string TreatmentCategory { get; set; }
        public string TreatmentType { get; set; }
        public int CaseId { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string SpecialInstructionNotes { get; set; }
        public int? PolicyID { get; set; }
        public int? EmploymentID { get; set; }
        public int? PrimaryConditionID { get; set; }
    }
}
