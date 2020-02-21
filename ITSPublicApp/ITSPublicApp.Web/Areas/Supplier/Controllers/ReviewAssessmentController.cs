using AutoMapper;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.LookUpService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.PractitionerService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class ReviewAssessmentController : BaseController
    {
        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IPatientService _patientService;
        private readonly IPractitionerService _practitionerService;
        private readonly ILookUpService _lookUpService;
        private readonly IReferrerService _referrerService;
        private readonly EmailService _emailService;
        private readonly ITSService.UserService.IUserService _userService;
        private readonly IEncryption _encryptionService; 

        public ReviewAssessmentController(ICaseService caseService, IAssessmentService assessmentService, IPatientService patientService,
                  IPractitionerService practitionerService, ILookUpService lookUpService, IReferrerService referrerService, EmailService emailService, ITSService.UserService.IUserService userService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _practitionerService = practitionerService;
            _lookUpService = lookUpService;
            _referrerService = referrerService;
            _emailService = emailService;
            _userService = userService;
            _encryptionService = encryptionService;
        }
        [HttpGet]
        public ActionResult Index(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            var caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(cid));
            var caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(cid));
            var caseAppointmentDate = Mapper.Map<Model.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(cid));
            var viewModel = new ReviewAssessmentViewModel
            {
                Patient = new Model.Patient
                {
                    FirstName = casePatientTreatment.FirstName,
                    LastName = casePatientTreatment.LastName,
                    BirthDate = casePatientTreatment.BirthDate,
                    InjuryDate = casePatientTreatment.InjuryDate
                },
                Case = new Model.Case
                {
                    CaseSubmittedDate = casePatientTreatment.CaseSubmittedDate,
                    CaseID = cid,
                    EncryptDecryptCaseID = id
                },
                Practitioners = Mapper.Map<IEnumerable<Model.Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(ITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                DateOfInitialAssessment = DateTime.Parse(caseAppointmentDate.strAppointmentDate),
                CaseAssessment = caseAssessment,
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<Model.TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes()),
                PatientRole = Mapper.Map<IEnumerable<Model.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Duration = Mapper.Map<IEnumerable<Model.Duration>>(_lookUpService.GetAllDuration()),
                CaseBespokeServicePricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingByCaseIDAndInComplete(cid)),
                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(cid)),
                AffectedAreas = Mapper.Map<IEnumerable<Model.AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<Model.RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<Model.StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<Model.SymptomDescription>>(_lookUpService.GetAllSymptomDescription())
            };
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult saveCaseTreatmentPricing(ReviewAssessmentViewModel viewModel)
        {
            //int CaseID = 0;
            if (viewModel.CaseTreatmentPricings != null)
            {
                foreach (var caseTreatmentPricing in viewModel.CaseTreatmentPricings)
                    _caseService.UpdateCaseTreatmentPricingByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(caseTreatmentPricing));
            }
            return Json("Saved Sucessfully", "text/html");
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReviewAssessment(ReviewAssessmentViewModel viewModel)
        {
            if (viewModel.CaseAssessment.IsSaved != null)
            {
                var test = viewModel.CaseAssessment.IsSaved.Value;
            }
            if (viewModel.CaseAssessment.RelevantTestUndertaken == null && viewModel.CaseAssessment.hdRelevantTestUndertaken == null)
                viewModel.CaseAssessment.RelevantTestUndertaken = " ";
            else
                viewModel.CaseAssessment.RelevantTestUndertaken = viewModel.CaseAssessment.hdRelevantTestUndertaken;

            if (viewModel.CaseAssessment.IncidentAndDiagnosisDescription == null)
                viewModel.CaseAssessment.IncidentAndDiagnosisDescription = " ";
            viewModel.CaseAssessment.UserID = ITSUser.UserID;
            viewModel.CaseAssessment.CaseAssessmentDetail.AssessmentDate = DateTime.Now;
            viewModel.CaseAssessment.CaseAssessmentRating = null;

            if (viewModel.CaseAssessment.CaseAssessmentDetail.TreatmentPeriodTypeID != 4)
            {
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriod = 0;
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID = 1;
            }
            if ((viewModel.CaseAssessment.AssessmentServiceID == 1) || (viewModel.CaseAssessment.AssessmentServiceID == 2
                && (viewModel.CaseAssessment.IsAccepted)) && !((viewModel.CaseAssessment.IsSaved != null) ? viewModel.CaseAssessment.IsSaved.Value : false))
            {
                viewModel.CaseAssessment.IsSaved = true;
                _assessmentService.AddCaseAssessment(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            }
            else
            {
                viewModel.CaseAssessment.IsSaved = true;
                _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            }
            if (viewModel.CaseBespokeServicePricings != null)
            {
                foreach (var CaseBespokeServicePricing in viewModel.CaseBespokeServicePricings)
                    _caseService.UpdateCaseBespokeServicePricingByCaseBespokeServiceID(Mapper.Map<ITSService.CaseService.CaseBespokeServicePricing>(CaseBespokeServicePricing));
            }
            int DidNotShowCnt = 0;

            // After click Save in Review Assessment  / Final Assessment  if only one "Did Not Show" checkbox is checked, send email to: a. support@innovatehmg.co.uk

            string[] _userEmails = _userService.GetAllUserEmailsByCaseContactByCaseID(viewModel.CaseAssessment.CaseID);
            string ReplyToEmails = "";
            bool result;

            if (_userEmails.Length > 1)
            {
                foreach (string _email in _userEmails)
                    ReplyToEmails += _email + ",";
            }
            else if (_userEmails.Length == 1)
                ReplyToEmails = _userEmails[0];

            if (DidNotShowCnt == 1)
            {
                var _resCase = _caseService.GetCaseByCaseID(viewModel.CaseAssessment.CaseID);
                var _resRef = _referrerService.GetReferrerDetailsByReferrerID(_resCase.ReferrerID);


                IntialAssessmentReportDetail _IntialReport = new IntialAssessmentReportDetail();
                _IntialReport = Mapper.Map<IntialAssessmentReportDetail>(_caseService.GetIntialAssessmentReportDetailsByCaseID(viewModel.CaseAssessment.CaseID));
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/TreatmentMatrixDidNotAttend.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("##ReferrerName##", _resRef.ReferrerName);
                MailText = MailText.Replace("##ReferenceNumber##", _resCase.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##PatientName##", _IntialReport.PatientFullName);
                MailText = MailText.Replace("##CaseReferenceNumber##", _resCase.CaseNumber);
                MailText = MailText.Replace("##SupplierName##", _IntialReport.SupplierName);
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);

                result = _emailService.SendMultipleEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], ReplyToEmails, "First Did Not Attend - " + viewModel.Patient.PatientFullName, MailText);

            }
            else if (DidNotShowCnt > 1)
            {
                string NumberToShow = "";

                var _resCase_DNS = _caseService.GetCaseByCaseID(viewModel.CaseAssessment.CaseID);
                var _resRef_DNS = _referrerService.GetReferrerDetailsByReferrerID(_resCase_DNS.ReferrerID);
                //string[] _userEmails = _userService.GetAllUserEmailsByCaseContactByCaseID(viewModel.CaseAssessment.CaseID);

                string[] numbering = {"2nd", "3rd", "4th" ,"5th" ,"6th" ,"7th" ,"8th" ,"9th" ,"10th" ,"11th" ,"12th" ,"13th" ,"14th" ,"15th" 
                                         ,"16th" ,"17th" ,"18th" ,"19th" ,"20th" ,"21st" ,"22nd","23rd","24th", "25th","26th" ,"27th", "28th", "29th" ,"30th"};

                /////// ....................... Loop with the foreach keyword........................../////
                foreach (string value in numbering)
                {
                    string[] numberValue = Regex.Split(value, @"\D+");
                    if (DidNotShowCnt == Convert.ToInt32(numberValue[0]))
                    {
                        NumberToShow = value;
                        break;
                    }
                }


                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/TreatmentMatrixDidNotAttendCheckbox.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText_DNS = str.ReadToEnd();
                str.Close();
                MailText_DNS = MailText_DNS.Replace("##ReferrerName##", _resRef_DNS.ReferrerName);
                MailText_DNS = MailText_DNS.Replace("##ReferrerReferenceNumber##", _resCase_DNS.CaseReferrerReferenceNumber);
                MailText_DNS = MailText_DNS.Replace("##PatientName##", viewModel.Patient.PatientFullName);
                MailText_DNS = MailText_DNS.Replace("##CaseReferenceNumber##", _resCase_DNS.CaseNumber);
                MailText_DNS = MailText_DNS.Replace("##NumberToShow##", NumberToShow);
                MailText_DNS = MailText_DNS.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText_DNS = MailText_DNS.Replace("##LogoPath2##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText_DNS = MailText_DNS.Replace("##LogoPath3##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);


                result = _emailService.SendMultipleEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], ReplyToEmails, "First Did Not Attend - " + viewModel.Patient.PatientFullName, MailText_DNS);

            }

            return Json(" Saved Sucessfully", "text/html");
        }

        [HttpPost]
        public ActionResult SubmitReviewAssessment(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            return Json("Sucessfully Submitted", "text/html");
        }
    }
}
