using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class UpdateReferrerGroup
    {
        public string NewName { get; set; }
        public string GroupName { get; set; }
        public int ReferrerID { get; set; }
        public string UserID { get; set; }
        public int[] MultipleUserID { get; set; }
    }
}
