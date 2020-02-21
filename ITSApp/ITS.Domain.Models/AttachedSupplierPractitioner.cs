using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class AttachedSupplierPractitioner
    {
        public string PractitionerName { get; set; }
        public string PractitionerRegistrationType { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int SupplierID { get; set; }
        public int PractitionerID { get; set; }
        public int SupplierPractitionerID { get; set; }
        public bool IsActive { get; set; }

    }
}
