using AutoMapper;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.LookUpService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.PractitionerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;


namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class InitialAssessmentController : BaseController
    {
        private readonly ICaseService _caseService;
        private readonly IAssessmentService _assessmentService;
        private readonly IPatientService _patientService;
        private readonly IPractitionerService _practitionerService;
        private readonly ILookUpService _lookUpService;
        private readonly EmailService _emailService;
        private readonly IEncryption _encryptionService;

        public InitialAssessmentController(ICaseService caseService, IAssessmentService assessmentService, IPatientService patientService, IPractitionerService practitionerService, ILookUpService lookUpService, EmailService emailService,IEncryption encryptionService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _practitionerService = practitionerService;
            _lookUpService = lookUpService;
            _emailService = emailService;
            _encryptionService = encryptionService;
        }
        [HttpGet]
        public ActionResult Index(string id)
            {
            // To get all CasePatientTreatment
            int cid = Convert.ToInt32(DecryptString(id));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            var caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(cid));
            var caseAppointmentDate = Mapper.Map<Model.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(cid));
            var viewModel = new InitialAssessmentViewModel
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
                    CaseID = cid                    ,
                    EncryptDecryptCaseID = id
                },
                Practitioners = Mapper.Map<IEnumerable<Model.Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(ITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(cid)).strAppointmentDate),
                strDateOfInitialAssessment = caseAppointmentDate.CaseBookIADate.ToShortDateString(),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<Model.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Duration = Mapper.Map<IEnumerable<Model.Duration>>(_lookUpService.GetAllDuration()),
                TreatmentPeriodTypes = Mapper.Map<IEnumerable<Model.TreatmentPeriodType>>(_lookUpService.GetTreatmentPeriodTypes()),
                AffectedAreas = Mapper.Map<IEnumerable<Model.AffectedArea>>(_lookUpService.GetAllAffecteArea()),
                RestrictionRanges = Mapper.Map<IEnumerable<Model.RestrictionRange>>(_lookUpService.GetAllRestrictionRange()),
                StrengthTestings = Mapper.Map<IEnumerable<Model.StrengthTesting>>(_lookUpService.GetAllStrengthTesting()),
                SymptomDescriptions = Mapper.Map<IEnumerable<Model.SymptomDescription>>(_lookUpService.GetAllSymptomDescription())


            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(InitialAssessmentViewModel viewModel)
        {
            int userID = ITSUser.UserID;
            viewModel.CaseAssessment.UserID = userID;
            viewModel.CaseAssessment.CaseAssessmentRating = null;
            if (viewModel.CaseAssessment.IncidentAndDiagnosisDescription == null && viewModel.CaseAssessment.hdIncidentAndDiagnosisDescription == null)
                viewModel.CaseAssessment.IncidentAndDiagnosisDescription = " ";
            else
                viewModel.CaseAssessment.IncidentAndDiagnosisDescription = viewModel.CaseAssessment.hdIncidentAndDiagnosisDescription;

            var assesmentByCaseID = _assessmentService.GetCaseAssessmentByCaseID(viewModel.CaseAssessment.CaseID);
            viewModel.CaseAssessment.IsSaved = true;
            if (viewModel.CaseAssessment.RelevantTestUndertaken == null && viewModel.CaseAssessment.hdRelevantTestUndertaken == null)
                viewModel.CaseAssessment.RelevantTestUndertaken = " ";
            else
                viewModel.CaseAssessment.RelevantTestUndertaken = viewModel.CaseAssessment.hdRelevantTestUndertaken;
            
            var assessment = Mapper.Map<CaseAssessment>(viewModel.CaseAssessment);
            if (viewModel.CaseAssessment.CaseAssessmentDetail.TreatmentPeriodTypeID != 4)
            {
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriod = 0;
                viewModel.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID = 1;
            }
            if (assesmentByCaseID == null)
                _assessmentService.AddCaseAssessment(assessment);
            else
            {
               assesmentByCaseID.HasPatientConsentForm = viewModel.CaseAssessment.HasPatientConsentForm;
               _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));
            }
            return Json("Save Successfully", "text/html");
        }

        [HttpPost]
        public ActionResult Submit(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            _caseService.UpdateCaseWorkFlow(cid, ITSUser.UserID);
            IntialAssessmentReportDetail _IntialReport = new IntialAssessmentReportDetail();
            _IntialReport = Mapper.Map<IntialAssessmentReportDetail>(_caseService.GetIntialAssessmentReportDetailsByCaseID(cid));
            string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/InitialReportSubmittedTemplate.html");
            System.IO.StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("##CaseReferrerReferenceNumber##", _IntialReport.CaseReferrerReferenceNumber);
            MailText = MailText.Replace("##PatientName##", _IntialReport.PatientFullName);
            MailText = MailText.Replace("##SupplierName##", _IntialReport.SupplierName);
            MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
            bool result = _emailService.SendGeneralEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Initial Assessment Report Submitted", MailText);
            return Json("Submitted Successfully", "text/html");
        }

    }
}

