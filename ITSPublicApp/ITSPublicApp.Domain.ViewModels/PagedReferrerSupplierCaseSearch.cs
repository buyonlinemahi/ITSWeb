using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class PagedReferrerSupplierCaseSearch
    {
        public IEnumerable<ReferrerSupplierCases> Cases { get; set; }
        public CaseSearchCriteria CaseSearchCriteria { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<ReferrerUserDetail> ReferrerGroupUserDetailData { get; set; }
    }
}
