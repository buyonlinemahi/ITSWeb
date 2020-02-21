using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.PractitionerModel;
namespace ITS.Domain.ViewModels
{
    public class PractitionerSearchResultViewModel
    {

        public PractitionerSearch PractitionerSearch { get; set; }
        public IEnumerable<PractitionerSearchResult> Practitioners { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
