using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.PractitionerModel
{
    public class Expertise 
    {
        public int PractitionerExpertiseID { get; set; }
        public int PractitionerID { get; set; }
        public int AreaofExpertiseID { get; set; }
    }
}
