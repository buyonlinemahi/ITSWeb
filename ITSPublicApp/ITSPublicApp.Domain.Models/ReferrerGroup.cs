using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class ReferrerGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int UserID { get; set; }
        public int ReferrerID { get; set; }
    }
}
