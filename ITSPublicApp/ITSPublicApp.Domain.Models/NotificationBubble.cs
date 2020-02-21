using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class NotificationBubble
    {
        public int NewPatientCount { get; set; }
        public int ExistingPatientsInitialAssessmentCount { get; set; }
        public int ExistingPatientsNextAssessmentCount { get; set; }
        public int AuthorisationCount { get; set; }
        public int StoppedCaseCount { get; set; }
    }
}
