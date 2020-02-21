using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITS.Domain.Models.SupplierModel
{
    public class Insurance : Document
    {
        public int SupplierInsuredID { get; set; }
        public string LevelOfCover { get; set; }
        public DateTime RenewalDate { get; set; }
        public HttpPostedFileBase InsuredDocumentFile { get; set; }

    }
}
