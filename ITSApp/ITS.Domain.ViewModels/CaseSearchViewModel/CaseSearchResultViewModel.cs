using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseSearch;
namespace ITS.Domain.ViewModels.CaseSearchViewModel
{
    public class CaseSearchResultViewModel
    {
        public CaseSearch CaseSearch { get; set; }
        public IEnumerable<CasePatientTreatmentWorkflow> Cases { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
