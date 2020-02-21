using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
  public  class PricingTypes
    {
        public int PricingTypeID { get; set; }
        public string PricingTypeName { get; set; }
        public decimal? Price { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
    }
}
