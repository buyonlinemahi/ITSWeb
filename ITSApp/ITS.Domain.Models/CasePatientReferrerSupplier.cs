using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CasePatientReferrerSupplier
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public string CaseNumber { get; set; }
        public string ReferrerName { get; set; }
        public string SupplierName { get; set; }
        public string SuppliersAddress { get; set; }
        public string SuppliersCity { get; set; }
        public string SuppliersRegion { get; set; }
        public string SuppliersPostCode { get; set; }
        public string Phone { get; set; }
        public string TreatmentTypeName { get; set; }
    }
}
