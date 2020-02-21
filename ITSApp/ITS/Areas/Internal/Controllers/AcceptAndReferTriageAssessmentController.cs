using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.Base;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class AcceptAndReferTriageAssessmentController : BaseController
    {
        //
        // GET: /Internal/AcceptAndReferTriageAssessment/
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.CaseService.ICaseService _caseService;

        public AcceptAndReferTriageAssessmentController(ITSService.PatientService.IPatientService patientService, ITSService.SupplierService.ISupplierService supplierService, ITSService.CaseService.ICaseService caseService)
        {
            _patientService = patientService;
            _supplierService = supplierService;
            _caseService = caseService;

        }

        public ActionResult Index(int id)
        {
            int caseID = id;

            CasePatientTreatment caseTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID));
            caseTreatment.ReferralFileDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = caseID });

            IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> suppliers = Mapper.Map<IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageTopSuppliersTreamentPricingByTreatmentCategoryID(caseTreatment.TreatmentCategoryID));

            var viewModel = new PatientCaseAndSupplierWithPatientPostCodeViewModel
            {
                CaseID = caseID,
                CasePatientTreatment = caseTreatment,
                SupplierSupplierTreatmentsAndSupplierTreatmenPricing = suppliers.OrderBy(supplier => supplier.Price).ThenByDescending(supplier => supplier.Ranking),

            };
            return View("~/Areas/Internal/Views/AcceptAndReferScreen/AcceptAndReferTriageAssessment.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetSuppliersTriageTreamentPricingByTreatmentCategoryID(int treatmentID)
        {
            IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> supplierSupplierTreatmentsAndSupplierTreatmenPricing = Mapper.Map<IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageSuppliersTreamentPricingByTreatmentCategoryID(treatmentID));
            return Json(supplierSupplierTreatmentsAndSupplierTreatmenPricing);
        }

        [HttpPost]
        public JsonResult SubmitTriageCaseToSupplier(int caseID, int supplierID)
        {
            if (_caseService.UpdateCaseSupplier(caseID, supplierID) > 0)
            {
               if (_caseService.UpdateCaseWorkFlow(caseID, ITSUser.UserID))
                    return Json("supplier successfully attached to case");
            }
            return Json("error while updating suppler to case");
        }
        [HttpPost]
        public JsonResult SubmitTriageCaseToSupplierChange(int caseID, int supplierID)
        {
            if (_caseService.UpdateCaseSupplier(caseID, supplierID) > 0)
            {
                  return Json("supplier successfully attached to case");
            }
            return Json("error while updating suppler to case");
        }

    }
}
