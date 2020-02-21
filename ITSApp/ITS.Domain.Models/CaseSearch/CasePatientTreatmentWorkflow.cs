using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseSearch
{
    public class CasePatientTreatmentWorkflow
    {
        public int CaseID { get; set; }
        public string TreatmentTypeName { get; set; }
        public int? TreatmentTypeID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string Definition { get; set; }
        public int WorkflowID { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string CaseNumber { get; set; }
        public string PostCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PatientID { get; set; }
        public string ReferrerName { get; set; }  

    }
}
