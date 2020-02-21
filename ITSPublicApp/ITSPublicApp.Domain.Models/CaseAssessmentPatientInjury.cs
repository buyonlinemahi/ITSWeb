﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class CaseAssessmentPatientInjury
    {
        public int CaseAssessmentPatientInjuryID { get; set; }
        public string AffectedArea { get; set; }
        public string Restriction { get; set; }
        public string Score { get; set; }
        public int CaseAssessmentDetailID { get; set; }
        public int SymptomDescriptionID { get; set; }
        public int StrengthTestingID { get; set; }
        public int AffectedAreaID { get; set; }
        public int RestrictionRangeID { get; set; }
        public string OtherSymptomDesciption { get; set; }
     
    }
}
