using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class AuthorisationAcceptanceViewModel
    {
        public CaseAssessment CaseAssessment { get; set; }
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public IEnumerable<TreatmentCategoriesBespokeService> TreatmentCategoriesBespokeServices { get; set; }
        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }

    }
}
