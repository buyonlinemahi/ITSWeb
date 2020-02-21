using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ITS.Domain.Models.CaseAssessmentModel;

namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
  public  class TriageAssessmentQAViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }

        public HttpPostedFileBase TriageAssessmentUploadFile { get; set; }

    }
}
