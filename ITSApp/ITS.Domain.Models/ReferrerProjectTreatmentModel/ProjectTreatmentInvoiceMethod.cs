using System.Collections.Generic;
using ITS.Domain.Models;
namespace ITS.Domain.Models.ReferrerProjectTreatmentModel
{
    public class ProjectTreatmentInvoiceMethod
    {
        public int ReferrerProjectTreatmentInvoiceID { get; set; }
        public decimal? InvoicePrice { get; set; }
        public decimal? ManagementPrice { get; set; }
        public bool? ManagementFeeEnabled { get; set; }
        public int InvoiceMethodID { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public IEnumerable<InvoiceMethod> ReferrerInvoiceMethods { get; set; }
    }
}
