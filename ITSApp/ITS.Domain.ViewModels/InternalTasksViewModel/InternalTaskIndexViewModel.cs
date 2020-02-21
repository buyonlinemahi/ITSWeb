using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.InternalTaskModel;

namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class InternalTaskIndexViewModel
    {
        public IEnumerable<CaseWorkflowCount> caseWorkflowCount { get; set; }
        public int TreatmentCategoryID { get; set; }
    }
}
