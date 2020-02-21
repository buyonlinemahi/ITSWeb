/*
Page Name:  SupplierDocumentUser.cs                      
Latest Version:  1.0                                             
Purpose: to create SupplierDocumentUser model class                                                                                              
1.0 – 02/14/2013, Created By : Robin Singh 
 * 
*/

using System;
using System.IO;
using System.Web;


namespace ITS.Domain.Models
{
    public class SupplierDocumentUser
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

        public HttpPostedFileBase RegistrationDocumentFileUpload { get; set; }

    }
}
