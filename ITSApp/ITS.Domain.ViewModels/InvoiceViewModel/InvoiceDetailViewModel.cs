using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.InvoiceModel;

namespace ITS.Domain.ViewModels.InvoiceViewModel
{
   public  class InvoiceDetailViewModel
    {
     public  CaseInvoicePatientReferrer CaseInvoicePatientReferrer { get; set; }
     public IEnumerable<InvoicePaymentUserName> InvoicePaymentHistory { get; set; }
     public IEnumerable<InvoiceCollectionActionUserName> InvoiceCollectionActionHistory { get; set; }
    }
}
