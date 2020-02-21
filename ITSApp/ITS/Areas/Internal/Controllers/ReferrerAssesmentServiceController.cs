using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;

/*
================================================
Latest  : Version 1.0
Author  : Harpreet Singh
Created On : 30-Jan-2013
Purpous :created saprate controller for assesment services  tab and added the methods from referrersetupcontroller to  assesment services  controller  
Version : 1.0
================================================
*/
namespace ITSWebApp.Areas.Internal.Controllers
{
    using ITS.Infrastructure.Global;

    public class ReferrerAssesmentServiceController : BaseController
    {
        
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        
        public ReferrerAssesmentServiceController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }

        [HttpPost]
        public JsonResult GetAllAssessmentService(int referrerProjectTreatmentID)
        {
            IEnumerable<ReferrerProjectTreatmentAssessment> projectTreatmentAssessments = Mapper.Map<IEnumerable<ReferrerProjectTreatmentAssessment>>(_referrerService.GetReferrerProjectTreatmentAssessmentByReferrerProjectTreatmentID(referrerProjectTreatmentID));
            IEnumerable<AssessmentService> assessmentServices = Mapper.Map<IEnumerable<AssessmentService>>(_referrerService.GetAllAssessmentService());
            foreach (AssessmentService assessmentService in assessmentServices)
            {
                ReferrerProjectTreatmentAssessment treatmentAssessment = projectTreatmentAssessments.SingleOrDefault(assessment => assessment.AssessmentServiceID == assessmentService.AssessmentServiceID);
                assessmentService.AssessmentTypeID = treatmentAssessment == null ? 0 : treatmentAssessment.AssessmentTypeID;
            }
            return Json(assessmentServices);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatmentAssessment(IEnumerable<AssessmentService> referrerProjectTreatmentAssessments)
        {
            IEnumerable<ReferrerProjectTreatmentAssessment> projectTreatmentAssessments = Mapper.Map<IEnumerable<ReferrerProjectTreatmentAssessment>>(_referrerService.GetReferrerProjectTreatmentAssessmentByReferrerProjectTreatmentID(referrerProjectTreatmentAssessments.FirstOrDefault().ReferrerProjectTreatmentID));

            foreach (ReferrerProjectTreatmentAssessment referrerProjectTreatmentAssessment in projectTreatmentAssessments)
            {
                AssessmentService assessmentService = referrerProjectTreatmentAssessments.SingleOrDefault((treatmentAssessment =>
                    (treatmentAssessment.AssessmentServiceID == referrerProjectTreatmentAssessment.AssessmentServiceID)
                    && (treatmentAssessment.AssessmentTypeID != referrerProjectTreatmentAssessment.AssessmentTypeID)));

                if (assessmentService != null)
                {
                    referrerProjectTreatmentAssessment.AssessmentTypeID = assessmentService.AssessmentTypeID;
                    _referrerService.UpdateReferrerProjectTreatmentAssignment(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentAssessment>(referrerProjectTreatmentAssessment));
                }
            }
            return Json(GlobalResource.UpdatedSuccessfully);
        }

    }
}
