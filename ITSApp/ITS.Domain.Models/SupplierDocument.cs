/*
Page Name:  SupplierDocument.cs                      
Latest Version:  1.1                                              
Purpose: to create SupplierDocument model class                                                      
Revision History:                                        
                                                           
1.0 – 12/19/2012, Created By : Robin Singh 

*updated by : robin Singh    
 Version    : 1.1 
 Date       : 12/21/2012
 Purpose:   : Added new field  UploadPath in model.
*/

using System;
using System.IO;
using System.Web;


namespace ITS.Domain.Models
{
    public class SupplierDocument
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
        public int? ReferrerProjectTreatmentID { get; set; }
        public int? CaseId { get; set; }
        public string Documentfullname { get; set; }
     //   public string Username { get; set; }
        public HttpPostedFileBase RegistrationDocumentFileUpload { get; set; }

   
    }
}
