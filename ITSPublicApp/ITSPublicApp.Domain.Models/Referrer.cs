using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class Referrer
    {
        public int ReferrerID { get; set; }
        
        public string ReferrerName { get; set; }
        public string ReferrerContactFirstName { get; set; }
        public string ReferrerContactLastName { get; set; }
        public string ReferrerMainContactEmail { get; set; }
        public string ReferrerMainContactFax { get; set; }
        public string ReferrerMainContactPhone { get; set; }
        public string DateAdded { get; set; }
        public bool? IsPolicyDetail { get; set; }
        public bool? IsEmploymentDetail { get; set; }        
        public bool? IsEmploeeDetail { get; set; }
        public bool? IsDrugandAlcoholTest { get; set; }
        public bool? IsRepresentation { get; set; }
        public bool? IsAdditionalQuestion { get; set; }
        public bool? IsJobDemand { get; set; }
        public string IsPolicyDetailOpenOrDropdowns { get; set; }
        public string IsNewPolicyTypes { get; set; }

    }
}
