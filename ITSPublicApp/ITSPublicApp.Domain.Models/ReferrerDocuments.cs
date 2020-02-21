using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITSPublicApp.Domain.Models
{
    public class ReferrerDocuments
    {
        public int ReferrerDocumentID { get; set; }
        public int ReferrerID { get; set; }
        public string DocumentName { get; set; }
        public int DocumentTypeID { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserID { get; set; }
        public string UploadPath { get; set; }
        public int? ReferrerProjectTreatmentID { get; set; }
        public int? ReferrerDocumentTypeID { get; set; }
        public int? CaseID { get; set; }
        public string ReferrerDocumentType { get; set; }
        public DateTime DocumentDate { get; set; }
        public string TempCaseID { get; set; }
        public string ReferrerDocumentTypeDesc { get; set; }
        public bool? SupplierCheck { get; set; }
        public bool? ReferrerCheck { get; set; }
        public string EncryptedCaseID { get; set; }
    }
}
