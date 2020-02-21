using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseAssessmentDetail
    {
        public int CaseAssessmentDetailID { get; set; }
        public int AssessmentServiceID { get; set; }
        public int CaseID { get; set; }
        public bool? HasThePatientHadTimeOff { get; set; }
        public string AbsentDetail { get; set; }
        public int AbsentPeriod { get; set; }
        public int? AbsentPeriodDurationID { get; set; }
        public bool? HasThePatientReturnedToWork { get; set; }
        public int PatientImpactOnWorkID { get; set; }
        public int PatientWorkstatusID { get; set; }
        public int PatientRecommendedTreatmentSessions { get; set; }
        public string PatientRecommendedTreatmentSessionsDetail { get; set; }
        public int PatientTreatmentPeriod { get; set; }
        public int? PatientTreatmentPeriodDurationID { get; set; }
        public bool IsFurtherTreatmentRecommended { get; set; }
        public int PatientLevelOfRecoveryID { get; set; }
        public int SessionsPatientAttended { get; set; }
        public string DatesOfSessionAttended { get; set; }
        public int SessionsPatientFailedToAttend { get; set; }
        public int? FollowingTreatmentPatientLevelOfRecoveryID { get; set; }
        public string AdditionalInformation { get; set; }
        public bool? HasCompliedHomeExerciseProgramme { get; set; }
        public string PatientTreatmentPeriodDetail { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int? PractitionerID { get; set; }
        public bool IsFurtherInvestigationOrOnwardReferralRequired { get; set; }
        public string FurtherInvestigationOrOnwardReferral { get; set; }
        public string EvidenceOfTreatmentRecommendations { get; set; }
        public string EvidenceOfClinicalReasoning { get; set; }
        public int TreatmentPeriodTypeID { get; set; }
        public DateTime? PatientDateOfReturn { get; set; }
        public string PatientRecommendationReturn { get; set; }
        public bool? IsPatientReturnToPreInjuryDuties { get; set; }
        public DateTime? PatientPreInjuryDutiesDate { get; set; }
        public string MainFactors { get; set; }

    }
}
