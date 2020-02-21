using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.InternalTaskModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.CaseModel;
using ITS.Domain.Models.UserModel;
using ITS.Domain.Models.SupplierModel;
using ITS.Domain.Models.CaseSearch;

namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class PatientCaseAndSupplierWithPatientPostCodeViewModel
    {
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public CasePatientReferrerSupplierWorkflow CasePatientReferrerSupplierWorkflow { get; set; }
        public IEnumerable<SupplierDistanceRankPrice> SupplierDistanceRankPrice { get; set; }
        public IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> TopTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing { get; set; }
        public IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> AllTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing { get; set; }
        public IEnumerable<CaseDocumentUser> CaseDocumentUsers { get; set; }
        public IEnumerable<SuppliersName> AllSupplierWithinPostCode { get; set; }
        public string CaseSpecialInstruction { get; set; }
        public bool IsFileExist { get; set; }
    }
}
