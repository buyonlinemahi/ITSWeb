using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseTreatmentPricing
    {

        //public int CaseTreatmentPricingID { get; set; }

        //public int CaseID { get; set; }

        //public int ReferrerPricingID { get; set; }

        //public decimal Price { get; set; }

        //public DateTime? DateOfService { get; set; }

        //public bool? PatientDidNotAttend { get; set; }

        //public bool? WasAbandoned { get; set; }

        //public bool? IsComplete { get; set; }

        public string PriceString { get; set; }

        public int CaseTreatmentPricingID { get; set; }
        public int CaseID { get; set; }
        public int ReferrerPricingID { get; set; }
        public decimal ReferrerPrice { get; set; }
        public DateTime? DateOfService { get; set; }
        public bool? PatientDidNotAttend { get; set; }
        public bool? WasAbandoned { get; set; }
        public bool? IsComplete { get; set; }
        public int? SupplierPriceID { get; set; }
        public decimal SupplierPrice { get; set; }
        public int Quantity { get; set; }

    }
}
