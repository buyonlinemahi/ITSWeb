using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITS.Domain.Models
{
    public class SupplierInsurance
    {

        public int SupplierInsuredID { get; set; }
        public string LevelOfCover { get; set; }
        public DateTime RenewalDate { get; set; }
        public int SupplierDocumentID { get; set; }
        public int SupplierID { get; set; }
        public string UploadPath { get; set; }
        public string DocumentName { get; set; }
        public string InsuredDocumentUrl { get; set; }      
        public DateTime UploadDate { get; set; }
        public HttpPostedFileBase InsuredDocumentFile { get; set; }

    }
}
