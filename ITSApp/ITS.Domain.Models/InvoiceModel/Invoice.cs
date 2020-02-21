using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.InvoiceModel
{
    public class Invoice
    {

        public int InvoiceID { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal Amount { get; set; }

        public int CaseID { get; set; }

        public int UserID { get; set; }

        public DateTime InvoiceDate { get; set; }

        public bool IsComplete { get; set; }
    }
}
