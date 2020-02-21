using System.Collections.Generic;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class AuthorisationController : Controller
    {
        //
        // GET: /Internal/Authorisation/
        private readonly ITSService.CaseService.ICaseService _caseService;

        public AuthorisationController(ITSService.CaseService.ICaseService caseService)
        {
            _caseService = caseService;

        }
        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_AuthorisationPartial.cshtml");
        }

        [HttpPost]
        public JsonResult GetAuthorisationCaseWorkflowPatientProjects(int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForAuthorisation = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetAuthorisationCaseWorkflowPatientProjects(skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForAuthorisation);
        }

        [HttpPost]
        public JsonResult GetAuthorisationCaseWorkflowPatientProjectsByTreatmentCategoryID(int treatmentCatagoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForAuthorisation = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetAuthorisationCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCatagoryID, skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForAuthorisation);
        }


    }
}
