using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.InvoiceModel;

namespace ITS.Domain.ViewModels.InvoiceViewModel
{
   public  class PagedCaseInvoicePatientReferrer
    {
       public IEnumerable<CaseInvoicePatientReferrer> caseInvoicePatientReferrer { get; set; }
       public int TotalCount { get; set; }
    }
}
