using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.PractitionerModel;
using ITS.Domain.Models.PatientModel;
using System.Web;


namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class ReviewAssessmentCustomQAViewModel
    {
        public Patient Patient { get; set; }
        public Case Case { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public CaseCommunicationHistory CaseCommunicationHistory { get; set; }
        public IEnumerable<TreatmentReferrerSupplierPricing> TreatmentReferrerSupplierPricing { get; set; }
        public IEnumerable<TreatmentCategoriesBespokeService> TreatmentCategoriesBespokeServices { get; set; }

        public IEnumerable<CaseTreatmentPricing> CaseTreatmentPricings { get; set; }
        public IEnumerable<CaseBespokeServicePricing> CaseBespokeServicePricings { get; set; }
        public CaseAssessmentDocument CaseAssessmentDocument { get; set; }
        public HttpPostedFileBase ReviewAssessmentFileFinal { get; set; }
        public int RefferId { get; set; }
        public int RefferProjectTreatmentID { get; set; }
        public ITS.Domain.Models.CaseAssessmentRating caseAssessmentRating { get; set; }
        public ITS.Domain.Models.CaseAssessmentCustom caseAssessmentCustom { get; set; }
        public IEnumerable<ITS.Domain.Models.SupplierDocument> supplierDocumentCustom { get; set; }
        public string ReviewAssesessmentCustomFilePath { get; set; }
        public ITS.Domain.Models.SupplierDocument supplierCustom { get; set; }
        public ITS.Domain.Models.CaseAppointmentDate CaseAppointmentDate { get; set; }
    }
}
