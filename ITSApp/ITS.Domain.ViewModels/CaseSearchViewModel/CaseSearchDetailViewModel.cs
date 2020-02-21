using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.CaseSearch;
using ITS.Domain.Models.SolicitorModel;

using ITS.Domain.Models;

namespace ITS.Domain.ViewModels.CaseSearchViewModel
{
    public class CaseSearchDetailViewModel
    {
        public CasePatientReferrerSupplierWorkflow CasePatientReferrerSupplierWorkflow { get; set; }
        public IEnumerable<CaseNoteUser> CaseNotes { get; set; }
        public IEnumerable<CaseHistoryUser> CaseHistories { get; set; }
        public IEnumerable<CaseTreatmentPricingType> CaseTreatmentPricingTypes { get; set; }
        public IEnumerable<CaseBespokeServicePricingType> CaseBespokeServicePricingTypes { get; set; }
        public IEnumerable<CaseDocumentUser> CaseDocumentUsers { get; set; }
        public string ReferralFileDownloadPath { get; set; }
        public bool IsFileExist { get; set; }
        public Solicitor Solicitor { get; set; }
        public IEnumerable<CaseCommunicationHistoryUser> CaseCommunicationHistoryUser { get; set; }
        public CaseAppointmentDate CaseAppointmentDate { get; set; }
        public IEnumerable<CaseReportsDetail> CaseReportsDetails { get; set; }
        public IEnumerable<CaseAssessmentDetail> CaseAssessmentDetails { get; set; }
        public IEnumerable<ITS.Domain.Models.CaseAssessmentModel.SupplierDistanceRankPrice> SupplierDistanceRankPrice { get; set; }
        public IEnumerable<ITS.Domain.Models.UserModel.SuppliersName> AllSupplierWithinPostCode { get; set; }
        public ITS.Domain.Models.CaseAssessmentModel.CasePatientTreatment CasePatientTreatment { get; set; }
        public IEnumerable<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing> TopTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing { get; set; }
        public IEnumerable<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing> AllTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing { get; set; }
        public int workflowID { get; set; }
        public IEnumerable<SupplierDocument> CaseReportsDetailsCustom { get; set; }
        public string ReportFileDownloadPath { get; set; }
        public IEnumerable<ITS.Domain.Models.CaseAssessmentModel.TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }
        public IEnumerable<CasePatientContactAttempt> CasePatientContactDates { get; set; }
        public IEnumerable<CasePatientContactAttempt> CaseUnsuccessfullContactDates { get; set; }
    }
}