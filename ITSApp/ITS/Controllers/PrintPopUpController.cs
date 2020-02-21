using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITS.Infrastructure.Base;
using System.Web.Mvc;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using AutoMapper;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.InternalTaskModel;
using ITS.Infrastructure.ApplicationFilters;



namespace ITSWebApp.Controllers
{

    public static class PrintPopUpExtension
    {
        public static string ToYesNo(this bool boolValue)
        {
            return (boolValue ? "Yes" : "No");
        }
    }

    [AuthorizedUserCheck]
    public class PrintPopUpController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSWebApp.ITSService.ReferrerService.IReferrerService _referrerService;
        public PrintPopUpController(ITSService.CaseService.ICaseService caseService, ITSService.AssessmentService.IAssessmentService assessmentService, ITSService.LookUpService.ILookUpService lookUpService, ITSService.PatientService.IPatientService patientService, ITSService.ReferrerService.IReferrerService referrerService)
        {
            _caseService = caseService;
            _lookUpService = lookUpService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _referrerService = referrerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrintInitialAssessmentQA(int caseid)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseid));
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(caseid));
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
                    CaseID = caseid
                },
                // Practitioners = Mapper.Map<IEnumerable<Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierIDITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(caseid)).strAppointmentDate),
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(caseid)),
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),
                Durations = Mapper.Map<IEnumerable<Duration>>(_lookUpService.GetAllDuration()),

            };

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult PrintReviewAssessmentQA(int caseid)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseid));
            var caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(caseid));
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
                    CaseID = caseid
                },
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(caseid)),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),

            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintFinalAssessmentQA(int caseid)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseid));
            var caseObj = Mapper.Map<ITS.Domain.Models.CaseModel.Case>(_caseService.GetCaseByCaseID(caseid));
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
                    CaseID = caseid
                },
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                //DateOfInitialAssessment = DateTime.Now,
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(caseid)).strAppointmentDate),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
                CaseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(caseid)),
                PatientRole = Mapper.Map<IEnumerable<PatientRole>>(_lookUpService.GetAllPatientRole()),

            };

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult PrintFinalAssessment(int caseID, int? caseAssessmentDetailID)
        {
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: caseID));
            ITS.Domain.Models.CaseAssessment caseAssessment = new ITS.Domain.Models.CaseAssessment();
            if (caseAssessmentDetailID.HasValue)
                caseAssessment = Mapper.Map<ITS.Domain.Models.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseIDAndCaseAssessmentDetailID(caseID, caseAssessmentDetailID.Value));
            else
                caseAssessment = Mapper.Map<ITS.Domain.Models.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(caseID));
            caseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessions = caseAssessment.CaseAssessmentDetail.SessionsPatientAttended;   
            var caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(caseID));
            var pricingTypes = Mapper.Map<IEnumerable<ITS.Domain.Models.PricingTypes>>(_referrerService.GetAllPricingType());
            var viewModel = new ITS.Domain.ViewModels.PrintBlankFinalAssessmentViewModel
            {
                Patient = new ITS.Domain.Models.Patient
                {
                    FirstName = casePatientTreatment.FirstName,
                    LastName = casePatientTreatment.LastName,
                    BirthDate = casePatientTreatment.BirthDate,
                    InjuryDate = casePatientTreatment.InjuryDate
                },
                Case = new ITS.Domain.Models.Case
                {
                    CaseSubmittedDate = casePatientTreatment.CaseSubmittedDate,
                    CaseID = caseID
                },

                //PRACTIONER DROPDOWN IS NOT NEEDED FOR PRINT ASSESSMENT
                //Practitioners = Mapper.Map<IEnumerable<Model.Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierID(ITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PatientImpacts = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ITS.Domain.Models.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                //DateOfInitialAssessment = DateTime.Now,
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<ITS.Domain.Models.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(caseID)).strAppointmentDate),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<ITS.Domain.Models.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Duration = Mapper.Map<IEnumerable<ITS.Domain.Models.Duration>>(_lookUpService.GetAllDuration()),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<ITS.Domain.Models.CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(caseID)),
                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(caseID)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<ITS.Domain.Models.TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID))
            };
            return View(viewModel);
        }
 
    }
}