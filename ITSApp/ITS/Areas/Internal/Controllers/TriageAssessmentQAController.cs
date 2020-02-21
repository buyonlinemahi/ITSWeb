using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationServices.Contracts;
using AutoMapper;
using ViewModel = ITS.Domain.ViewModels;
using ITS.Infrastructure.Global;
using ITS.Domain.Models;
using ITS.Domain.ViewModels;
using System.Configuration;
using System.Web;
using System.IO;
using System;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class TriageAssessmentQAController : BaseController
    {
        //
        // GET: /Internal/ReferralReceived/
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.PatientService.IPatientService _patientService;

        private readonly ISupplierStorage _supplierStorageService;


        public TriageAssessmentQAController(ITSService.CaseService.ICaseService caseService, ITSService.PatientService.IPatientService patientService, ISupplierStorage supplierStorageService)
        {
            _caseService = caseService;
            _patientService = patientService;
            _supplierStorageService = supplierStorageService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Internal/Views/Shared/MainScreen/_TriageAssessmentQAPartial.cshtml");
        }


        public ActionResult GetTriageAssessmentCases(int skip, int take)
        {
            var pagedCaseWorkflowPatientReferrerProject = Mapper.Map<ViewModel.PagedCaseWorkflowPatientReferrerProject>(_caseService.GetTriageAssessmentQACaseWorkflowPatientProjects(skip, take));

            return Json(pagedCaseWorkflowPatientReferrerProject);
        }

        [HttpPost]
        public JsonResult GetTriageAssessmentCasesByTreatmentCategoryID(int treatmentCatagoryID, int skip, int take)
        {
            ViewModel.PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForRefReceived = Mapper.Map<ViewModel.PagedCaseWorkflowPatientReferrerProject>(_caseService.GetTriageAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCatagoryID, skip, take));

            return Json(caseWorkflowPatientReferrerProjectsForRefReceived);
        }

        public ActionResult GetTriageAssessmentByCaseID(int id)
        {
            int caseID = id;

            var viewModel = new TriageAssessmentQAViewModel
                 {
                     CasePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id))
                 };
            viewModel.CasePatientTreatment.CaseID = id;

            return View("~/Areas/Internal/Views/TriageAssessmentQA/TriageAssessmentQA.cshtml", viewModel);
        }


        public ActionResult UploadAssessment(TriageAssessmentQAViewModel viewModel, bool IsTreatmentRequired)
        {
            if (viewModel.TriageAssessmentUploadFile != null)
            {
                if (System.IO.File.Exists(_supplierStorageService.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), viewModel.CasePatientTreatment.SupplierID.Value,
                    (_caseService.GetTriageCaseDocumentByCaseID(viewModel.CasePatientTreatment.CaseID)).UploadPath.ToString(), GlobalConst.SupplierDocumentType.Triage)))
                {
                    System.IO.File.Delete(_supplierStorageService.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), viewModel.CasePatientTreatment.SupplierID.Value,
                        (_caseService.GetTriageCaseDocumentByCaseID(viewModel.CasePatientTreatment.CaseID)).UploadPath.ToString(), GlobalConst.SupplierDocumentType.Triage));
                }
                _caseService.UpdateTriageCaseDocumentByCaseID(new ITSService.CaseService.CaseDocument
                {
                    CaseID = viewModel.CasePatientTreatment.CaseID,
                    DocumentTypeID = (int)GlobalConst.SupplierDocumentTypeId.Triage,
                    UploadDate = DateTime.Now,
                    DocumentName = viewModel.CasePatientTreatment.CaseNumber.ToString(),
                    UploadPath = UpdateDocumentFile(viewModel.TriageAssessmentUploadFile, viewModel.CasePatientTreatment.SupplierID.Value)
                });
            }
            _caseService.UpdateCaseIsTreatmentRequired(viewModel.CasePatientTreatment.CaseID, IsTreatmentRequired);
            _caseService.UpdateCaseWorkFlow(viewModel.CasePatientTreatment.CaseID, ITSUser.UserID);

            viewModel.TriageAssessmentUploadFile = null;
            return Json(viewModel, "text/html");
        }

        private string UpdateDocumentFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorageService.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.Triage);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }

    }
}
