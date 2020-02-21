using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.InternalTaskModel;


namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class PagedCaseWorkflowPatientReferrerProject
    {
        public IEnumerable<CaseWorkflowPatientReferrerProject> CaseWorkflowPatientReferrerProjects { get; set; }
        public int CaseWorkflowPatientReferrerProjectTotalCount { get; set; }
        public int TreatmentCategoryID { get; set; }
    }
}
