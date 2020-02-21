using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.SupplierModel
{
    public class Document
    {
        public int SupplierDocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentName { get; set; }
        public string UserName { get; set; }
        public string UploadPath { get; set; }
        public string DocumentUrl { get; set; }
    }
}
