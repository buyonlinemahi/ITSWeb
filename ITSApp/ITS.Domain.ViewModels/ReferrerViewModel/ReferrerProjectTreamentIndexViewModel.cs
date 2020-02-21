using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.ReferrerProjectTreatmentModel;
using ITS.Domain.Models.ReferrerModel;

namespace ITS.Domain.ViewModels.ReferrerViewModel
{
    public class ReferrerProjectTreamentIndexViewModel
    {
        public ProjectTreatment ProjectTreatment { get; set; }

        public IEnumerable<ProjectTreatmentAssessmentService> ProjectTreatmentAssessmentService { get; set; }

        public IEnumerable<ProjectTreatmentDelegateAuthorisation> ProjectTreatmentDelegateAuthorisation { get; set; }

        public IEnumerable<ProjectTreatmentDocumentSetup> ProjectTreatmentDocumentSetup { get; set; }

        public IEnumerable<ProjectTreatmentEmailTypeName> ProjectTreatmentEmailSetup { get; set; }

        public ProjectTreatmentInvoiceMethod ProjectTreatmentInvoiceMethod { get; set; }

        public IEnumerable<TreatmentPricing> ProjectTreatmentPricing { get; set; }

        public IEnumerable<ProjectTreatmentSLAName> ProjectTreatmentSLA { get; set; }
        public bool IsTriage { get; set; }
    } 
}
