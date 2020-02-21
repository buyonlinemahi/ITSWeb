using System;

namespace ITSPublicApp.Domain.Models
{
    public class CasePatientContactAttempt
    {
        public int CasePatientContactAttemptID { get; set; }
        public int PatientID { get; set; }
        public int CaseID { get; set; }
        public DateTime ContactAttemptDate { get; set; }
    }
}
