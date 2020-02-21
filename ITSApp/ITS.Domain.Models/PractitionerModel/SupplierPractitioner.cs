using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.PractitionerModel
{
    public class SupplierPractitioner
    {
        public int SupplierPractitionerID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int PractitionerRegistrationID { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationTypeName { get; set; }
        
    }
}
