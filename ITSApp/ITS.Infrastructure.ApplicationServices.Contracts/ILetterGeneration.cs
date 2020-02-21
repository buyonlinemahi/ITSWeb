using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models;
using ITS.Domain.Models.CaseAssessmentModel;

namespace ITS.Infrastructure.ApplicationServices.Contracts
{
    public interface ILetterGeneration
    {
        byte[] GeneratePatientReferrerToSupplier(CasePatientReferrerSupplier casePatientReferrerSupplier, ITSUser currentUser);
        byte[] GenerateInvoice(ITS.Domain.Models.CaseModel.CasePatientReferrer casePatientReferrer, IEnumerable<CaseTreatmentPricingType> treatmentPricing,
            IEnumerable<CaseBespokeServicePricingType> bespokePricing, decimal vat, string imageLogoPath, out decimal totalAmountOut, out string invoiceNumberOut);
    }
}
