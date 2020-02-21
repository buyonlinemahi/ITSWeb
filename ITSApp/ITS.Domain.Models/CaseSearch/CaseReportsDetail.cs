using System;

namespace ITS.Domain.Models.CaseSearch
{
    public class CaseReportsDetail
    {
        public string DocumentName { get; set; }

        public int CaseID { get; set; }

        public DateTime EventDate { get; set; }

        public int UserID { get; set; }

        public string EventDescription { get; set; }

        public int EventTypeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int CaseAssessmentDetailID { get; set; }
        
        public int AssessmentServiceID { get; set; }

    }
}