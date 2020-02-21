using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseAssessmentModel;


namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class CaseCompleteInvoicingViewModel
    {
        public IEnumerable<CaseTreatmentPricingType> CaseTreatmentPricingType { get; set; }
        public IEnumerable<CaseBespokeServicePricingType> CaseBespokeServicePricingType { get; set; }
        public int CaseID { get; set; }
        public ReferrerAndSupplierPricing ReferrerAndSupplierVATPricing { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricings { get; set; }
    }
}
