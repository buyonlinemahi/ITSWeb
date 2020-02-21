using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.Base;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class AcceptAndReferController : BaseController
    {
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly IReferrerStorage _referrerStorage;

        public AcceptAndReferController(ITSService.PatientService.IPatientService patientService, ITSService.SupplierService.ISupplierService supplierService, ITSService.CaseService.ICaseService caseService,
            IReferrerStorage referrerStorage)
        {
            _patientService = patientService;
            _supplierService = supplierService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
        }

        public ActionResult Index(int id)
        {
            int caseID = id;
            CasePatientTreatment caseTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID));
            caseTreatment.ReferralFileDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = caseID });

            IEnumerable<SupplierDistanceRankPrice> suppliers = Mapper.Map<IEnumerable<SupplierDistanceRankPrice>>(_supplierService.GetSupplierWithinPostCode(caseTreatment.PostCode, 10, caseTreatment.TreatmentCategoryID));

            if (suppliers == null)
                suppliers = new List<SupplierDistanceRankPrice>();
            else
                suppliers = suppliers.OrderBy(supplier => supplier.Distance).ThenByDescending(supplier => supplier.Ranking);

            var viewModel = new PatientCaseAndSupplierWithPatientPostCodeViewModel
            {
                CaseID=caseID,
                CasePatientTreatment = caseTreatment,
                SupplierDistanceRankPrice = suppliers//suppliers.OrderBy(supplier => supplier.Distance).ThenByDescending(supplier => supplier.Ranking)
            };

            return View("~/Areas/Internal/Views/AcceptAndReferScreen/AcceptAndRefer.cshtml", viewModel);

        }

        [HttpPost]
        public JsonResult SubmitCaseToSupplier(int caseID, int supplierID)
        {
            if (_caseService.UpdateCaseSupplier(caseID, supplierID) > 0)
            {
                if (_caseService.UpdateCaseWorkFlow(caseID, ITSUser.UserID))
                    return Json("supplier successfully attached to case");
            }
            return Json("error while updating suppler to case");
        }


    }
}
