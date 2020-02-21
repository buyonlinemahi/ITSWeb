using System.Collections.Generic;
using ITSPublicApp.Domain.Models;
using System.Web;

namespace ITSPublicApp.Domain.ViewModels
{
    public class ReviewAssessmentCustomViewModel
    {
        public IEnumerable<CaseTreatmentPricingType> CaseTreatmentPricingTypes { get; set; }
        public IEnumerable<CaseBespokeServicePricingType> CaseBespokeServicePricingTypes { get; set; }
        public CasePatientTreatment casePatientTreatment { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        public CaseAssessmentCustom caseAssessmentCustom { get; set; }

        //
        public HttpPostedFileBase AssessmentCustomUploadFile { get; set; }
        public string AssessmentFileCustomURLPath { get; set; }
        public IEnumerable<SupplierDocument> supplierdocumentModel { get; set; }
        public List<ReferrerDocuments> ReferrerDocument { get; set; }
        public Case CaseService { get; set; }
        public bool IsFurtherTreatment { get; set; }
        public bool RequiredTreatment { get; set; }
    }
}
