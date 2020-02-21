using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseModel;
using ITS.Domain.Models.CaseAssessmentModel;

namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class AuthorisationQAViewModel
    {
        public Case Case { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public IEnumerable<TreatmentCategoriesBespokeService> TreatmentCategoriesBespokeServices { get; set; }
        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }
        public IEnumerable<ReferrerCaseAssessmentModification> ReferrerCaseAssessmentModification { get; set; }
    }
}
