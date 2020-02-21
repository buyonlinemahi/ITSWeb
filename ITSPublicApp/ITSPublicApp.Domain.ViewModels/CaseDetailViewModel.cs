using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class CaseDetailViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public IEnumerable<CaseAssessmentDetail> CaseAssessmentDetails { get; set; }
        public IEnumerable<SupplierDocument> caseAssessmentReportsCustom { get; set; }
    }
}
