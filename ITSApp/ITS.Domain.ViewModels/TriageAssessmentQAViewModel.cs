using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using ITS.Domain.Models;


namespace ITS.Domain.ViewModels
{
    public class TriageAssessmentQAViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }

        public HttpPostedFileBase TriageAssessmentUploadFile { get; set; }

       

    }
}
