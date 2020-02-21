using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.ReferrerModel
{
    public class ReferrerSearchResult
    {
        public int ReferrerID { get; set; }
        public string EncryptedReferrerID { get; set; }
        public string ReferrerName { get; set; }
        public string ReferrerMainContactPhone { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
    }
}
