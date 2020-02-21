using System;
using System.Collections.Generic;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class PrintBlankFinalAssessmentViewModel
    {
        public IEnumerable<Practitioner> Practitioners { get; set; }
        public IEnumerable<PsychologicalFactor> PsychologicalFactors { get; set; }
        public IEnumerable<PatientWorkstatus> PatientWorkstatuses { get; set; }
        public IEnumerable<PatientImpact> PatientImpacts { get; set; }
        public IEnumerable<PatientImpactValue> PatientImpactValues { get; set; }
        public IEnumerable<PatientImpactOnWork> PatientImpactOnWorks { get; set; }
        public IEnumerable<PatientLevelOfRecovery> PatientLevelOfRecoveries { get; set; }
        public IEnumerable<ProposedTreatmentMethod> ProposedTreatmentMethods { get; set; }
        public IEnumerable<PatientRole> PatientRole { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public IEnumerable<Duration> Duration { get; set; }
        public DateTime DateOfInitialAssessment { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public IEnumerable<TreatmentCategoriesBespokeService> TreatmentCategoriesBespokeServices { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }
        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        public IEnumerable<CaseTreatmentPricingType> CaseTreatmentPricingTypes { get; set; }
        public Patient Patient { get; set; }
        public Case Case { get; set; }
    }
}