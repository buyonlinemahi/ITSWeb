using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class Policie
    {
        public int PolicyID { get; set; }
        public int? PolicyTypeId { get; set; }
        public int? TypeCoverId { get; set; }
        public int? PolicyCriteriaId { get; set; }
        public bool? RehabProportionateBenefit { get; set; }
        public int? FitForWorkId { get; set; }
        public bool? ReInsuredId { get; set; }
        public string ReferenceNo { get; set; }
        public int? AdmittedId { get; set; }
        public DateTime? BenefitDate { get; set; }
        public decimal MonthlyValue { get; set; }
        public decimal WeeklyValue { get; set; }
        public DateTime? EndBenefitDate { get; set; }
        public string NameOfReinsurer { get; set; }
        public string PolicyWording { get; set; }
        public int NameOfReinsurerID { get; set; }
    }
}
