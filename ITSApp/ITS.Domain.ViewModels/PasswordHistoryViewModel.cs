using System.Collections.Generic;
using System.Web;
using ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class PasswordHistoryViewModel
    {
        public IEnumerable<PasswordHistory> PasswordHistoryDetails { get; set; }
    }
}
