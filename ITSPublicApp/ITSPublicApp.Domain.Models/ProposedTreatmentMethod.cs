using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class ProposedTreatmentMethod
    {
        public int ProposedTreatmentMethodID { get; set; }
        public string ProposedTreatmentMethodName { get; set; }
        public bool Checked { get; set; }
    }
}
