using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
/*
================================================
Latest  : Version 1.0
Author  : Harpreet Singh
Created On : 30-Jan-2013
Purpous :created saprate controller for Document setup  tab and added the methods from referrersetupcontroller to Document setup controller  
Version : 1.0
================================================
*/
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferrerDocumentSetupController : BaseController
    {
        
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        public ReferrerDocumentSetupController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }

        [HttpPost]
        public JsonResult GetAllReferrerProjectTreatmentDocumentsByReferrerProjectTreatmentID(int referrerProjectTreatmentID)
        {
            //MARKED: candidate for optimization -ftan 
            IEnumerable<ReferrerProjectTreatmentDocumentSetUp> referrerProjectTreatmentDocumentSetUps = Mapper.Map<IEnumerable<ReferrerProjectTreatmentDocumentSetUp>>(_referrerService.GetReferrerProjectTreatmentDocumentSetupByReferrerProjectTreatmentID(referrerProjectTreatmentID));
            IEnumerable<AssessmentService> assmentServices = Mapper.Map<IEnumerable<AssessmentService>>(_referrerService.GetAllAssessmentService());
            foreach (AssessmentService assessmentService in assmentServices)
            {
                ReferrerProjectTreatmentDocumentSetUp document = referrerProjectTreatmentDocumentSetUps.SingleOrDefault(treatmentAssessment => treatmentAssessment.AssessmentServiceID == assessmentService.AssessmentServiceID);
                document.AssessmentServiceName = assessmentService.AssessmentServiceName;
            }
            return Json(referrerProjectTreatmentDocumentSetUps);
        }
        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatmentDocumentSetUp(IEnumerable<ReferrerProjectTreatmentDocumentSetUp> referrerProjectTreatmentDocuments)
        {
            foreach (ReferrerProjectTreatmentDocumentSetUp document in referrerProjectTreatmentDocuments)
            {
                _referrerService.UpdateReferrerProjectTreatmentDocumentSetup(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup>(document));
            }
            return Json(GlobalResource.UpdatedSuccessfully);
        }
       
    }
}
