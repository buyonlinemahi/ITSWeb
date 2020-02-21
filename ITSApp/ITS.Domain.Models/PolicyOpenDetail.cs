using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class PolicyOpenDetail
    {
        public int PolicyOpenDetailID { get; set; }
        public string PolicyType { get; set; }
        public string TypeCover { get; set; }
        public string PolicyCriteria { get; set; }
        public string RehabORProportionate { get; set; }
        public string FitforWork { get; set; }
        public string ReInsured { get; set; }
        public string ReferenceNo { get; set; }
        public string Admitted { get; set; }
        public DateTime? OpenBenefitDate { get; set; }
        public decimal OpenMonthlyValue { get; set; }
        public DateTime? OpenEndBenefitDate { get; set; }
        public string NameofReinsurer { get; set; }
        public string OpenPolicyWording { get; set; }
    }
}
