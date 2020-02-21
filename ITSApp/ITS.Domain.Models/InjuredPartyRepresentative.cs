using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class InjuredPartyRepresentative
    {
        public int InjuredID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ReferralID { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Relationship { get; set; }
    }
}
