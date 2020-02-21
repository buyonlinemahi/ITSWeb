using System;
using ITSPublicApp.Domain.Models;
using System.Web;

namespace ITSPublicApp.Domain.ViewModels
{
    public class ReferrerDocumentViewModel
    {
        public ReferrerDocuments _ReferrerDocuments { get; set; }
        public HttpPostedFile _ReferrerUploadFile { get; set; }
    }
}
