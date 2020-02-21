using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class PrintViewAsssessmentViewModel
    {
        public IEnumerable<PsychologicalFactor> PsychologicalFactors { get; set; }
        public IEnumerable<PatientWorkstatus> PatientWorkstatuses { get; set; }
        public IEnumerable<PatientImpact> PatientImpacts { get; set; }
        public IEnumerable<PatientImpactValue> PatientImpactValues { get; set; }
        public IEnumerable<PatientImpactOnWork> PatientImpactOnWorks { get; set; }
        public IEnumerable<PatientLevelOfRecovery> PatientLevelOfRecoveries { get; set; }
        public IEnumerable<ProposedTreatmentMethod> ProposedTreatmentMethods { get; set; }
        public Patient Patient { get; set; }
        public IEnumerable<PatientRole> PatientRole { get; set; }
        public Case Case { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public IEnumerable<Duration> Durations { get; set; }
        public Practitioner Practitioner { get; set; }

        
    }
}
