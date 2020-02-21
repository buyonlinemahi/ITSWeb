using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class PagedCaseWorkflowPatientReferrerProject
    {
        public IEnumerable<CaseWorkflowPatientReferrerProject> CaseWorkflowPatientReferrerProjects { get; set; }
        public int CaseWorkflowPatientReferrerProjectTotalCount { get; set; }
        
    }
}
