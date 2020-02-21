using AutoMapper;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Web.ITSService.AssessmentService;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.LookUpService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.PractitionerService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Controllers
{
    [AuthorizedUserCheck]
    public class PrintPopUpController : BaseController
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IPatientService _patientService;
        private readonly ICaseService _caseService;
        private readonly IPractitionerService _practitionerService;
        private readonly ILookUpService _lookUpService;
        private readonly IReferrerService _referrerService;
        private readonly IEncryption _encryptionService;

        public PrintPopUpController(ICaseService caseService, IAssessmentService assessmentService, IPatientService patientService,
                  IPractitionerService practitionerService, ILookUpService lookUpService, IReferrerService referrerService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _practitionerService = practitionerService;
            _lookUpService = lookUpService;
            _referrerService = referrerService;
            _encryptionService = encryptionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrintBlankInitialAssessment(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            var viewModel = new PrintBlankInitialAssessmentViewModel()
            {
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
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
                    CaseID = cid
                },

            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintInitialAssessment(string caseID, string caseAssessmentDetailID)
        {
            int? _caseAssessmentDetailID = null;
            if (caseAssessmentDetailID != null)
                _caseAssessmentDetailID = Convert.ToInt32(DecryptString(caseAssessmentDetailID));
            int cid = Convert.ToInt32(DecryptString(caseID));

            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));
            Model.CaseAssessment caseAssessment = new Model.CaseAssessment();

            if (_caseAssessmentDetailID.HasValue)
            {
                caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseIDAndCaseAssessmentDetailID(cid, _caseAssessmentDetailID.Value));
                caseAssessment.CaseAssessmentPatientInjuriesBL = Mapper.Map<IList<Model.CaseAssessmentPatientInjuryBL>>(_assessmentService.GetCaseAssessmentPatientInjuriesByCaseAssessmentDetailID(_caseAssessmentDetailID.Value));
            }
            else
                caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(cid));
            var caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(cid));
            var pricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.PricingTypes>>(_referrerService.GetAllPricingType());
            var viewModel = new PrintBlankInitialAssessmentViewModel
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
                    CaseID = cid
                },

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
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(cid)),
                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(cid)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID))
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintBlankReviewAssessment(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));

            var viewModel = new PrintBlankReviewAssessmentViewModel
            {
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
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
                    CaseID = cid
                }

            };
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult PrintReviewAssessment(string caseID, string caseAssessmentDetailID)
        {
            int? _caseAssessmentDetailID = null;
            if (caseAssessmentDetailID != null)
                _caseAssessmentDetailID = Convert.ToInt32(DecryptString(caseAssessmentDetailID));
            int _caseID = Convert.ToInt32(DecryptString(caseID));

            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: _caseID));
            Model.CaseAssessment caseAssessment = new Model.CaseAssessment();
            if (_caseAssessmentDetailID.HasValue)
            {
                caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseIDAndCaseAssessmentDetailID(_caseID, _caseAssessmentDetailID.Value));
                caseAssessment.CaseAssessmentPatientInjuriesBL = Mapper.Map<IList<Model.CaseAssessmentPatientInjuryBL>>(_assessmentService.GetCaseAssessmentPatientInjuriesByCaseAssessmentDetailID(_caseAssessmentDetailID.Value));
            }
            else
                caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(_caseID));                      
            caseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessions = caseAssessment.CaseAssessmentDetail.SessionsPatientAttended;
            var caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(_caseID));
            var pricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.PricingTypes>>(_referrerService.GetAllPricingType());
            var viewModel = new PrintBlankReviewAssessmentViewModel
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
                    CaseID = _caseID
                },
                //PRACTIONER DROPDOWN IS NOT NEEDED FOR PRINT ASSESSMENT
                
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                //DateOfInitialAssessment = DateTime.Now,
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(_caseID)).strAppointmentDate),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<Model.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Duration = Mapper.Map<IEnumerable<Model.Duration>>(_lookUpService.GetAllDuration()),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(_caseID)),
                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(_caseID)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID))
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult PrintFinalAssessment(string caseID, string caseAssessmentDetailID)
        {
            int? _caseAssessmentDetailID = null;
            if (caseAssessmentDetailID != null)
                _caseAssessmentDetailID = Convert.ToInt32(DecryptString(caseAssessmentDetailID));
            int _caseID = Convert.ToInt32(DecryptString(caseID));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: _caseID));
            Model.CaseAssessment caseAssessment = new Model.CaseAssessment();
            if (_caseAssessmentDetailID.HasValue)
                if (caseAssessment!=null)
                {
                    caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseIDAndCaseAssessmentDetailID(_caseID, _caseAssessmentDetailID.Value));
                    caseAssessment.CaseAssessmentPatientInjuriesBL = Mapper.Map<IList<Model.CaseAssessmentPatientInjuryBL>>(_assessmentService.GetCaseAssessmentPatientInjuriesByCaseAssessmentDetailID(_caseAssessmentDetailID.Value));
                }
            else
                    caseAssessment = Mapper.Map<Model.CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(_caseID));                  
            caseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessions = caseAssessment.CaseAssessmentDetail.SessionsPatientAttended;
            var caseObj = Mapper.Map<ITSPublicApp.Domain.Models.Case>(_caseService.GetCaseByCaseID(_caseID));
            var pricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.PricingTypes>>(_referrerService.GetAllPricingType());


            var viewModel = new PrintBlankReviewAssessmentViewModel
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
                    CaseID = _caseID
                },

                ////PRACTIONER DROPDOWN IS NOT NEEDED FOR PRINT ASSESSMENT
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<Model.PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<Model.PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<Model.PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<Model.PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<Model.ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                //DateOfInitialAssessment = DateTime.Now,
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(_caseID)).strAppointmentDate),
                CaseAssessment = caseAssessment,
                PatientRole = Mapper.Map<IEnumerable<Model.PatientRole>>(_lookUpService.GetAllPatientRole()),
                Duration = Mapper.Map<IEnumerable<Model.Duration>>(_lookUpService.GetAllDuration()),
                CaseBespokeServicePricings = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseBespokeServicePricing>>(_caseService.GetCaseBespokeServicePricingByCaseID(_caseID)),

                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDAndInComplete(_caseID)),
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID))

            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PrintBlankFinalAssessment(string caseid)
        {
            int cid = Convert.ToInt32(DecryptString(caseid));
            var casePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID: cid));

            var viewModel = new PrintBlankFinalAssessmentViewModel
            {
                PatientImpacts = Mapper.Map<IEnumerable<Model.PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
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
                    CaseID = cid
                }

            };
            return View(viewModel);
        }
    }


    //what is this? why is this here?
    public static class PrintPopUpExtension
    {
        public static string ToYesNo(this bool boolValue)
        {
            return (boolValue ? "Yes" : "No");
        }
    }
}
