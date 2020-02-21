using System.Collections.Generic;
using System.Web;
using ITSPublicApp.Domain.Models;


namespace ITSPublicApp.Domain.ViewModels
{
   public class PasswordHistoryViewModel
    {
        public IEnumerable<PasswordHistory> PasswordHistoryDetails { get; set; }
    }
}
