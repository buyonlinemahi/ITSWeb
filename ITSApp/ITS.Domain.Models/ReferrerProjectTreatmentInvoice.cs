using System.Collections.Generic;

namespace ITS.Domain.Models
{
    public class ReferrerProjectTreatmentInvoice
    {
        public int ReferrerProjectTreatmentInvoiceID { get; set; }
        public double InvoicePrice { get; set; }
        public double ManagementPrice { get; set; }
        public bool ManagementFeeEnabled { get; set; }
        public int InvoiceMethodID { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public IEnumerable<InvoiceMethod> ReferrerInvoiceMethodTreatment { get; set; }
    }
}
