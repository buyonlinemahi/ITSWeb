using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region
/*
*  Latest Version  : 1.0

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 08-March-2013
*  Description  : Created the Practitioner Registration Model

*/
#endregion


namespace ITS.Domain.Models
{
    public class PractitionerRegistration
    {
        public int PractitionerID { get; set; }
        public int PractitionerRegistrationID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int? RegistrationTypeID { get; set; }
        public string RegistrationNumber { get; set; }
        public string Qualification { get; set; }
        public DateTime QualificationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int YearsQualified { get; set; }
        public bool IsActive { get; set; }

        public string TreatmentCategoryName { get; set; }
        public string RegistrationTypeName { get; set; }
    }
}
