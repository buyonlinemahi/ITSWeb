using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class Practitioner
    {
        public int PractitionerID { get; set; }
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public string PractitionerFullName { get { return this.PractitionerFirstName + " " + this.PractitionerLastName; } }
    }
}
