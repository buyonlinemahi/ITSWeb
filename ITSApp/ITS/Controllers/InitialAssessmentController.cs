using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Infrastructure.Base;
using ITS.Domain.Models.PractitionerModel;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.ApplicationServices;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using ITSModel = ITS.Domain.Models;
using System.Net;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class InitialAssessmentController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService; 
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.PractitionerService.IPractitionerService _PractitionerService;
        private readonly EmailService _emailService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITS.Infrastructure.ApplicationServices.ReferrerStorageService _referrerStorage;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITS.Domain.Models.SupplierDocument supplierDocument;
        private readonly ISupplierStorage _supplierStorageService;

        public InitialAssessmentController(ITSService.CaseService.ICaseService caseService,
            ITSService.AssessmentService.IAssessmentService assessmentService,
            ITSService.LookUpService.ILookUpService lookUpService,
            ITSService.PatientService.IPatientService patientService,
            EmailService emailService,
            ITSService.PractitionerService.IPractitionerService practitionerService,
            ITSService.ReferrerService.IReferrerService referrerService,
            ITS.Infrastructure.ApplicationServices.ReferrerStorageService referrerStorage,
            ITSService.SupplierService.ISupplierService supplierService,
            ISupplierStorage supplierStorageService,
            ITS.Domain.Models.SupplierDocument _supplierDocument)
        {
            _caseService = caseService;
            _lookUpService = lookUpService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _PractitionerService = practitionerService;
            _emailService = emailService;
            _referrerService = referrerService;
            _referrerStorage = referrerStorage;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            supplierDocument = _supplierDocument;
        }
        public ActionResult Index()
        {
            return View(GetInitialAssessmentQACaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }
        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetInitialAssessmentQACaseWorkflowPatientProjects(int skip, int take)
        {
            var pagedCaseWorkflowPatientInitialAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
                 (_caseService.GetInitialAssessmentQACaseWorkflowPatientProjects(skip, take));
            pagedCaseWorkflowPatientInitialAssessment.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
            {
                if (assessmentObj.WorkflowID == 70)
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.InitialAssessmentController.InitialAssessmentQA, GlobalConst.Controllers.InitialAssessment, new { id = assessmentObj.CaseID });
                else
                {
                    if (assessmentObj.WorkflowID == 72)
                        assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.InitialAssessmentController.InitialAssessmentQACustom, GlobalConst.Controllers.InitialAssessment, new { id = assessmentObj.CaseID });
                }
            });
            pagedCaseWorkflowPatientInitialAssessment.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return pagedCaseWorkflowPatientInitialAssessment;
        }
        public ActionResult InitialAssessmentQA(int id)
        {
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var treatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID));
            var caseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id));
            var caseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(id));
            //for adding initial assessment price as default in case treatment pricing grid.
            if (caseTreatmentPricings.Count() == 0)
            {
                var caseTreatmentPricing = treatmentReferrerSupplierPricing.FirstOrDefault(i => i.PricingTypeName == "Initial Assessment");
                List<CaseTreatmentPricing> lst = new List<CaseTreatmentPricing>() { };
                lst.Add(new CaseTreatmentPricing { CaseID = caseObj.CaseID, ReferrerPrice = caseTreatmentPricing.ReferrerPrice, ReferrerPricingID = caseTreatmentPricing.ReferrerPricingID, SupplierPrice = caseTreatmentPricing.SupplierPrice, Quantity = 0, SupplierPriceID = caseTreatmentPricing.SupplierPriceID, AuthorizationStatus = true });
                _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(lst).ToArray());
            }
            var viewModel = new InitialAssessmentQAViewModel
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
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),
                TreatmentReferrerSupplierPricing = treatmentReferrerSupplierPricing,
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                Durations = Mapper.Map<IEnumerable<Duration>>(_lookUpService.GetAllDuration()),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                CaseAppointmentDate = Mapper.Map<ITSModel.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)id)),
                AffectedAreas = Mapper.Map<IEnumerable<AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<SymptomDescription>>(_lookUpService.GetAllSymptomDescription()),
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes())
            };
            return View(viewModel);
        }
        public ActionResult InitialAssessmentQACustom(int id)
        {
            string filestoragePath = "";
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var treatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID));
            var caseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id));
            //string storagePath = ConfigurationManager.AppSettings["ITSPublicAppStorage"];
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            var _supplierDocument = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(id, (int)GlobalConst.CaseDocumentTypeId.InitialAssessmentSupplierCustom));
            foreach (var data in _supplierDocument)
            {
                filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)data.SupplierID, (int)data.ReferrerProjectTreatmentID, (int)data.CaseId, data.UploadPath, storagePath);
            }
            //for adding initial assessment price as default in case treatment pricing grid.
            if (caseTreatmentPricings.Count() == 0)
            {
                if (treatmentReferrerSupplierPricing.Count() != 0)
                {
                    var caseTreatmentPricing = treatmentReferrerSupplierPricing.FirstOrDefault(i => i.PricingTypeName == "Initial Assessment");
                    List<CaseTreatmentPricing> lst = new List<CaseTreatmentPricing>() { };
                    lst.Add(new CaseTreatmentPricing { CaseID = caseObj.CaseID, ReferrerPrice = caseTreatmentPricing.ReferrerPrice, ReferrerPricingID = caseTreatmentPricing.ReferrerPricingID, SupplierPrice = caseTreatmentPricing.SupplierPrice, Quantity = 0, SupplierPriceID = caseTreatmentPricing.SupplierPriceID, AuthorizationStatus = true });
                    _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(lst).ToArray());
                }
            }
            var viewModel = new InitialAssessmentQAViewModelCustom
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
                TreatmentReferrerSupplierPricing = treatmentReferrerSupplierPricing,
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                RefferId = caseObj.ReferrerID,
                RefferProjectTreatmentID = caseObj.ReferrerProjectTreatmentID,
                supplierDocumentCustom = _supplierDocument,
                InitialAssesessmentCustomFilePath = filestoragePath,
                CaseAppointmentDate = Mapper.Map<ITSModel.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)id)),
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult DeleteCaseTreatmentPricingByCaseTreatmentPricingID(int caseTreatmentPricingID)
        {
            _caseService.DeleteCaseTreatmentPricingByCaseTreatmentPricingID(caseTreatmentPricingID);
            return Json("Successfully Deleted");
        }

        public ActionResult PhysiotherapyInitialAssessmentQA(int id)
        {
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var treatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID));
            var caseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id));
            var caseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(id));
            //for adding initial assessment price as default in case treatment pricing grid.
            if (caseTreatmentPricings.Count() == 0)
            {
                var caseTreatmentPricing = treatmentReferrerSupplierPricing.FirstOrDefault(i => i.PricingTypeName == GlobalConst.DocumentType.InitialAssessment);
                List<CaseTreatmentPricing> lst = new List<CaseTreatmentPricing>() { };
                lst.Add(new CaseTreatmentPricing { CaseID = caseObj.CaseID, ReferrerPrice = caseTreatmentPricing.ReferrerPrice, ReferrerPricingID = caseTreatmentPricing.ReferrerPricingID, SupplierPrice = caseTreatmentPricing.SupplierPrice, Quantity = 0, SupplierPriceID = caseTreatmentPricing.SupplierPriceID, AuthorizationStatus = true });
                _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(lst).ToArray());
            }
            var viewModel = new InitialAssessmentQAViewModel
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
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),
                TreatmentReferrerSupplierPricing = treatmentReferrerSupplierPricing,
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                Durations = Mapper.Map<IEnumerable<Duration>>(_lookUpService.GetAllDuration()),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                CaseAppointmentDate = Mapper.Map<ITSModel.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)id)),
                AffectedAreas = Mapper.Map<IEnumerable<AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<SymptomDescription>>(_lookUpService.GetAllSymptomDescription()),
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes())
            };
            return View(viewModel);
        }

        #region Initial Assessment Post Method Section
        [HttpPost]
        public ActionResult InitialAssessment(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject viewModel;
            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
                viewModel = GetInitialAssessmentQACaseWorkflowPatientProjects(skip, take);
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetInitialAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(assessmentObj =>
                {
                    assessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.InitialAssessmentController.InitialAssessmentQA, GlobalConst.Controllers.InitialAssessment, new { id = assessmentObj.CaseID });
                });
                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }
        [HttpPost]
        public ActionResult UpdateInitialAssessmentQA(InitialAssessmentQAViewModel viewModel)
        {
            var _resCase = _caseService.GetCaseByCaseID(viewModel.CaseAssessment.CaseID);
            var _resPat = _patientService.GetPatientAndCaseByCaseID(viewModel.CaseAssessment.CaseID);
            var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);
            var _re = _assessmentService.GetAllCaseAssessmentDetailByCaseID(viewModel.CaseAssessment.CaseID);
            int TotalIATreamentSession = 0;
            foreach (var _r in _re)
            {
                if (_r.AssessmentServiceID == 1)
                    TotalIATreamentSession += _r.PatientRecommendedTreatmentSessions;
            }
            viewModel.CaseAppointmentDate.CaseID = viewModel.CaseAssessment.CaseID;
            _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.CaseAppointmentDate));
            viewModel.CaseAssessment.UserID = ITSUser.UserID;
            if (viewModel.CaseAssessment.IsAccepted)
                viewModel.CaseAssessment.DeniedMessage = null;
            viewModel.CaseAssessment.CaseAssessmentRating.AssessmentServiceID = viewModel.CaseAssessment.AssessmentServiceID;
            viewModel.CaseAssessment.CaseAssessmentRating.RatingDate = DateTime.Now;
            viewModel.CaseAssessment.CaseAssessmentDetail.AssessmentDate = DateTime.Now;

            viewModel.CaseAssessment.CaseAssessmentDetail.PatientDateOfReturn = _re[0].PatientDateOfReturn;
            viewModel.CaseAssessment.CaseAssessmentDetail.PatientRecommendationReturn = _re[0].PatientRecommendationReturn;
            viewModel.CaseAssessment.CaseAssessmentDetail.IsPatientReturnToPreInjuryDuties = _re[0].IsPatientReturnToPreInjuryDuties;
            viewModel.CaseAssessment.CaseAssessmentDetail.PatientPreInjuryDutiesDate = _re[0].PatientPreInjuryDutiesDate;
             viewModel.CaseAssessment.CaseAssessmentDetail.MainFactors = _re[0].MainFactors;
            viewModel.CaseAssessment.IsSaved = true;

            _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            if (viewModel.CaseAssessment.IsAccepted)
            {
                if (viewModel.CaseTreatmentPricings != null)
                    _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings.Where(i => i.CaseTreatmentPricingID == 0)).ToArray());
                if (viewModel.CaseBespokeServicePricings != null)
                    _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).ToArray());

                string filename = null;                
                if (viewModel.CaseAssessmentDocument.FinalVersionFileUpload != null)
                {
                    filename = AddCaseUploads(viewModel.CaseAssessmentDocument);                    
                }                

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
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityApproved.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "Initial Assessment");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString()); // As there was not treatment session alloted yet.
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
               // bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Under DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Under DA", MailText);
                ITS.Domain.Models.IntialAssessmentReportDetail _IntialReport = new ITS.Domain.Models.IntialAssessmentReportDetail();
                _IntialReport = Mapper.Map<ITS.Domain.Models.IntialAssessmentReportDetail>(_caseService.GetInitialReviewAssessmentByCaseID(viewModel.CaseAssessment.CaseID));
                if (_IntialReport.DelegatedAuthorisationTypeID == 2)
                {

                    string InitialFilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/InitialReviewReportAcceptedBySupplier.html");
                    System.IO.StreamReader _str = new StreamReader(InitialFilePath);
                    string InitialMailText = _str.ReadToEnd();
                    _str.Close();
                    InitialMailText = InitialMailText.Replace("##CaseReferrerReferenceNumber##", _IntialReport.CaseReferrerReferenceNumber);
                    InitialMailText = InitialMailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                    InitialMailText = InitialMailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                    InitialMailText = InitialMailText.Replace("##TotalSession##", TotalIATreamentSession.ToString());
                    InitialMailText = InitialMailText.Replace("##AssessmentType##", "Initial Assessment");
                    InitialMailText = InitialMailText.Replace("##ActiveText##", "Many thanks for submitting the (Initial Assessment) for " + _IntialReport.PatientFullName + ". We have forwarded the report to the referrer.Please continue with treatment up to (0) sessions as per existing delegated authority. Should additional sessions over delegated authority have been requested we will provide an update on authority for these sessions once the referrer has reviewed the report");
                    InitialMailText = InitialMailText.Replace("##TotalSession##", _IntialReport.TotalSession.ToString());
                    InitialMailText = InitialMailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                    InitialMailText = InitialMailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                    InitialMailText = InitialMailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                    bool result = _emailService.SendGeneralEmail(_IntialReport.Email.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Accepted by Innovate", InitialMailText); 
                }
                else
                {
                    string InitialFilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/InitialReviewReportAcceptedBySupplier.html");
                    System.IO.StreamReader _str = new StreamReader(InitialFilePath);
                    string InitialMailText = _str.ReadToEnd();
                    str.Close();
                    InitialMailText = InitialMailText.Replace("##CaseReferrerReferenceNumber##", _IntialReport.CaseReferrerReferenceNumber);
                    InitialMailText = InitialMailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                    InitialMailText = InitialMailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                    InitialMailText = InitialMailText.Replace("##TotalSession##", TotalIATreamentSession.ToString());
                    InitialMailText = InitialMailText.Replace("##AssessmentType##", "Initial Assessment");
                    InitialMailText = InitialMailText.Replace("##ActiveText##", "Many thanks for submitting the Initial Assessment Report for " + _IntialReport.PatientFullName + ". We have forwarded the report to the referrer. ");
                    InitialMailText = InitialMailText.Replace("##TotalSession##", _IntialReport.TotalSession.ToString());
                    InitialMailText = InitialMailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                    InitialMailText = InitialMailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                    InitialMailText = InitialMailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                    bool result = _emailService.SendGeneralEmail(_IntialReport.Email.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Accepted by Innovate", InitialMailText); 
                }
            }
            else
            {
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityDenied.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "Initial Assessment");
                //ITS.Domain.Models.EvaluateDelegatedAuthorisationCost _ss = new ITSModel.EvaluateDelegatedAuthorisationCost();
                var _ss = _caseService.GetEvaluateDelegatedAuthorisationCost(viewModel.CaseAssessment.CaseID, 1);
                if (_ss.Cnt == 0)
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake treatment up to £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " and have advised the clinic to provide treatment only up to this limit.");
                else
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake ( £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " )  sessions and have advised the clinic to provide treatment only up to this limit.");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString()); // As there was not treatment session alloted yet.
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Over DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Over DA", MailText);
            }
            int IsSent2Authorization = _caseService.GetCheckCaseTreatmentPricingByCaseID(viewModel.CaseAssessment.CaseID, GlobalConst.AssessmentService.InitialAssessment);
            if (IsSent2Authorization > 0)
            {
                // if IsSent2Authorization = 1 then total amount of treatment prices for the case exceed the Delegated Authorization, the case would be sent to Authorization.
            }
            else
            {
                // if IsSent2Authorization = 0 then total amount of treatment prices f0or the case does not exceed the Delegated Authorization, the case is not sent to Authorization.
                _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(viewModel.CaseAssessment.CaseID);
            }
            _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            _assessmentService.UpdateCaseAssessmentIsSavedByCaseID(viewModel.CaseAssessment.CaseID);
            return Json(GlobalResource.UpdatedSuccessfully, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }

        private Attachment GenerateReport(int CaseID, string filename)
        {
            Attachment objAttachment = null;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            //string reportURL = string.Format(ConfigurationManager.AppSettings["InitialAssessmentReportUrl"], CaseID, GlobalConst.DocumentType.InitialAssessment, GlobalConst.ReportFormat.Word);
            string reportURL = string.Format(ConfigurationManager.AppSettings["InitialAssessmentReportUrl"], CaseID, GlobalConst.ReportFormat.Word);
            client.DownloadFile(reportURL, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            objAttachment = new System.Net.Mail.Attachment(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            return objAttachment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInitialAssessmentQACustom(InitialAssessmentQAViewModelCustom viewModel)
        {
            var _resCase = _caseService.GetCaseByCaseID(viewModel.caseAssessmentRating.CaseID);
            var _resPat = _patientService.GetPatientAndCaseByCaseID(viewModel.caseAssessmentRating.CaseID);
            var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);
            var _re = _assessmentService.GetAllCaseAssessmentDetailByCaseID(viewModel.caseAssessmentRating.CaseID);
            int TotalIATreamentSession = 0;
            foreach (var _r in _re)
            {
                if (_r.AssessmentServiceID == 1)
                    TotalIATreamentSession += _r.PatientRecommendedTreatmentSessions;
            }
            //var _res3 = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.caseAssessmentRating.CaseID);
            int IsSent2Authorization = 0;
            viewModel.caseAssessmentRating.AssessmentServiceID = GlobalConst.AssessmentService.InitialAssessment;
            viewModel.caseAssessmentRating.RatingDate = DateTime.Now;
            _assessmentService.UpdateCaseAssessmentRatingByCaseIDAndAssessmentServiceID(viewModel.caseAssessmentRating.CaseID, viewModel.caseAssessmentRating.AssessmentServiceID, viewModel.caseAssessmentRating.Rating);
            viewModel.CaseAppointmentDate.CaseID = viewModel.Case.CaseID;
            if (!viewModel.CaseAppointmentDate.ToString().Contains("0001"))
                _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.CaseAppointmentDate));
            if (viewModel.caseAssessmentCustom.isAccepted == true)
            {
                if (viewModel.CaseTreatmentPricings != null)
                    _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings.Where(i => i.CaseTreatmentPricingID == 0)).ToArray());
                if (viewModel.CaseBespokeServicePricings != null)
                    _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).ToArray());
                viewModel.caseAssessmentCustom.Message = "";
                string filestoragePath = "";
                string UploaedDocmentName = "";
                if (viewModel.InitialAssessmentFileFinal != null)
                {
                    UploaedDocmentName = (string)GlobalConst.SupplierDocumentType.InitialAssessmentCustomFinal.ToString() + Path.GetExtension(viewModel.InitialAssessmentFileFinal.FileName).ToString();
                    string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                    supplierDocument.SupplierID = (int)viewModel.supplierCustom.SupplierID;
                    supplierDocument.ReferrerProjectTreatmentID = (int?)viewModel.RefferProjectTreatmentID;
                    supplierDocument.UserID = ITSUser.UserID;
                    supplierDocument.DocumentName = (string)GlobalConst.SupplierDocumentType.InitialAssessmentCustomFinal;
                    supplierDocument.UploadPath = UploaedDocmentName;
                    supplierDocument.UploadDate = DateTime.Now;
                    supplierDocument.DocumentTypeID = (int)GlobalConst.CaseDocumentTypeId.InitialAssessmentCustomFinal;
                    supplierDocument.CaseId = viewModel.supplierCustom.CaseId;
                    int inerted = _supplierService.AddSupplierDocumentCustom(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                    filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewModel.supplierCustom.SupplierID, (int)viewModel.RefferProjectTreatmentID, (int)viewModel.supplierCustom.CaseId, UploaedDocmentName, storagePath);
                    viewModel.InitialAssessmentFileFinal.SaveAs(filestoragePath);
                }
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
                // if IsSent2Authorization = 1 then total amount of treatment prices for the case exceed the Delegated Authorization, the case would be sent to Authorization.
                IsSent2Authorization = _caseService.GetCheckCaseTreatmentPricingByCaseID(viewModel.Case.CaseID, GlobalConst.AssessmentService.InitialAssessment);
                if (IsSent2Authorization == 0)
                {
                    _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(viewModel.Case.CaseID);
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.Case.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
                }
                else
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.Case.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisationCustom);
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityApproved.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "InitialAssessment");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString());
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
               // bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Under DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Under DA", MailText);
            }
            else
            {
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.Case.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.ReportNotAcceptedCustom);
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityDenied.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "InitialAssessment");
                var _ss = _caseService.GetEvaluateDelegatedAuthorisationCost(viewModel.caseAssessmentRating.CaseID, 1);
                if (_ss.Cnt == 0)
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake treatment up to £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " and have advised the clinic to provide treatment only up to this limit.");
                else
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake ( £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " )  sessions and have advised the clinic to provide treatment only up to this limit.");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString()); // As there was not treatment session alloted yet.
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                bool result = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Over DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Over DA", MailText);
            }
            _assessmentService.UpdateCaseAssessmentCustom(Mapper.Map<ITSService.AssessmentService.CaseAssessmentCustom>(viewModel.caseAssessmentCustom));
            _assessmentService.UpdateCaseInitialAssessmentMessageCustom(Mapper.Map<ITSService.AssessmentService.CaseAssessmentCustom>(viewModel.caseAssessmentCustom));
            return Json(GlobalResource.UpdatedSuccessfully, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }
        [NonAction]
        private string AddCaseUploads(CaseAssessmentDocument caseAssessmentDocument)
        {
            string filename = AddDocumentFile(caseAssessmentDocument.FinalVersionFileUpload);
            caseAssessmentDocument.UserID = ITSUser.UserID;
            caseAssessmentDocument.DocumentTypeID = GlobalConst.CaseDocumentTypeId.InitialAssessment;
            caseAssessmentDocument.UploadDate = DateTime.Now;
            caseAssessmentDocument.UploadPath = filename;
            _caseService.AddCaseDocument(Mapper.Map<ITSService.CaseService.CaseDocument>(caseAssessmentDocument));
            return filename;
        }
        [NonAction]
        private string AddDocumentFile(HttpPostedFileBase file)
        {
            if (!Directory.Exists(Server.MapPath(GlobalConst.CaseDocumentStoragePath)))
                Directory.CreateDirectory(Server.MapPath(GlobalConst.CaseDocumentStoragePath));
            var path = Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName)));
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
        public ActionResult ViewIntialAssessmentReport(int CaseID)
        {
            return Redirect("~/WebForms/Reports/InitialAssessmentQAReport.aspx?CaseID=" + CaseID);
        }
        #endregion
    }
}

