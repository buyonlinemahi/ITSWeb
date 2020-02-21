using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseSearch
{
    public class CasePatientReferrerSupplierWorkflow
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }
        public DateTime InjuryDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int CaseID { get; set; }
        public string CaseNumber { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string TreatmentTypeName { get; set; }
        public string SupplierName { get; set; }
        public string ProjectName { get; set; }
        public string SupplierPhone { get; set; }
        public string CurrentStatus { get; set; }
        public string ReferrerName { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int TreatmentTypeID { get; set; }
        public int PatientID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public bool HasLegalRep { get; set; }
        public int GenderID { get; set; }
        public int? SolicitorID { get; set; }
        public string SpecialInstructionNotes { get; set; }
    }
}
