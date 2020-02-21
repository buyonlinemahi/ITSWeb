using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.Base;
using ITSWebApp.ITSService.AssessmentService;
using ITSWebApp.ITSService.ReferrerService;
using CaseAssessment = ITS.Domain.Models.CaseAssessment;
using PatientImpact = ITS.Domain.Models.PatientImpact;
using PatientImpactOnWork = ITS.Domain.Models.PatientImpactOnWork;
using PatientImpactValue = ITS.Domain.Models.PatientImpactValue;
using PatientLevelOfRecovery = ITS.Domain.Models.PatientLevelOfRecovery;
using PatientWorkstatus = ITS.Domain.Models.PatientWorkstatus;
using ProposedTreatmentMethod = ITS.Domain.Models.ProposedTreatmentMethod;
using PsychologicalFactor = ITS.Domain.Models.PsychologicalFactor;
using ReferrerProjectTreatmentPricing = ITS.Domain.Models.ReferrerProjectTreatmentPricing;
using TreatmentCategoriesPricingTypes = ITS.Domain.Models.TreatmentCategoriesPricingTypes;
using TreatmentCategoriesBespokeService = ITS.Domain.Models.TreatmentCategoriesBespokeService;
using ITS.Infrastructure.Global;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class InitialAssessmentQAController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITSService.PractitionerService.IPractitionerService _practitionerService;
        private readonly IReferrerService _referrerService;

        public InitialAssessmentQAController(ITSService.CaseService.ICaseService caseService, ITSService.AssessmentService.IAssessmentService assessmentService, ITSService.PatientService.IPatientService patientService, ITSService.PractitionerService.IPractitionerService practitionerService,
            ITSService.ReferrerService.IReferrerService referrerService)
        {
            _caseService = caseService;
            _assessmentService = assessmentService;
            _patientService = patientService;
            _practitionerService = practitionerService;
            _referrerService = referrerService;
        }
      

        public ActionResult Index()
        {
            //IEnumerable<CaseWorkflowPatientReferrerProject> caseWorkflowPatientReferrerProjectsForInitialAssessment =
            //   Mapper.Map<IEnumerable<CaseWorkflowPatientReferrerProject>>(_caseService.GetInitialAssessmentQACaseWorkflowPatientProjects());
            //foreach (var caseObj in caseWorkflowPatientReferrerProjectsForInitialAssessment)
            //{
            //    caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
            //                                              GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            //}
            //return View("~/Areas/Internal/Views/Shared/MainScreen/_InitialAssessmentPartial.cshtml", caseWorkflowPatientReferrerProjectsForInitialAssessment);
           return View("~/Areas/Internal/Views/Shared/MainScreen/_InitialAssessmentPartial.cshtml");
           

        }
        [HttpPost]
        public JsonResult GetInitialAssessmentQACaseWorkflowPatientProjects(int skip, int take)
        {
           PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForInitialAssessment =
                Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetInitialAssessmentQACaseWorkflowPatientProjects(skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForInitialAssessment);
        }

        [HttpPost]
        public JsonResult GetInitialAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForInitialAssessment = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetInitialAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID(treatmentCategoryID, skip, take));
            return Json(caseWorkflowPatientReferrerProjectsForInitialAssessment);
        }

       

        public ActionResult GetCaseAssessmentByCaseID(int id)
        {
            int caseID = id;
            var caseAssessment = Mapper.Map<CaseAssessment>(_assessmentService.GetCaseAssessmentByCaseID(caseID));
            //todo : needs to received referrerprojecttreatment
            var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID));

            var caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(caseID));
            var referrerProjectTreatmentID = caseObj.ReferrerProjectTreatmentID;

            var treatmentPricing = Mapper.Map<IEnumerable<ReferrerProjectTreatmentPricing>>(_referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentID(referrerProjectTreatmentID));

            var pricingTypes = Mapper.Map<IEnumerable<PricingTypes>>(_referrerService.GetAllPricingType());

            foreach (ReferrerProjectTreatmentPricing pricing in treatmentPricing)
            {
                pricing.PricingTypeName = pricingTypes.SingleOrDefault(p => p.PricingTypeID == pricing.PricingTypeID).PricingTypeName;
            }

            var viewModel = new InitialAssessmentQAViewModel
            {
                Patient = new Patient
                {
                    FirstName = casePatientTreatment.FirstName,
                    LastName = casePatientTreatment.LastName,
                    BirthDate = casePatientTreatment.BirthDate,
                    InjuryDate = casePatientTreatment.InjuryDate
                },
                Case = new Case
                {
                    CaseSubmittedDate = casePatientTreatment.CaseSubmittedDate,
                    CaseID = caseID
                },
                // Practitioners = Mapper.Map<IEnumerable<Practitioner>>(_practitionerService.GetPracitionersByTreatmentCategoryIDAndSupplierIDITSUser.SupplierID.Value, casePatientTreatment.TreatmentCategoryID)),
                PsychologicalFactors = Mapper.Map<IEnumerable<PsychologicalFactor>>(_assessmentService.GetAllPsychologicalFactors()),
                PatientImpacts = Mapper.Map<IEnumerable<PatientImpact>>(_assessmentService.GetAllPatientImpacts()),
                PatientImpactValues = Mapper.Map<IEnumerable<PatientImpactValue>>(_assessmentService.GetAllPatientImpactValues()),
                PatientWorkstatuses = Mapper.Map<IEnumerable<PatientWorkstatus>>(_assessmentService.GetAllPatientWorkstatus()),
                PatientImpactOnWorks = Mapper.Map<IEnumerable<PatientImpactOnWork>>(_assessmentService.GetAllPatientImpactOnWork()),
                PatientLevelOfRecoveries = Mapper.Map<IEnumerable<PatientLevelOfRecovery>>(_assessmentService.GetAllPatientLevelOfRecovery()),
                ProposedTreatmentMethods = Mapper.Map<IEnumerable<ProposedTreatmentMethod>>(_assessmentService.GetAllProposedTreatmentMethod()),
                CaseAssessment = caseAssessment,
                DateOfInitialAssessment = DateTime.Parse(Mapper.Map<CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID(caseID)).strAppointmentDate),
                ReffererProjectTreatmentPricings = treatmentPricing,
                TreatmentCategoriesBespokeServices = Mapper.Map<IEnumerable<TreatmentCategoriesBespokeService>>(_assessmentService.GetTreatmentCategoriesBespokeServicesByTreatmentCategoryID(caseObj.TreatmentTypeID)),
            };
            return View("~/Areas/Internal/Views/InitialAssessmentQA/InitialAssessment.cshtml", viewModel);
        }

        public ActionResult Update(InitialAssessmentQAViewModel viewModel)
        {
            viewModel.CaseAssessment.UserID = ITSUser.UserID;

            if (viewModel.CaseAssessment.IsAccepted)
            {
                viewModel.CaseAssessment.AssessmentAuthorisationID = 1;
                viewModel.CaseAssessment.DeniedMessage = null;
            }
            else
            {
                viewModel.CaseAssessment.AssessmentAuthorisationID = 3;
            }
            viewModel.CaseAssessment.AssessmentServiceID = 1;

            viewModel.CaseAssessment.CaseAssessmentRating.AssessmentServiceID = 1;
            viewModel.CaseAssessment.CaseAssessmentRating.RatingDate = DateTime.Now;

            _assessmentService.UpdateCaseAssessmentByCaseID(Mapper.Map<ITSService.AssessmentService.CaseAssessment>(viewModel.CaseAssessment));

            if (viewModel.CaseTreatmentPricings != null)
            {
                //viewModel.CaseTreatmentPricings.ToList().ForEach(o => o.Price = Convert.ToDecimal(o.PriceString));
                _caseService.AddCaseTreatmentPricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseTreatmentPricing>>(viewModel.CaseTreatmentPricings).ToArray());

             
            }

            if (viewModel.CaseBespokeServicePricings != null)
            {
                _caseService.AddCaseBespokeServicePricing(Mapper.Map<IEnumerable<ITSService.CaseService.CaseBespokeServicePricing>>(viewModel.CaseBespokeServicePricings).ToArray());

            }

            _caseService.UpdateCaseWorkFlow(viewModel.CaseAssessment.CaseID, ITSUser.UserID);
            return Json("success");
        }
    }
}