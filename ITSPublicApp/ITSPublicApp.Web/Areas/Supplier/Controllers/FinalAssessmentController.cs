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
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
     [AuthorizedUserCheck("Supplier")]
    public class FinalAssessmentController : BaseController
    {

        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IPatientService _patientService;
        private readonly IPractitionerService _practitionerService;
        private readonly IEncryption _encryptionService; 
        private readonly ILookUpService _lookUpService;
        private readonly IReferrerService _referrerService;
        private readonly EmailService _emailService;

        public FinalAssessmentController(
            ICaseService caseService, 
            IAssessmentService assessmentService, 
            IPatientService patientService,
            IPractitionerService practitionerService, 
            ILookUpService lookUpService, 
            IReferrerService referrerService,
            EmailService emailService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _practitionerService = practitionerService;
            _lookUpService = lookUpService;
            _referrerService = referrerService;
            _emailService = emailService;
            _encryptionService = encryptionService;
        }
        //
        // GET: /Supplier/FinalAssessment/

        [HttpGet]
        public ActionResult Index(string id)
        {
            int cid = Convert.ToInt32(DecryptString(id));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            var caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(cid));
            var caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(cid));
          
            var viewModel = new FinalAssessmentViewModel
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
                    EncryptedCaseID = EncryptString(cid.ToString())
                },
                Practitioners = Mapper.Map<IEnumerable<Model.Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(ITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(cid)).strAppointmentDate),
                CaseAssessment = caseAssessment,
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
            //  
        }
        [HttpPost]
        public ActionResult saveCaseTreatmentPricing(ReviewAssessmentCustomViewModel viewModel)
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
        public ActionResult SaveFinalAssessment(FinalAssessmentViewModel viewModel)
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
            viewModel.CaseAssessment.IsSaved = true;
            _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            
            if (viewModel.CaseBespokeServicePricings != null)
            {
                foreach (var CaseBespokeServicePricing in viewModel.CaseBespokeServicePricings)
                    _caseService.UpdateCaseBespokeServicePricingByCaseBespokeServiceID(Mapper.Map<ITSService.CaseService.CaseBespokeServicePricing>(CaseBespokeServicePricing));
            }

            int DidNotShowCnt = 0;
            if (viewModel.CaseTreatmentPricings != null)
            {
                foreach (var _caseTreatmentPricing in viewModel.CaseTreatmentPricings)
                {
                    if (_caseTreatmentPricing.PatientDidNotAttend != null)
                        if (_caseTreatmentPricing.PatientDidNotAttend.Value)
                            DidNotShowCnt++;

                    _caseService.UpdateCaseTreatmentPricingByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(_caseTreatmentPricing));
                }
            }
            // After click Save in Review Assessment / Final Assessment  if only one "Did Not Show" checkbox is checked, send email to:   a. support@innovatehmg.co.uk

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
                bool result = _emailService.SendGeneralEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "First Did Not Attend - " + _IntialReport.PatientFullName, MailText);
            }
            return Json(" Saved Sucessfully", "text/html");
        }
        [HttpPost]
        public ActionResult SubmitFinalAssessment(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            return Json("Sucessfully Submitted", "text/html");
        }

    }

    
}
