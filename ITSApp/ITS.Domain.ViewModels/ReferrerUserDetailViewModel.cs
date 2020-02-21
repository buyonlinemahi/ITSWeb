using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.UserModel;
using ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class ReferrerUserDetailViewModel
    {
        public IEnumerable<ITS.Domain.Models.ReferrerUserDetail> ReferrerDetailData { get; set; }
    }
}
