using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Domain.Models.CaseAssessmentModel;
using System.Configuration;
using System.IO;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class TriageAssessmentController : BaseController
    {
  
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.PatientService.IPatientService _patientService;

        private readonly ISupplierStorage _supplierStorageService;

        public TriageAssessmentController(ITSService.CaseService.ICaseService caseService, ITSService.PatientService.IPatientService patientService, ISupplierStorage supplierStorageService)
        {
            _caseService = caseService;
            _patientService = patientService;
            _supplierStorageService = supplierStorageService;
        }

        public ActionResult Index()
        {


            return View(GetTriageAssessmentQACaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
       
        }

        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetTriageAssessmentQACaseWorkflowPatientProjects(int skip, int take)
        {
            var pagedCaseWorkflowPatientTriageAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
           (_caseService.GetTriageAssessmentQACaseWorkflowPatientProjects(skip, take));
            pagedCaseWorkflowPatientTriageAssessment.CaseWorkflowPatientReferrerProjects.ToList().ForEach(triageAssessmentObj =>
            {
                triageAssessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.TriageAssessmentController.TriageAssessmentQA, GlobalConst.Controllers.TriageAssessment, new { id = triageAssessmentObj.CaseID });
            });

            pagedCaseWorkflowPatientTriageAssessment.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return pagedCaseWorkflowPatientTriageAssessment;
        }

        public ActionResult TriageAssessmentQA(int id)
        {
            var viewModel = new ITS.Domain.ViewModels.InternalTasksViewModel.TriageAssessmentQAViewModel
            {
                CasePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id))
            };
            viewModel.CasePatientTreatment.CaseID = id;

            return View(viewModel);

        }

        #region TriageAssessment Post Method Section
        [HttpPost]
        public ActionResult TriageAssessment(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject viewModel;

            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel = GetTriageAssessmentQACaseWorkflowPatientProjects(skip, take);
            }
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetTriageAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(triageAssessmentObj =>
                {
                    triageAssessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.TriageAssessmentController.TriageAssessmentQA, GlobalConst.Controllers.TriageAssessment, new { id = triageAssessmentObj.CaseID });

                });

                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }

        [HttpPost]
        public ActionResult UpdateTriageAssessmentQA(TriageAssessmentQAViewModel viewModel, bool IsTreatmentRequired)
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
                    UserID = ITSUser.UserID,
                    UploadPath = UpdateDocumentFile(viewModel.TriageAssessmentUploadFile, viewModel.CasePatientTreatment.SupplierID.Value)
                });
            }
            _caseService.UpdateCaseIsTreatmentRequired(viewModel.CasePatientTreatment.CaseID, IsTreatmentRequired);
            _caseService.UpdateCaseWorkFlow(viewModel.CasePatientTreatment.CaseID, ITSUser.UserID);

            viewModel.TriageAssessmentUploadFile = null;

            return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
          

        }

        private string UpdateDocumentFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorageService.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.Triage);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }


        #endregion

    }
}
