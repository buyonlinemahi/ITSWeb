using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Domain.Models.PractitionerModel;
using ITS.Infrastructure.ApplicationServices;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Net;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class FinalAssessmentsController : BaseController
    {
        //
        // GET: /FinalAssessment/
        private readonly ITSService.CaseService.ICaseService _caseService;

        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.PractitionerService.IPractitionerService _PractitionerService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly EmailService _emailService;
        private readonly ISupplierStorage _supplierStorageService;

        public FinalAssessmentsController(ISupplierStorage supplierStorageService,ITSService.SupplierService.ISupplierService supplierService,EmailService emailService,ITSService.CaseService.ICaseService caseService, ITSService.AssessmentService.IAssessmentService assessmentService,
            ITSService.LookUpService.ILookUpService lookUpService, ITSService.PatientService.IPatientService patientService, ITSService.PractitionerService.IPractitionerService practitionerService)
        {
            _caseService = caseService;
            _lookUpService = lookUpService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _PractitionerService = practitionerService;
            _emailService = emailService;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
        }

        public ActionResult Index()
        {

            return View(GetFinalAssessmentCaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }

        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetFinalAssessmentCaseWorkflowPatientProjects(int skip, int take)
        {
           
            var pagedCaseWorkflowPatientFinalAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
               (_caseService.GetFinalAssessmentCaseWorkflowPatientProjects(skip, take));
            

            pagedCaseWorkflowPatientFinalAssessment.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
            {
                if (assessmentObj.WorkflowID == 160)
                {
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.FinalAssessmentController.FinalAssessmentQA, GlobalConst.Controllers.FinalAssessment, new
                    {
                        id = assessmentObj.CaseID
                    });
                }
                else
                {
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.FinalAssessmentController.FinalAssessmentQACustom, GlobalConst.Controllers.FinalAssessment, new
                    {
                        id = assessmentObj.CaseID
                    });
                }

            });

            pagedCaseWorkflowPatientFinalAssessment.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return pagedCaseWorkflowPatientFinalAssessment;
        }

        public ActionResult FinalAssessmentQA(int id)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));

            var referrerProjectTreatmentID = caseObj.ReferrerProjectTreatmentID;


            var viewModel = new FinalAssessmentQAViewModel
            {
                Patient = new ITS.Domain.Models.PatientModel.Patient
                {
                    FirstName = casePatientTreatment.FirstName,
                    LastName = casePatientTreatment.LastName,
                    BirthDate = casePatientTreatment.BirthDate,
                    InjuryDate = casePatientTreatment.InjuryDate
                },
                Case = new ITS.Domain.Models.CaseModel.Case
                {
                    CaseSubmittedDate = casePatientTreatment.CaseSubmittedDate,
                    CaseID = id
                },
                Practitioners = Mapper.Map<IEnumerable<Practitioner>>(_PractitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(caseObj.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentQA(id)),
                caseAppointmentDate = Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(id)),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                Durations = Mapper.Map<IEnumerable<Duration>>(_lookUpService.GetAllDuration()),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                TreatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID)),
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),
                AffectedAreas = Mapper.Map<IEnumerable<AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<SymptomDescription>>(_lookUpService.GetAllSymptomDescription()),
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes())
            };

            var _treatmentSessionByCaseID = _caseService.GetTreatmentSessionByCaseID(id);
            //viewModel.CaseAssessment.CaseAssessmentDetail.SessionsPatientAttended = _treatmentSessionByCaseID.AllTreatmentSessions;
            //viewModel.CaseAssessment.CaseAssessmentDetail.DatesOfSessionAttended = _treatmentSessionByCaseID.ShowAllAttendedSessionsDateOfService;
            //viewModel.CaseAssessment.CaseAssessmentDetail.SessionsPatientFailedToAttend = _treatmentSessionByCaseID.AllFailedTreatmentSessions;

            viewModel.caseAppointmentDate.CaseBookIADate = DateTime.Parse(viewModel.caseAppointmentDate.strAppointmentDate);
            return View(viewModel);
        }


        public ActionResult FinalAssessmentQACustom(int? id)
        {
            int CaseId = id.Value;
            FinalAssessmentQACustomViewModel viewmodel = new FinalAssessmentQACustomViewModel();
            viewmodel.casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(CaseId));
            viewmodel.CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentQA(CaseId));
            viewmodel.caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(CaseId));
            //string storagePath = ConfigurationManager.AppSettings["ITSPublicAppStorage"];
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            viewmodel.supplierDocument = Mapper.Map<List<ITS.Domain.Models.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(CaseId, (int)GlobalConst.CaseDocumentTypeId.FinalAssessmentSupplierCustom));
            viewmodel.FinalAssesessmentCustomFilePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewmodel.supplierDocument[0].SupplierID, (int)viewmodel.supplierDocument[0].ReferrerProjectTreatmentID, (int)viewmodel.supplierDocument[0].CaseId, viewmodel.supplierDocument[0].UploadPath, storagePath);
            viewmodel.CaseAppointmentDate = Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)id));
            return View(viewmodel);
        }
        #region Initial Assessment Post Method Section
        [HttpPost]
        public ActionResult FinalAssessment(int treatmentCategoryID, int skip, int take)
        {

            PagedCaseWorkflowPatientReferrerProject viewModel;
            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel = GetFinalAssessmentCaseWorkflowPatientProjects(skip, take);
            }
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetFinalAssessmentCaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
                {
                    if (assessmentObj.WorkflowID == 160)
                    {
                        assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.FinalAssessmentController.FinalAssessmentQA, GlobalConst.Controllers.FinalAssessment, new
                        {
                            id = assessmentObj.CaseID
                        });
                    }
                    else
                    {
                        assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.FinalAssessmentController.FinalAssessmentQACustom, GlobalConst.Controllers.FinalAssessment, new
                        {
                            id = assessmentObj.CaseID
                        });
                    }

                });
                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }

            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFinalAssessmentQA(FinalAssessmentQAViewModel viewModel)
        {
            viewModel.caseAppointmentDate.CaseID = viewModel.CaseAssessment.CaseID;
            _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.caseAppointmentDate));
            viewModel.CaseAssessment.UserID = ITSUser.UserID;
            if (viewModel.CaseAssessment.IsAccepted)
            {
                string filename = null;
                if (viewModel.CaseAssessmentDocument.FinalVersionFileUpload != null)
                {
                    filename = AddCaseUploads(viewModel.CaseAssessmentDocument);                
                }

                viewModel.CaseAssessment.DeniedMessage = null;
                if (viewModel.CaseCommunicationHistory.NotifyReferrer)
                {                    
                    Attachment obj = null;
                    if (viewModel.CaseAssessmentDocument.FinalVersionFileUpload != null)
                    {                        
                        obj = new System.Net.Mail.Attachment(Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, viewModel.CaseAssessmentDocument.UploadPath)));
                    }
                    else
                    {
                        filename = Guid.NewGuid().ToString() + "." + GlobalConst.ReportFormatExt.doc;
                        obj = GenerateReport(viewModel.CaseAssessment.CaseID, filename);
                    }


                    viewModel.CaseCommunicationHistory.UserID = ITSUser.UserID;
                    viewModel.CaseCommunicationHistory.UploadPath = filename;
                    bool result = _emailService.SendGeneralEmail(viewModel.CaseCommunicationHistory.SentTo, viewModel.CaseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), viewModel.CaseCommunicationHistory.Subject, viewModel.CaseCommunicationHistory.Message, obj);
                    if (result)
                        _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(viewModel.CaseCommunicationHistory));
                }
            }
            viewModel.CaseAssessment.CaseAssessmentRating.AssessmentServiceID = viewModel.CaseAssessment.AssessmentServiceID;
            viewModel.CaseAssessment.CaseAssessmentRating.RatingDate = DateTime.Now;
            viewModel.CaseAssessment.CaseAssessmentDetail.AssessmentDate = DateTime.Now;
            _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            _assessmentService.UpdateCaseAssessmentIsSavedByCaseID(viewModel.CaseAssessment.CaseID); 
            return Json("success", GlobalConst.ContentTypes.TextHTML);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFinalAssessmentQACustom(FinalAssessmentQACustomViewModel viewModel)
        {
            
            
            _assessmentService.UpdateCaseFinalAssessmentMessageCustom(Mapper.Map<ITSService.AssessmentService.CaseAssessmentCustom>(viewModel.caseAssessmentCustom));
            viewModel.CaseAppointmentDate.CaseID = viewModel.caseObj.CaseID;
            _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.CaseAppointmentDate));
            viewModel.caseAssessmentRating.AssessmentServiceID = GlobalConst.AssessmentService.FinalAssessment;
            viewModel.caseAssessmentRating.RatingDate = DateTime.Now;
            ITS.Domain.Models.CaseAssessmentRating caseAssessmentRating = Mapper.Map<ITS.Domain.Models.CaseAssessmentRating>(_assessmentService.GetCaseAssessmentRatingByCaseIDAndAssessmentServiceID(viewModel.caseObj.CaseID, GlobalConst.AssessmentService.FinalAssessment));
            if (caseAssessmentRating == null)
                _assessmentService.AddCaseAssessmentRating(Mapper.Map<ITSService.AssessmentService.CaseAssessmentRating>(viewModel.caseAssessmentRating));
            else
                _assessmentService.UpdateCaseAssessmentRatingByCaseIDAndAssessmentServiceID(viewModel.caseObj.CaseID, GlobalConst.AssessmentService.FinalAssessment,viewModel.caseAssessmentRating.Rating);

            
            if (viewModel.caseAssessmentCustom.isAccepted)
            {
                string filestoragePath = "";
                string UploaedDocmentName = "";
                if (viewModel.FinalAssessmentFileFinal != null)
                {
                    UploaedDocmentName = (string)GlobalConst.SupplierDocumentType.FinalAssessmentFinalCustom.ToString() + Path.GetExtension(viewModel.FinalAssessmentFileFinal.FileName).ToString();
                    //string storagePath = GlobalMethod.GetVirtualPublicAppPath();
                    string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                    viewModel.supplierCustom.UserID = ITSUser.UserID;
                    viewModel.supplierCustom.DocumentName = (string)GlobalConst.SupplierDocumentType.FinalAssessmentFinalCustom;
                    viewModel.supplierCustom.UploadPath = UploaedDocmentName;
                    viewModel.supplierCustom.UploadDate = DateTime.Now;
                    viewModel.supplierCustom.DocumentTypeID = (int)GlobalConst.CaseDocumentTypeId.FinalAssessmentFinalCustom;
                    int inerted = _supplierService.AddSupplierDocumentCustom(Mapper.Map<ITSService.SupplierService.SupplierDocument>(viewModel.supplierCustom));
                    filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewModel.supplierCustom.SupplierID, (int)viewModel.supplierCustom.ReferrerProjectTreatmentID, (int)viewModel.supplierCustom.CaseId, UploaedDocmentName, storagePath);
                    viewModel.FinalAssessmentFileFinal.SaveAs(filestoragePath);
                }

                viewModel.caseAssessmentCustom.FinalAssessmentMessage = "";
                 
                Attachment obj = null;
                if (filestoragePath != null)
                    obj = new System.Net.Mail.Attachment(filestoragePath);
                if (viewModel.CaseCommunicationHistory.NotifyReferrer)
                {
                    viewModel.CaseCommunicationHistory.UserID = ITSUser.UserID;
                    viewModel.CaseCommunicationHistory.UploadPath = UploaedDocmentName;
                    bool result = _emailService.SendGeneralEmail(viewModel.CaseCommunicationHistory.SentTo, viewModel.CaseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), viewModel.CaseCommunicationHistory.Subject, viewModel.CaseCommunicationHistory.Message, obj);
                    if (result)
                        _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(viewModel.CaseCommunicationHistory));
                }


                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseObj.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoReferrerCustom);
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseObj.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.CaseCompleted);
            }
            else
            {
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseObj.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.FinalAssessmentReportNotAcceptedCustom);
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseObj.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
            }
            return Json("success", GlobalConst.ContentTypes.TextHTML);
        }

        private Attachment GenerateReport(int CaseID, string filename)
        {
            Attachment objAttachment = null;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], CaseID, GlobalConst.DocumentType.FinalAssessment, GlobalConst.ReportFormat.Word);
            client.DownloadFile(reportURL, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            objAttachment = new System.Net.Mail.Attachment(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            return objAttachment;
        }

        [NonAction]
        private string AddCaseUploads(CaseAssessmentDocument caseAssessmentDocument)
        {
            string filename = AddDocumentFile(caseAssessmentDocument.FinalVersionFileUpload);
            caseAssessmentDocument.UserID = ITSUser.UserID;
            caseAssessmentDocument.DocumentTypeID = GlobalConst.CaseDocumentTypeId.FinalAssessment;
            caseAssessmentDocument.UploadDate = DateTime.Now;
            caseAssessmentDocument.UploadPath = filename;
            _caseService.AddCaseDocument(Mapper.Map<ITSService.CaseService.CaseDocument>(caseAssessmentDocument));
            return filename;
        }

        [NonAction]
        private string AddDocumentFile(HttpPostedFileBase file)
        {
            if (!Directory.Exists(Server.MapPath(GlobalConst.CaseDocumentStoragePath)))
            {
                Directory.CreateDirectory(Server.MapPath(GlobalConst.CaseDocumentStoragePath));
            }
            var path = Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName)));
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }

        #endregion
    }
}
