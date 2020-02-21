using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseAssessmentCustom
    {
        public int CaseAssessmentID { get; set; }
        public int CaseID { get; set; }
        public string Message { get; set; }
        public bool IsFurtherTreatment { get; set; }
        public bool isAccepted { get; set; }
        public string FinalAssessmentMessage { get; set; }
        public string ReviewAssessmentMessage { get; set; }
    }
}
