using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.SupplierService;
using Model = ITSPublicApp.Domain.Models;
using CaseDTO = ITSPublicApp.Web.ITSService.CaseService;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    public class ExistingPatientsReviewAssessmentController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ICaseService _caseService;
        private readonly IReferrerStorage _referrerStorage;

        public ExistingPatientsReviewAssessmentController(ISupplierService supplierService, ICaseService caseService, IReferrerStorage referrerStorage)
        {
            _supplierService = supplierService;
            _caseService = caseService;
            _referrerStorage = referrerStorage;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetInitialAssessments()
        {
            var supplierID = ITSUser.SupplierID.Value;
            return Json(Mapper.Map<IEnumerable<Model.SupplierCasePatient>>(_supplierService.GetSupplierExistingPatientsInitialAssessments(supplierID)));
        }

        [HttpPost]
        public ActionResult UpdateCase(Model.Case caseObj)
        {
            int userID = ITSUser.UserID;
            return Json(_caseService.UpdateCaseWorkFlow(caseObj.CaseID, userID));
        }

        
    }
}
