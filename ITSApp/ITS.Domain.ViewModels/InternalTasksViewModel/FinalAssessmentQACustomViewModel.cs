using System.Collections.Generic;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.CaseModel;
using System.Web;

namespace ITS.Domain.ViewModels.InternalTasksViewModel
{
    public class FinalAssessmentQACustomViewModel
    {
        public CasePatientTreatment casePatientTreatment { get; set; }
        public CaseAssessment CaseAssessment { get; set; }
        public List<ITS.Domain.Models.SupplierDocument> supplierDocument { get; set; }
        public Case caseObj { get; set; }
        public CaseCommunicationHistory CaseCommunicationHistory { get; set; }
        public CaseAssessmentDocument CaseAssessmentDocument { get; set; }
        public HttpPostedFileBase FinalAssessmentFileFinal { get; set; }
        public int RefferId { get; set; }
        public int RefferProjectTreatmentID { get; set; }
        public ITS.Domain.Models.CaseAssessmentRating caseAssessmentRating { get; set; }
        public string FinalAssesessmentCustomFilePath { get; set; }
        public ITS.Domain.Models.SupplierDocument supplierCustom { get; set; }
        public ITS.Domain.Models.CaseAssessmentCustom caseAssessmentCustom { get; set; }
        public ITS.Domain.Models.CaseAppointmentDate CaseAppointmentDate { get; set; }
    }
}
