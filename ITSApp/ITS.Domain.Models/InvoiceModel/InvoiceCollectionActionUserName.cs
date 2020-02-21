using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.InvoiceModel
{
    public class InvoiceCollectionActionUserName
    {
        public int InvoiceCollectionActionID { get; set; }

        public int InvoiceID { get; set; }

        public string Action { get; set; }

        public string UserName { get; set; }

        public int UserID { get; set; }

        public DateTime FollowUpDate { get; set; }

        public DateTime DateofAction { get; set; }
    }
}
