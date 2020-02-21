using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.InvoiceViewModel;
using AutoMapper;
using ITS.Domain.Models.InvoiceModel;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class InvoiceController : BaseController
    {

        private readonly ITSService.FinanceService.IFinanceService _financeService;

        public InvoiceController(ITSService.FinanceService.IFinanceService financeService)
        {
            _financeService = financeService;

        }
        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            return View(GetOutstandingInvoices((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));

        }

        [NonAction]
        public PagedCaseInvoicePatientReferrer GetOutstandingInvoices(int skip, int take)
        {
            var pagedCaseWorkflowPatientReferrerProject = Mapper.Map<PagedCaseInvoicePatientReferrer>(_financeService.GetOutstandingInvoicesCasePatientReferrer(skip, take));
            foreach (var item in pagedCaseWorkflowPatientReferrerProject.caseInvoicePatientReferrer)
            {
                item.ActionUrl = Url.Action(GlobalConst.Actions.InvoiceController.InvoiceDetail, GlobalConst.Controllers.Invoice, new { id = item.InvoiceID });
            }
            return pagedCaseWorkflowPatientReferrerProject;
        }


        [HttpPost]
        public ActionResult Index(int skip, int take)
        {
            PagedCaseInvoicePatientReferrer viewModel;
            viewModel = GetOutstandingInvoices(skip, take);
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }



        [HttpGet]
        public ActionResult InvoiceDetail(int id)
        {
            var InvoiceDetailViewModel = new InvoiceDetailViewModel()
            {
                CaseInvoicePatientReferrer = Mapper.Map<CaseInvoicePatientReferrer>(_financeService.GetOutstandingInvoicesCasePatientReferrerByInvoiceID(id)),
                InvoiceCollectionActionHistory = Mapper.Map<IEnumerable<InvoiceCollectionActionUserName>>(_financeService.GetInvoiceCollectionActionByInvoiceID(id)),
                InvoicePaymentHistory = Mapper.Map<IEnumerable<InvoicePaymentUserName>>(_financeService.GetInvoicePaymentByInvoiceID(id))

            };
            return View(InvoiceDetailViewModel);
        }

        [HttpPost]
        public ActionResult AddPaymentAmount(InvoicePaymentUserName invoicePayment)
        {
            invoicePayment.InvoicePaymentDate = DateTime.Now;
            invoicePayment.UserID = ITSUser.UserID;
            invoicePayment.UserName = ITSUser.UserName;
            if (_financeService.AddInvoicePayment(Mapper.Map<ITSService.FinanceService.InvoicePayment>(invoicePayment)) != -1)
            {
                return Json(invoicePayment, GlobalConst.ContentTypes.TextHTML);
            }
            else
                return Json(-1, GlobalConst.ContentTypes.TextHTML);
        }

        [HttpPost]
        public ActionResult AddCollectionAction(InvoiceCollectionActionUserName invoiceCollectionAction)
        {
            invoiceCollectionAction.DateofAction = DateTime.Now;
            invoiceCollectionAction.UserID = ITSUser.UserID;
            invoiceCollectionAction.UserName = ITSUser.UserName;
            if (_financeService.AddInvoiceCollectionAction(Mapper.Map<ITSService.FinanceService.InvoiceCollectionAction>(invoiceCollectionAction)) != -1)
            {
                return Json(invoiceCollectionAction, GlobalConst.ContentTypes.TextHTML);
            }
            else
                return Json(-1, GlobalConst.ContentTypes.TextHTML);
        }

    }
}
