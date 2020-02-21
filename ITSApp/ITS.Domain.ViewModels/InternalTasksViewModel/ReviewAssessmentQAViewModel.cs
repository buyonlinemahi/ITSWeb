using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.PractitionerModel;
using ITS.Domain.Models.PatientModel;


namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class ReviewAssessmentQAViewModel
    {
      
        public Patient Patient { get; set; }
        public Case Case { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public IEnumerable<Practitioner> Practitioners { get; set; }
        public DateTime DateOfInitialAssessment { get; set; }
        public IEnumerable<PatientWorkstatus> PatientWorkstatuses { get; set; }
        public IEnumerable<PatientImpact> PatientImpacts { get; set; }
        public IEnumerable<PatientImpactValue> PatientImpactValues { get; set; }
        public IEnumerable<PatientImpactOnWork> PatientImpactOnWorks { get; set; }
        public IEnumerable<PatientLevelOfRecovery> PatientLevelOfRecoveries { get; set; }
        public IEnumerable<ProposedTreatmentMethod> ProposedTreatmentMethods { get; set; }
        public IEnumerable<TreatmentCategoriesBespokeService> TreatmentCategoriesBespokeServices { get; set; }
        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        //  public IEnumerable<PsychologicalFactor> PsychologicalFactors { get; set; }
        public IEnumerable<Duration> Durations { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }
        public IEnumerable<PatientRole> PatientRole { get; set; }
        public CaseCommunicationHistory CaseCommunicationHistory { get; set; }
        public CaseAssessmentDocument CaseAssessmentDocument { get; set; }
        public ITS.Domain.Models.CaseAppointmentDate caseAppointmentDate { get; set; }

        public IEnumerable<AffectedArea> AffectedAreas { get; set; }
        public IEnumerable<RestrictionRange> RestrictionRanges { get; set; }
        public IEnumerable<StrengthTesting> StrengthTestings { get; set; }
        public IEnumerable<SymptomDescription> SymptomDescriptions { get; set; }
        public IEnumerable<TreatmentPeriodType> TreatmentPeriodTypes { get; set; }

    
    }
}
