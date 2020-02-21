using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.UserModel;

namespace ITS.Domain.ViewModels
{
    public class UserSearchResultViewModel
    {
        public UserSearch UserSearch { get; set; }
        public IEnumerable<UserSearchResult> Users { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
