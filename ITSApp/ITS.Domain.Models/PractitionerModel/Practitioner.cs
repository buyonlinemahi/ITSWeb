using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.PractitionerModel
{
    public class Practitioner
    {
        public int PractitionerID { get; set; }
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public IEnumerable<int> AreaofExpertiseIDs { get; set; }
        
    }
}
