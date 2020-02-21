using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.ViewModels
{
    public class CasePatientSupplierPractitioner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string PostCode { get; set; }
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public string CaseNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
    }
}
