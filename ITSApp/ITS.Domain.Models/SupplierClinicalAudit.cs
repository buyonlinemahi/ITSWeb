using System;
using System.Web;


#region Comments
/*
 * Latest Version 1.2
 * 
 * Author       : Pardeep Kumar
 * Date         : 01-Jan-2013
 * Version      : 1.0
 * 
 * Modified By  : Pardeep Kumar
 * Date         : 25-Jan-2013
 * Description  : Updated data type of SupplierClinicalAuditStatus from String to boolean in  SupplierClinicalAudit Model
 * Version      : 1.1
 * 
 * 
 * Modified by    : Pardeep Kumar
 * Date           : 11-Feb-2013
 * Latest version : 1.2
 * Description    : Removed the DataMember named ReferrerID
*/
#endregion

namespace ITS.Domain.Models
{

    public class SupplierClinicalAudit
    {
        public int CaseID { get; set; }

        public DateTime AuditDate { get; set; }
        public int SupplierClinicalAuditID { get; set; }
        public Boolean AuditPass { get; set; }
        public int SupplierDocumentID { get; set; }
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string CaseNumber { get; set; }
        public string UploadPath { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }
        public HttpPostedFileBase ClinicalAuditDocumentFileUpload { get; set; }
        public string ClinicalAuditOldFileName { get; set; }
    }
}
