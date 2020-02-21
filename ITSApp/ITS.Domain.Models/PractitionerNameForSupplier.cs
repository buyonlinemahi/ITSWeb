using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class PractitionerNameForSupplier
    {

        public int SupplierPractitionerID { get; set; }
        public bool IsActive { get; set; }
        public string PractitionerFirstName { get; set; }
        public int PractitionerID { get; set; }
        public string PractitionerLastName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationTypeName { get; set; }
        public string TreatmentCategoryName { get; set; }

    }
}
