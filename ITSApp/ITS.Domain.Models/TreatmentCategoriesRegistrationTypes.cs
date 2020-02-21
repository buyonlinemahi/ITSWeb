using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
   public class TreatmentCategoriesRegistrationTypes
    {
        public int RegistrationTypeID { get; set; }
        public string RegistrationTypeName { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int TreatmentCategoryRegistrationTypeID { get; set; }
    }
}
