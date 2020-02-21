using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITS.Domain.Models.CaseAssessmentModel
{
    public class CaseAssessmentDocument
    {
        public int CaseID { get; set; }
        public int DocumentTypeID { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentName { get; set; }
        public string UploadPath { get; set; }
        public int? UserID { get; set; }
        public HttpPostedFileBase FinalVersionFileUpload { get; set; }
       
    }
}
