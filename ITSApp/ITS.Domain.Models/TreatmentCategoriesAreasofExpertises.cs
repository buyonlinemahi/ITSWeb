using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class TreatmentCategoriesAreasofExpertises
    {
        public int TreatmentCategoryAreasofExpertiseID { get; set; }
        public int AreasofExpertiseID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string AreasofExpertiseName { get; set; }
        public string TreatmentCategoryName { get; set; }
    }
}
