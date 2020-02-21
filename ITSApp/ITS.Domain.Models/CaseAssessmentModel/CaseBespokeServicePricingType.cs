using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseAssessmentModel
{
    public class CaseBespokeServicePricingType
    {
        public int CaseBespokeServiceID { get; set; }
        public int CaseID { get; set; }
        public int TreatmentCategoryBespokeServiceID { get; set; }
        public decimal ReferrerPrice { get; set; }
        public decimal SupplierPrice { get; set; }
        public DateTime? DateOfService { get; set; }
        public bool? PatientDidNotAttend { get; set; }
        public bool? WasAbandoned { get; set; }
        public bool? IsComplete { get; set; }
        public string BespokeServiceName { get; set; }

    }
}
