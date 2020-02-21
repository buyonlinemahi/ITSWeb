using System.Collections.Generic;
using System.Web;
using ITSPublicApp.Domain.Models;
namespace ITSPublicApp.Domain.ViewModels
{
  public  class CaseDocumentUserViewModel
    {
      public IEnumerable<CaseDocumentUser> CaseDocumentUsers { get; set; }
      public IEnumerable<ReferrerDocumentType> ReferrerDocumentTypes { get; set; }
    }
}
