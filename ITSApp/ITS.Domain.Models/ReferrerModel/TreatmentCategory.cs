using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.ReferrerModel
{
    public class TreatmentCategory
    {
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public bool Enabled { get; set; }
    }
}
