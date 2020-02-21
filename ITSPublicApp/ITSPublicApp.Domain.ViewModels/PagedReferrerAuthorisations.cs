using System.Collections.Generic;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class PagedReferrerAuthorisations
    {
        public IEnumerable<ReferrerAuthorisations> ReferrerAuthorisations { get; set; }
        public int ReferrerAuthorisationTotalCount { get; set; }
        
    }
}