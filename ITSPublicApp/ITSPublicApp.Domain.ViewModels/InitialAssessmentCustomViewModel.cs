using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.Global;

namespace ITSPublicApp.Domain.ViewModels
{
    public class InitialAssessmentCustomViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public HttpPostedFileBase InitialAssessmentCustomUploadFile { get; set; }
        public Case CaseService { get; set; }
        public IEnumerable<SupplierDocument> supplierdocumentModel { get; set; }
        public IEnumerable<ReferrerDocuments> ReferrerDocument { get; set; }
        public string InitialAssessmentCustomURLPath { get; set; }

    }
}
