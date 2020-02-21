

using System;
namespace ITSPublicApp.Domain.Models
{
    public class ReferrerCaseAssessmentModificationAuthority
    {
        public int CaseID { get; set; }
        public string Type { get; set; }
        public int QTY { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfService { get; set; }
    }
}
