using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.Global;

namespace ITSPublicApp.Domain.ViewModels
{
    public class TriageAssessmentViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public HttpPostedFileBase TriageAssessmentUploadFile { get; set; }
        public Case Case { get; set; }
        
    }
}
