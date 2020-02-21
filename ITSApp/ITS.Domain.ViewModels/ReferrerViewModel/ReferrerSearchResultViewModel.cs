using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.ReferrerModel;

namespace ITS.Domain.ViewModels
{
   public class ReferrerSearchResultViewModel
    {

       public ReferrerSearch ReferrerSearch { get; set; }
       public IEnumerable<ReferrerSearchResult> Referrers { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
