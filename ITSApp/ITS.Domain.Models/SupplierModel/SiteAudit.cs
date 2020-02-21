using System;
using System.Web;

#region Comment
/*
    * Latest Version : 1.4
  
    * Author         : Manjit Singh
    * Latest Version : 1.0
    * Purpose        : Model For Supplier Site Audit    
    * Date           : 27-Dec-2012   
  
    * Updated By     : Manjit Singh
    * Latest Version : 1.1
    * Purpose        : Model Changed(added UserName) For Supplier Site Audit     
    * Date           : 28-Dec-2012  
  
    * Updated By     : Manjit Singh
    * Latest Version : 1.2
    * Purpose        : Model Changed(added UploadPath) For Supplier Site Audit     
    * Date           : 7-Jan-2013 
  
    * Updated By     : Manjit Singh
    * Latest Version : 1.3
    * Purpose        : Model Changed(added DocumentName) For Supplier Site Audit     
    * Date           : 7-Jan-2013 
  
    * Updated By     : Manjit Singh
    * Latest Version : 1.4
    * Purpose        : Model Changed(added UploadDate) For Supplier Site Audit     
    * Date           : 19-Feb-2013
*/
#endregion
namespace ITS.Domain.Models.SupplierModel
{
    public class SiteAudit : Document
    {
        public int SupplierSiteAuditID { get; set; }
        public string AuditNotes { get; set; }
        public DateTime AuditDate { get; set; }
        public bool AuditPass { get; set; }
        public HttpPostedFileBase FileUploadSiteAudit { get; set; }

    }
}
