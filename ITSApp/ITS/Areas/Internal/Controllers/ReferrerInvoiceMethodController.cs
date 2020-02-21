using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;

/*
================================================
Latest  : Version 1.0
Author  : Harpreet Singh
Created On : 29-Jan-2013
Purpous :created saprate controller for invoice method tab and added the methods from referrersetupcontroller to invoicemethodcontroller  
Version : 1.0
================================================
*/

namespace ITSWebApp.Areas.Internal.Controllers
{
    using ITS.Infrastructure.Global;

    public class ReferrerInvoiceMethodController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public ReferrerInvoiceMethodController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }
        

        #region Invoice Methods Implementations

        [HttpPost]
        public JsonResult GetAllInvoiceMethodByReferrerProjectTreatmentID(int referrerProjectTreatmentID)
        {
            ReferrerProjectTreatmentInvoice refrrerInvoiceMethod = Mapper.Map<ReferrerProjectTreatmentInvoice>(_referrerService.GetReferrerProjectTreatmentInvoiceByReferrerProjectTreatmentID(referrerProjectTreatmentID).SingleOrDefault());

            IEnumerable<InvoiceMethod> InvoiceMethods = Mapper.Map<IEnumerable<ITS.Domain.Models.InvoiceMethod>>(_referrerService.GetAllInvoiceMethod());

            refrrerInvoiceMethod.ReferrerInvoiceMethodTreatment = InvoiceMethods;

            return Json(refrrerInvoiceMethod);

        }
        [HttpPost]
        public JsonResult UpdateInvoiceMethodByReferrerProjectTreatmentID(IEnumerable<ReferrerProjectTreatmentInvoice> referrerProjectTreatmentInvoices)
        {
            foreach (ReferrerProjectTreatmentInvoice referrerProjectTreatmentInvoice in referrerProjectTreatmentInvoices)
            {
                _referrerService.UpdateReferrerProjectTreatmentInvoice(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentInvoice>(referrerProjectTreatmentInvoice));
            }

            return Json(GlobalResource.UpdatedSuccessfully);
        }
        #endregion
    }
}
