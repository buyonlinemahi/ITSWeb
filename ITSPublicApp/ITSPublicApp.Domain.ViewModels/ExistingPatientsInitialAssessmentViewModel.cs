using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class ExistingPatientsInitialAssessmentViewModel
    {
        public IEnumerable<SupplierCasePatient> supplierCasePatient { get; set; }
        public List<ReferrerDocuments> ReferrerDocument { get; set; }
        public string AssessmentFileCustomURLPath { get; set; }
    }
}
