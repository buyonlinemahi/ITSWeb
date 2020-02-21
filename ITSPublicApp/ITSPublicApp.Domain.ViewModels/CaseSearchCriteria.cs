using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.ViewModels
{
    public class CaseSearchCriteria
    {
        public int SearchCriteria { get; set; }
        public string SearchText { get; set; }
        public string AdditionalParam { get; set; }
        public string GroupName { get; set; }
    }
}
