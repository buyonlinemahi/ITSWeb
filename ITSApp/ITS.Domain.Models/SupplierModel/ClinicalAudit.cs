using System;
using System.Web;
using ITS.Domain.Models.SupplierModel;

namespace ITS.Domain.Models.SupplierModel
{

    public class ClinicalAudit : Document
    {
        public int CaseID { get; set; }

        public DateTime AuditDate { get; set; }
        public int SupplierClinicalAuditID { get; set; }
        public Boolean AuditPass { get; set; }
        public string CaseNumber { get; set; }
        public HttpPostedFileBase ClinicalAuditDocumentFileUpload { get; set; }
        public string ClinicalAuditOldFileName { get; set; }

        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
    }
}
