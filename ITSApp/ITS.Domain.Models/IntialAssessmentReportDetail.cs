using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
  public  class IntialAssessmentReportDetail
    {
        public string CaseReferrerReferenceNumber { get; set; }
        public string SupplierName { get; set; }
        public string PatientFullName { get; set; }
        public string Email { get; set; }
        public int? TotalSession { get; set; }
        public int DelegatedAuthorisationTypeID { get; set; }
    }
}
