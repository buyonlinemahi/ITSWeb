using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.InvoiceModel
{
   public class CaseInvoicePatientReferrer
    {
       
        public string InvoiceNumber { get; set; }
       
        public decimal InvoiceActualAmount { get; set; }
       
        public decimal? TotalAmountReceived { get; set; }
       
        public int CaseID { get; set; }
       
        public string CaseNumber { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string CaseReferrerReferenceNumber { get; set; }
       
        public string ReferrerContactFirstName { get; set; }
       
        public string ReferrerContactLastName { get; set; }
       
        public string ReferrerMainContactEmail { get; set; }
       
        public int InvoiceID { get; set; }
       
        public int PatientID { get; set; }
       
        public int ReferrerID { get; set; }
       
        public decimal? OutstandingAmount { get; set; }
        public string ReferrerMainContactPhone { get; set; }
        public string ActionUrl { get; set; }
    }
}
