using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseAssessmentModel
{
    public class CasePatientTreatment
    {
        public int PatientID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public DateTime InjuryDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string CaseNumber { get; set; }
        public DateTime CaseDateOfInquiry { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string TreatmentTypeName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string ReferralFileDownloadPath { get; set; }
        public DateTime CaseSubmittedDate { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int CaseID { get; set; }
        public int? SupplierID { get; set; }
        public string InjuryType { get; set; }
        public bool IsTriage { get; set; }
    }
}
