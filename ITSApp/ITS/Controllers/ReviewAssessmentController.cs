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
using System.Net;


namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReviewAssessmentController : BaseController
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
        public ReviewAssessmentController(ITSService.CaseService.ICaseService caseService,
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
            _assessmentService = assessmentService;
            _lookUpService = lookUpService;
            _patientService = patientService;
            _PractitionerService = practitionerService;
            _emailService = emailService;
            _referrerStorage = referrerStorage;
            _supplierService = supplierService;
            _supplierStorageService = supplierStorageService;
            supplierDocument = _supplierDocument;
            _referrerService = referrerService;

        }
        public ActionResult Index()
        {
            return View(GetReviewAssessmentQACaseWorkflowPatientProjects((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }
        public ActionResult ReviewAssessmentQA(int id)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(id));
            var referrerProjectTreatmentID = caseObj.ReferrerProjectTreatmentID;
            var viewModel = new ReviewAssessmentQAViewModel
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
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentQA(id)),
                caseAppointmentDate = Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(id)),
                Practitioners = Mapper.Map<IEnumerable<Practitioner>>(_PractitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(caseObj.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),
                Durations = Mapper.Map<IEnumerable<Duration>>(_lookUpService.GetAllDuration()),
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                TreatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID)),
                AffectedAreas = Mapper.Map<IEnumerable<AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<SymptomDescription>>(_lookUpService.GetAllSymptomDescription()),
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes())

            };
            var _treatmentSessionByCaseID = _caseService.GetTreatmentSessionByCaseID(id);
            viewModel.caseAppointmentDate.CaseBookIADate = DateTime.Parse(viewModel.caseAppointmentDate.strAppointmentDate);
            return View(viewModel);
        }
        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetReviewAssessmentQACaseWorkflowPatientProjects(int skip, int take)
        {
            var pagedCaseWorkflowPatientReviewAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>
                (_caseService.GetReviewAssessmentQACaseWorkflowPatientProjects(skip, take));
            pagedCaseWorkflowPatientReviewAssessment.CaseWorkflowPatientReferrerProjects.ToList().ForEach(reviewAssessmentObj =>
            {
                if (reviewAssessmentObj.WorkflowID == (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoInnovateCustom)
                    reviewAssessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.ReviewAssessmentController.ReviewAssessmentCustomQA, GlobalConst.Controllers.ReviewAssessment, new { id = reviewAssessmentObj.CaseID });
                else
                    reviewAssessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.ReviewAssessmentController.ReviewAssessmentQA, GlobalConst.Controllers.ReviewAssessment, new { id = reviewAssessmentObj.CaseID });
            });
            pagedCaseWorkflowPatientReviewAssessment.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
            return pagedCaseWorkflowPatientReviewAssessment;
        }
        public ActionResult ReviewAssessmentCustomQA(int id)
        {
            string filestoragePath = "";
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(id));
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            var treatmentReferrerSupplierPricing = Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, casePatientTreatment.TreatmentCategoryID));
            var caseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id));
            var _caseAssessmentRating = Mapper.Map<ITS.Domain.Models.CaseAssessmentRating>(_assessmentService.GetCaseAssessmentRatingByCaseIDAndAssessmentServiceID(id, GlobalConst.AssessmentService.ReviewAssessment));
            var _caseAssessmentCustom = Mapper.Map<ITS.Domain.Models.CaseAssessmentCustom>(_assessmentService.GetCaseAssessmentCustomByCaseID(id));
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            var _supplierDocument = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseIdAndDocumentTypeId(id, (int)GlobalConst.CaseDocumentTypeId.ReviewAssessmentSupplierCustom));
            foreach (var data in _supplierDocument)
            {
                filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)data.SupplierID, (int)data.ReferrerProjectTreatmentID, (int)data.CaseId, data.UploadPath, storagePath);
            }
            //for adding or fetching Review assessment price as default in case treatment pricing grid.
            if (caseTreatmentPricings.Count() == 0)
            {
                var caseTreatmentPricing = treatmentReferrerSupplierPricing.FirstOrDefault(i => i.PricingTypeName == GlobalConst.DocumentType.ReviewAssessment);
                List<CaseTreatmentPricing> lst = new List<CaseTreatmentPricing>() { };
                lst.Add(new CaseTreatmentPricing { CaseID = caseObj.CaseID, ReferrerPrice = caseTreatmentPricing.ReferrerPrice, ReferrerPricingID = caseTreatmentPricing.ReferrerPricingID, SupplierPrice = caseTreatmentPricing.SupplierPrice, Quantity = 0, SupplierPriceID = caseTreatmentPricing.SupplierPriceID });
                _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(lst).ToArray());
            }
            var viewModel = new ReviewAssessmentCustomQAViewModel
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
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentQA(id)),
                TreatmentReferrerSupplierPricing = treatmentReferrerSupplierPricing,
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                caseAssessmentRating = _caseAssessmentRating,
                caseAssessmentCustom = _caseAssessmentCustom,
                CaseTreatmentPricings = Mapper.Map<IEnumerable<CaseTreatmentPricing>>(_caseService.GetCaseTreatmentPricingByCaseID(id)),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(id)),
                RefferId = caseObj.ReferrerID,
                RefferProjectTreatmentID = caseObj.ReferrerProjectTreatmentID,
                supplierDocumentCustom = _supplierDocument,
                ReviewAssesessmentCustomFilePath = filestoragePath,
                CaseAppointmentDate = Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)id)),
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteCaseTreatmentPricingByCaseTreatmentPricingID(int caseTreatmentPricingID)
        {
            _caseService.DeleteCaseTreatmentPricingByCaseTreatmentPricingID(caseTreatmentPricingID);
            return Json("Successfully Deleted");
        }
        #region Post Method Section.

        [HttpPost]
        public ActionResult ReviewAssessment(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject viewModel;
            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
                viewModel = GetReviewAssessmentQACaseWorkflowPatientProjects(skip, take);
            else
            {
                viewModel = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReviewAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
                viewModel.CaseWorkflowPatientReferrerProjects.ToList().ForEach(reviewAssessmentObj =>
                {
                    reviewAssessmentObj.ActionUrl = Url.Action(GlobalConst.Actions.ReviewAssessmentController.ReviewAssessmentQA, GlobalConst.Controllers.ReviewAssessment, new { id = reviewAssessmentObj.CaseID });
                });
                viewModel.TreatmentCategoryID = treatmentCategoryID;
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateReviewAssessmentQA(ReviewAssessmentQAViewModel viewModel)
        {
            try
            {
                var _resCase = _caseService.GetCaseByCaseID(viewModel.CaseAssessment.CaseID);
                var _resPat = _patientService.GetPatientAndCaseByCaseID(viewModel.CaseAssessment.CaseID);
                var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);
                var _res3 = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.CaseAssessment.CaseID);
                var _re = _assessmentService.GetAllCaseAssessmentDetailByCaseID(viewModel.CaseAssessment.CaseID);

                int TotalIATreamentSession = 0;
                foreach (var _r in _re)
                {
                    if (_r.AssessmentServiceID == 1)
                        TotalIATreamentSession += _r.PatientRecommendedTreatmentSessions;
                }

                viewModel.caseAppointmentDate.CaseID = viewModel.CaseAssessment.CaseID;
                _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.caseAppointmentDate));

                viewModel.CaseAssessment.UserID = ITSUser.UserID;
                if (viewModel.CaseAssessment.IsAccepted)
                    viewModel.CaseAssessment.DeniedMessage = null;
                viewModel.CaseAssessment.CaseAssessmentRating.AssessmentServiceID = viewModel.CaseAssessment.AssessmentServiceID;
                viewModel.CaseAssessment.CaseAssessmentDetail.AssessmentDate = DateTime.Now;
                viewModel.CaseAssessment.CaseAssessmentRating.RatingDate = DateTime.Now;
                viewModel.CaseAssessment.IsSaved = true;
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientDateOfReturn = _re[0].PatientDateOfReturn;
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientRecommendationReturn = _re[0].PatientRecommendationReturn;
                viewModel.CaseAssessment.CaseAssessmentDetail.IsPatientReturnToPreInjuryDuties = _re[0].IsPatientReturnToPreInjuryDuties;
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientPreInjuryDutiesDate = _re[0].PatientPreInjuryDutiesDate;
                viewModel.CaseAssessment.CaseAssessmentDetail.MainFactors = _re[0].MainFactors;
                _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));

                if (viewModel.CaseAssessment.IsAccepted)
                {
                    if (viewModel.CaseTreatmentPricings != null)
                    {
                        viewModel.CaseTreatmentPricings.ToList().ForEach(o => o.ReferrerPrice = Convert.ToDecimal(o.ReferrerPrice));
                        _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings).ToArray());
                    }
                    if (viewModel.CaseBespokeServicePricings != null)
                    {
                        viewModel.CaseBespokeServicePricings.ToList().ForEach(o => o.CaseID = viewModel.CaseAssessment.CaseID);
                        _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).ToArray());
                    }

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

                        //obj.Dispose();

                        if (result)
                            _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(viewModel.CaseCommunicationHistory));
                    }
                    var _tp = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.CaseAssessment.CaseID);

                    string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityApproved.html");
                    System.IO.StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                    MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                    MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                    MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                    MailText = MailText.Replace("##AssessmentType##", "Review Assessment");
                    MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString());
                    MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                    MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                    MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                    bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Under DA", MailText);
                    // bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Under DA", MailText);

                    ITS.Domain.Models.IntialAssessmentReportDetail _IntialReport = new ITS.Domain.Models.IntialAssessmentReportDetail();
                    _IntialReport = Mapper.Map<ITS.Domain.Models.IntialAssessmentReportDetail>(_caseService.GetInitialReviewAssessmentByCaseID(viewModel.CaseAssessment.CaseID));


                    int ActiveSessionsCount = 0;
                    foreach (var i in _tp)
                    {
                        if (i.AuthorizationStatus != null)
                        {
                            if (i.AuthorizationStatus.Value == true)
                                ActiveSessionsCount++;
                        }
                    }

                    if (_IntialReport.DelegatedAuthorisationTypeID == 2)
                    {

                        string ReviewFilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/InitialReviewReportAcceptedBySupplier.html");
                        System.IO.StreamReader _str = new StreamReader(ReviewFilePath);
                        string ReviewMailText = _str.ReadToEnd();
                        _str.Close();
                        ReviewMailText = ReviewMailText.Replace("##CaseReferrerReferenceNumber##", _IntialReport.CaseReferrerReferenceNumber);
                        ReviewMailText = ReviewMailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                        ReviewMailText = ReviewMailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                        ReviewMailText = ReviewMailText.Replace("##TotalSession##", ActiveSessionsCount.ToString());
                        ReviewMailText = ReviewMailText.Replace("##AssessmentType##", "Review Assessment");
                        ReviewMailText = ReviewMailText.Replace("##ActiveText##", "Many thanks for submitting the Review Assessment for " + _IntialReport.PatientFullName + ". We have forwarded the report to the referrer.Please continue with treatment up to (" + ActiveSessionsCount.ToString() + ") sessions as per existing delegated authority. Should additional sessions over delegated authority have been requested we will provide an update on authority for these sessions once the referrer has reviewed the report");
                        ReviewMailText = ReviewMailText.Replace("##TotalSession##", _IntialReport.TotalSession.ToString());
                        ReviewMailText = ReviewMailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                        ReviewMailText = ReviewMailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                        ReviewMailText = ReviewMailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                        bool result = _emailService.SendGeneralEmail(_IntialReport.Email.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Accepted by Innovate", ReviewMailText);

                    }
                    else
                    {
                        string ReviewFilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/InitialReviewReportAcceptedBySupplier.html");
                        System.IO.StreamReader _str = new StreamReader(ReviewFilePath);
                        string ReviewMailText = _str.ReadToEnd();
                        str.Close();
                        ReviewMailText = ReviewMailText.Replace("##CaseReferrerReferenceNumber##", _IntialReport.CaseReferrerReferenceNumber);
                        ReviewMailText = ReviewMailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                        ReviewMailText = ReviewMailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                        ReviewMailText = ReviewMailText.Replace("##TotalSession##", ActiveSessionsCount.ToString());
                        ReviewMailText = ReviewMailText.Replace("##AssessmentType##", "Review Assessment");
                        ReviewMailText = ReviewMailText.Replace("##ActiveText##", "Many thanks for submitting the Review Assessment Report for " + _IntialReport.PatientFullName + ". We have forwarded the report to the referrer. ");
                        ReviewMailText = ReviewMailText.Replace("##TotalSession##", _IntialReport.TotalSession.ToString());
                        ReviewMailText = ReviewMailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                        ReviewMailText = ReviewMailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                        ReviewMailText = ReviewMailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                        bool result = _emailService.SendGeneralEmail(_IntialReport.Email.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Accepted by Innovate", ReviewMailText);
                    }
                }
                else
                {

                    var _tp = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.CaseAssessment.CaseID);
                    int PendingSessionsCnt = 0;
                    foreach (var i in _tp)
                    {
                        if (i.AuthorizationStatus != null)
                        {
                            if (i.AuthorizationStatus.Value == false)
                                PendingSessionsCnt++;
                        }
                    }
                    string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityDenied.html");
                    System.IO.StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                    MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                    MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                    MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                    MailText = MailText.Replace("##AssessmentType##", "Review Assessment");
                    var _ss = _caseService.GetEvaluateDelegatedAuthorisationCost(viewModel.CaseAssessment.CaseID, 2);
                    if (_ss.Cnt == 0)
                        MailText = MailText.Replace("##Text##", "We have delegated authority to undertake treatment up to £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " and have advised the clinic to provide treatment only up to this limit.");
                    else
                        MailText = MailText.Replace("##Text##", "We have delegated authority to undertake ( £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " )  sessions and have advised the clinic to provide treatment only up to this limit.");
                    MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString());
                    MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                    MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                    MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                    bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Over DA", MailText);
                    //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Over DA", MailText);
                }
                int IsSent2Authorization = _caseService.GetCheckCaseTreatmentPricingByCaseID(viewModel.CaseAssessment.CaseID, GlobalConst.AssessmentService.ReviewAssessment);
                if (IsSent2Authorization > 0)
                {
                    // if IsSent2Authorization = 1 then total amount of treatment prices for the case exceed the Delegated Authorization, the case would be sent to Authorization.
                }
                else
                {
                    // if IsSent2Authorization = 0 then total amount of treatment prices for the case does not exceed the Delegated Authorization, the case is not sent to Authorization.
                    _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(viewModel.CaseAssessment.CaseID);
                }
                _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
                _assessmentService.UpdateCaseAssessmentIsSavedByCaseID(viewModel.CaseAssessment.CaseID);
                return Json(GlobalResource.UpdatedSuccessfully, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
            }
            catch (Exception ex)
            {
                return Json("Error : " + ex.Message, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
            }
        }
        private Attachment GenerateReport(int CaseID, string filename)
        {
            Attachment objAttachment = null;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], CaseID, GlobalConst.DocumentType.ReviewAssessment, GlobalConst.ReportFormat.Word);
            client.DownloadFile(reportURL, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            objAttachment = new System.Net.Mail.Attachment(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"] + GlobalConst.Folders.CaseDocuments + filename));
            return objAttachment;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateReviewAssessmentCustomQA(ReviewAssessmentCustomQAViewModel viewModel, string radioHasValue)
        {
            var _resCase = _caseService.GetCaseByCaseID(viewModel.Case.CaseID);
            var _resPat = _patientService.GetPatientAndCaseByCaseID(viewModel.Case.CaseID);
            var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);
            var _res3 = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.Case.CaseID);

            var _re = _assessmentService.GetAllCaseAssessmentDetailByCaseID(viewModel.Case.CaseID);
            int TotalIATreamentSession = 0;
            foreach (var _r in _re)
            {
                if (_r.AssessmentServiceID == 1)
                    TotalIATreamentSession += _r.PatientRecommendedTreatmentSessions;
            }


            int IsSent2Authorization = 0;
            if (radioHasValue == "yes")
                viewModel.caseAssessmentCustom.IsFurtherTreatment = true;
            else
                viewModel.caseAssessmentCustom.IsFurtherTreatment = false;
            if (viewModel.caseAssessmentCustom.isAccepted)
            {
                _assessmentService.UpdateCaseAssessmentCustom(Mapper.Map<ITSService.AssessmentService.CaseAssessmentCustom>(viewModel.caseAssessmentCustom));
                viewModel.caseAssessmentCustom.ReviewAssessmentMessage = "";
            }
            ITS.Domain.Models.CaseAssessmentRating caseAssessmentRating = Mapper.Map<ITS.Domain.Models.CaseAssessmentRating>(_assessmentService.GetCaseAssessmentRatingByCaseIDAndAssessmentServiceID(viewModel.caseAssessmentCustom.CaseID, GlobalConst.AssessmentService.ReviewAssessment));
            _assessmentService.UpdateCaseRiewAssessmentMessageCustom(Mapper.Map<ITSService.AssessmentService.CaseAssessmentCustom>(viewModel.caseAssessmentCustom));
            viewModel.CaseAppointmentDate.CaseID = viewModel.Case.CaseID;
            _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.CaseAppointmentDate));
            viewModel.caseAssessmentRating.AssessmentServiceID = GlobalConst.AssessmentService.ReviewAssessment;
            viewModel.caseAssessmentRating.RatingDate = DateTime.Now;
            if (caseAssessmentRating == null)
                _assessmentService.AddCaseAssessmentRating(Mapper.Map<ITSService.AssessmentService.CaseAssessmentRating>(viewModel.caseAssessmentRating));
            else
                _assessmentService.UpdateCaseAssessmentRatingByCaseIDAndAssessmentServiceID(viewModel.caseAssessmentCustom.CaseID, GlobalConst.AssessmentService.ReviewAssessment, viewModel.caseAssessmentRating.Rating);
            if (viewModel.caseAssessmentCustom.isAccepted)
            {
                if (viewModel.CaseTreatmentPricings != null)
                {
                    viewModel.CaseTreatmentPricings.ToList().ForEach(o => o.ReferrerPrice = Convert.ToDecimal(o.ReferrerPrice));
                    _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings.Where(i => i.CaseTreatmentPricingID == 0)).ToArray());
                }
                if (viewModel.CaseBespokeServicePricings != null)
                {
                    viewModel.CaseBespokeServicePricings.ToList().ForEach(o => o.CaseID = viewModel.Case.CaseID);
                    _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).ToArray());
                }
                string filestoragePath = "";
                string UploadDocmentName = "";
                if (viewModel.ReviewAssessmentFileFinal != null)
                {
                    UploadDocmentName = (string)GlobalConst.SupplierDocumentType.ReviewAssessmentFinalCustom.ToString() + "_" + DateTime.Now.ToString("MMddyyhhmmss") + Path.GetExtension(viewModel.ReviewAssessmentFileFinal.FileName).ToString();
                    string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                    supplierDocument.SupplierID = (int)viewModel.supplierCustom.SupplierID;
                    supplierDocument.ReferrerProjectTreatmentID = (int?)viewModel.RefferProjectTreatmentID;
                    supplierDocument.UserID = ITSUser.UserID;
                    supplierDocument.DocumentName = (string)GlobalConst.SupplierDocumentType.ReviewAssessmentFinalCustom;
                    supplierDocument.UploadPath = UploadDocmentName;
                    supplierDocument.UploadDate = DateTime.Now;
                    supplierDocument.DocumentTypeID = (int)GlobalConst.CaseDocumentTypeId.ReviewAssessmentFinalCustom;
                    supplierDocument.CaseId = viewModel.supplierCustom.CaseId;
                    int inerted = _supplierService.AddSupplierDocumentCustom(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                    filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewModel.supplierCustom.SupplierID, (int)viewModel.RefferProjectTreatmentID, (int)viewModel.supplierCustom.CaseId, UploadDocmentName, storagePath);
                    viewModel.ReviewAssessmentFileFinal.SaveAs(filestoragePath);
                }
                Attachment obj = null;
                if (filestoragePath != null)
                    obj = new System.Net.Mail.Attachment(filestoragePath);
                if (viewModel.CaseCommunicationHistory.NotifyReferrer)
                {
                    viewModel.CaseCommunicationHistory.UserID = ITSUser.UserID;
                    viewModel.CaseCommunicationHistory.UploadPath = UploadDocmentName;

                    bool result = _emailService.SendGeneralEmail(viewModel.CaseCommunicationHistory.SentTo, viewModel.CaseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), viewModel.CaseCommunicationHistory.Subject, viewModel.CaseCommunicationHistory.Message, obj);
                    if (result)
                        _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(viewModel.CaseCommunicationHistory));
                }
                IsSent2Authorization = _caseService.GetCheckCaseTreatmentPricingByCaseID(viewModel.Case.CaseID, GlobalConst.AssessmentService.ReviewAssessment);
                if (IsSent2Authorization > 0)
                {
                    // if IsSent2Authorization = 1 then total amount of treatment prices for the case exceed the Delegated Authorization, the case would be sent to Authorization.
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseAssessmentCustom.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoReferrerCustom);
                }
                else
                {
                    // if IsSent2Authorization = 0 then total amount of treatment prices for the case does not exceed the Delegated Authorization, the case is not sent to Authorization.
                    _caseService.UpdateCaseTreatmentPricingAuthorisationStatusByCaseID(viewModel.Case.CaseID);
                    _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseAssessmentCustom.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
                }
                var _tp = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.Case.CaseID);
                //int PendingSessionsCnt = 0;
                //foreach (var i in _tp)
                //{
                //    if (i.AuthorizationStatus.Value == false)
                //        PendingSessionsCnt++;
                //}
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityApproved.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "Review Assessment");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString());
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Under DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Under DA", MailText);
            }
            else
            {
                //IsSent2Authorization = 1;
                var _tp = _caseService.GetCaseTreatmentPricingByCaseID(viewModel.Case.CaseID);
                int PendingSessionsCnt = 0;
                foreach (var i in _tp)
                {
                    if (i.AuthorizationStatus.Value == false)
                        PendingSessionsCnt++;
                }
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/DelegatedAuthorityDenied.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _resPat.FirstName + ' ' + _resPat.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##AssessmentType##", "Review Assessment");
                var _ss = _caseService.GetEvaluateDelegatedAuthorisationCost(viewModel.Case.CaseID, 2);
                if (_ss.Cnt == 0)
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake treatment up to £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " and have advised the clinic to provide treatment only up to this limit.");
                else
                    MailText = MailText.Replace("##Text##", "We have delegated authority to undertake ( £" + _ss.Amount.ToString().Replace(".0000", ".00").Trim() + " )  sessions and have advised the clinic to provide treatment only up to this limit.");
                MailText = MailText.Replace("##TreatmentSessions##", TotalIATreamentSession.ToString());
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                bool _res = _emailService.SendGeneralEmail(_resRef.ReferrerMainContactEmail.Trim(), System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Report Approved Over DA", MailText);
                //bool _res = _emailService.SendGeneralEmail("rkumar@vsaindia.com", "software@hcrg.com", "Report Approved Over DA", MailText);
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseAssessmentCustom.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.ReviewAssessmentReportNotAcceptedCustom);
                _caseService.UpdateCaseWorkflowCustomByCaseID(viewModel.caseAssessmentCustom.CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom);
            }
            return Json(GlobalResource.UpdatedSuccessfully, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }
        [NonAction]
        private string AddCaseUploads(CaseAssessmentDocument caseAssessmentDocument)
        {
            string filename = AddDocumentFile(caseAssessmentDocument.FinalVersionFileUpload);
            caseAssessmentDocument.UserID = ITSUser.UserID;
            caseAssessmentDocument.DocumentTypeID = GlobalConst.CaseDocumentTypeId.ReviewAssessment;
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
        #endregion
    }
}
