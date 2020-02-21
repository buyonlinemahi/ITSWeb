using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Infrastructure.Global;
using AutoMapper;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.ViewModels.CaseStoppedViewModel;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Domain.Models.CaseModel;
using ITS.Infrastructure.ApplicationServices;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.IO;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class CaseCompleteController : BaseController
    {
        //
        // GET: /CaseComplete/

        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.FinanceService.IFinanceService _financeService;
      
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly EmailService _emailService;
        private readonly ILetterGeneration _letterGeneration;
       
        public CaseCompleteController(EmailService emailService, ITSService.CaseService.ICaseService caseService, 
            ITSService.FinanceService.IFinanceService financeService, ILetterGeneration letterGeneration, ITSService.PatientService.IPatientService patientService)
        {
            _caseService = caseService;

            _financeService = financeService;
            _emailService = emailService;
            _letterGeneration = letterGeneration;
            _patientService = patientService;
        }
        public ActionResult Index()
        {
            return View(GetCaseCompleteCaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }
        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetCaseCompleteCaseWorkflowPatientProjects(int skip, int take)
        {
            var pagedCaseWorkflowPatientCaseComplete = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
               (_caseService.GetCaseCompletedCaseWorkflowPatientProjects(skip, take));
            pagedCaseWorkflowPatientCaseComplete.CaseWorkflowPatientReferrerProjects.ToList().ForEach(obj =>
            {
                obj.ActionUrl = Url.Action("CaseCompleteScreen", GlobalConst.Controllers.CaseComplete, new { id = obj.CaseID });
            });
            pagedCaseWorkflowPatientCaseComplete.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return (pagedCaseWorkflowPatientCaseComplete);
        }


        [HttpPost]
        public ActionResult GetCaseComplete(int treatmentCategoryID, int skip, int take)
        {

            PagedCaseWorkflowPatientReferrerProject viewModel;
            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
                viewModel = GetCaseCompleteCaseWorkflowPatientProjects(skip, take);
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetCaseCompletedCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(obj =>
                {
                    obj.ActionUrl = Url.Action("CaseCompleteScreen", GlobalConst.Controllers.CaseComplete, new { id = obj.CaseID });
                });

                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);

        }


        #region Case Complete Screen

        public ActionResult CaseCompleteScreen(int id)
        {
            int CaseId = id;
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(id));
            var viewModel = new CaseCompleteInvoicingViewModel()
            {
                CaseID = CaseId,
                CaseTreatmentPricingType = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingsForInvoice(CaseId)),
                CaseBespokeServicePricingType = Mapper.Map<IEnumerable<CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingForInvoice(CaseId)),
                ReferrerAndSupplierVATPricing = Mapper.Map<ReferrerAndSupplierPricing>(_caseService.GetReferrerAndSupplierPricingVat(CaseId)),
                TreatmentReferrerSupplierPricings = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID)),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaseCompleteSubmit(int caseID, CaseCompleteInvoicingViewModel caseCompleteInvoicingViewModel, CaseCommunicationHistory caseCommunicationHistory)
        {
            int userID = ITSUser.UserID;

            if (caseCompleteInvoicingViewModel.CaseTreatmentPricingType != null)
            {
                caseCompleteInvoicingViewModel.CaseTreatmentPricingType.ToList().ForEach(caseTreatmentPricing =>
                {
                    _caseService.UpdateCaseTreatmentPricingPriceByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(caseTreatmentPricing));
                });
            }


            if (caseCompleteInvoicingViewModel.CaseBespokeServicePricingType != null)
            {
                caseCompleteInvoicingViewModel.CaseBespokeServicePricingType.ToList().ForEach(caseBespokeServicePricing =>
                {
                    _caseService.UpdateCaseBespokeReferrerPriceByCaseBespokeServiceID(caseBespokeServicePricing.CaseBespokeServiceID, caseBespokeServicePricing.ReferrerPrice);
                });
            }

            decimal totalAmountOut;
            string invoiceNumberOut;
            caseCommunicationHistory.UserID = ITSUser.UserID;
            ITS.Domain.Models.CaseModel.CasePatientReferrer casePatientReferrer = Mapper.Map<ITS.Domain.Models.CaseModel.CasePatientReferrer>(_caseService.GetCasePatientReferrerByCaseID(caseID));
            decimal vat = caseCompleteInvoicingViewModel.ReferrerAndSupplierVATPricing.ReferrerPrice.Value;

            MemoryStream memmoryStreamObj = new MemoryStream(_letterGeneration.GenerateInvoice(casePatientReferrer, caseCompleteInvoicingViewModel.CaseTreatmentPricingType, caseCompleteInvoicingViewModel.CaseBespokeServicePricingType, vat, Server.MapPath("~/Content/Images/innovate-logo.jpg"),
                               out totalAmountOut, out invoiceNumberOut));
            
            if (caseCommunicationHistory.NotifyReferrer)
            {
                string fileName = string.Format("{0}.pdf", /*Guid.NewGuid().ToString()*/"Invoice");
                bool result = _emailService.SendGeneralEmail(caseCommunicationHistory.SentTo, caseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), caseCommunicationHistory.Subject, caseCommunicationHistory.Message,
                        new System.Net.Mail.Attachment(memmoryStreamObj, fileName));
                if (result)
                    _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(caseCommunicationHistory));
            }

            if (caseCompleteInvoicingViewModel.ReferrerAndSupplierVATPricing != null && caseCompleteInvoicingViewModel.ReferrerAndSupplierVATPricing.ReferrerPrice > 0)
            {
                CaseVAT caseVat = new CaseVAT
                {
                    CaseID = caseID,
                    VAT = caseCompleteInvoicingViewModel.ReferrerAndSupplierVATPricing.ReferrerPrice.Value,
                };
                _caseService.AddCaseVAT((Mapper.Map<ITSService.CaseService.CaseVAT>(caseVat)));

            }
            _caseService.UpdateCaseInvoiceDate(DateTime.Now, caseID);
            ITS.Domain.Models.InvoiceModel.Invoice invoice = new ITS.Domain.Models.InvoiceModel.Invoice() { InvoiceDate = DateTime.Now, Amount = totalAmountOut, CaseID = caseID, InvoiceNumber = invoiceNumberOut, UserID = userID };


            _financeService.AddInvoice(Mapper.Map<ITSService.FinanceService.Invoice>(invoice));
            _caseService.UpdateCaseWorkFlow(caseID, userID);

            return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
        }
        #endregion
    }
}
