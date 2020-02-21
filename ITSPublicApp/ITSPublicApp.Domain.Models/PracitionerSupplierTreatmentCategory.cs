using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class PracitionerSupplierTreatmentCategory
    {
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public int SupplierID { get; set; }
        public int SuplierPractitionerID { get; set; }
        public int PractitionerRegistrationID { get; set; }
        public int PractitionerID { get; set; }
        public int TreatmentCategoryID { get; set; }
    }
}
