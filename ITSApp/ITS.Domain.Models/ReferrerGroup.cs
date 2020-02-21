using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLModel = ITS.Domain.Models;

namespace ITS.Domain.Models
{
    public class ReferrerGroup
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int UserID { get; set; }
        public int ReferrerID { get; set; }
        public int[] MultipleUserID { get; set; }
        public string EncryptedReferrerID { get; set; }
        public string Msg { get; set; }
        public string NewName { get; set; }
        public int UpdateCheck { get; set; }
        public IList<ITS.Domain.Models.ReferrerUserDetail> ReferrerData { get; set; }
    }
}
