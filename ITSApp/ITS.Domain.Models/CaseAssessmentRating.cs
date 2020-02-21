using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseAssessmentRating
    {
        public int CaseAssessmentRatingID { get; set; }
        public int CaseID { get; set; }
        public int AssessmentServiceID { get; set; }  
        public decimal Rating { get; set; }
        public DateTime RatingDate { get; set; }
    }
}
