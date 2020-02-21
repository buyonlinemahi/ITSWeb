using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class TriageAssessmentController : BaseController
    {
        private readonly IPatientService _patientService;
        private readonly ICaseService _caseService;
        private readonly IEncryption _encryptionService;
        private readonly ISupplierStorage _supplierStorageService;

        public TriageAssessmentController(ICaseService caseService, IPatientService patientService, ISupplierStorage supplierStorageService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _patientService = patientService;
            _supplierStorageService = supplierStorageService;
            _encryptionService = encryptionService;

        }
        [HttpGet]
        public ActionResult Index(string id)
        {
            int _id = Convert.ToInt16(DecryptString(id.ToString()));
            TriageAssessmentViewModel triageAssessmentViewModel = new TriageAssessmentViewModel
                {
                    CasePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(_id))
                };
            triageAssessmentViewModel.CasePatientTreatment.CaseID = _id;
            return View(triageAssessmentViewModel);
        }

        public ActionResult Save(TriageAssessmentViewModel viewModel)
        {
            int supplierID = ITSUser.SupplierID.Value;
            int userID = ITSUser.UserID;
            int CaseID = viewModel.CasePatientTreatment.CaseID;
            if (viewModel.TriageAssessmentUploadFile != null)
            {
                string filename = AddDocumentFile(viewModel.TriageAssessmentUploadFile, supplierID);

                _caseService.AddCaseDocument(new ITSService.CaseService.CaseDocument
                {
                    CaseID = CaseID,
                    DocumentTypeID = (int)GlobalConst.SupplierDocumentTypeID.Triage,
                    UploadDate = DateTime.Now,
                    DocumentName = GlobalConst.TriageAssessment,
                    UploadPath = filename,
                    UserID = userID
                });
            }
            _caseService.UpdateCaseWorkFlow(CaseID, userID);

            viewModel.TriageAssessmentUploadFile = null;
            return Json(viewModel, "text/html");
        }

        private string AddDocumentFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorageService.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.Triage);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
    }
}
