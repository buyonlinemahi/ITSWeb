using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITS.Domain.Models.SupplierModel
{
    public class Registration : Document
    {
        public HttpPostedFileBase RegistrationDocumentFileUpload { get; set; }
    }
}
