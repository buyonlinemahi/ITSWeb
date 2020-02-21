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
Purpous :created saprate controller for email setup  tab and added the methods from referrersetupcontroller to email setup controller  
Version : 1.0
================================================
*/
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferrerEmailSetupController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        

        public ReferrerEmailSetupController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }
        
        [HttpPost]
        public JsonResult GetAllReferrerProjectTreatmentEmailsByReferrerProjectTreatmentID(int referrerProjectTreatmentID)
        {
            IEnumerable<ReferrerProjectTreatmentEmailSetUp> projectTreatmentAssessments = Mapper.Map<IEnumerable<ReferrerProjectTreatmentEmailSetUp>>(_referrerService.GetByReferrerProjectTreatmentId(referrerProjectTreatmentID));
            IEnumerable<EmailTypes> emailTypeServices = Mapper.Map<IEnumerable<EmailTypes>>(_referrerService.GetAllEmailType());
            foreach (EmailTypes emailType in emailTypeServices)
            {
                ReferrerProjectTreatmentEmailSetUp emailSetup = projectTreatmentAssessments.SingleOrDefault(treatmentAssessment => treatmentAssessment.EmailTypeID == emailType.EmailTypeID);
                emailType.EmailTypeValueID = emailSetup == null ? 0 : emailSetup.EmailTypeValueID;
            }

            return Json(emailTypeServices);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatmentEmailSetUp(IEnumerable<EmailTypes> referrerProjectTreatmentDocuments)
        {
            IEnumerable<ReferrerProjectTreatmentEmailSetUp> projectTreatmentAssessments = Mapper.Map<IEnumerable<ReferrerProjectTreatmentEmailSetUp>>(_referrerService.GetByReferrerProjectTreatmentId(referrerProjectTreatmentDocuments.FirstOrDefault().ReferrerProjectTreatmentID));

            foreach (ReferrerProjectTreatmentEmailSetUp _referrerProjectTreatmentDocument in projectTreatmentAssessments)
            {
                EmailTypes emailType = referrerProjectTreatmentDocuments.SingleOrDefault((treatmentAssessment =>
                    (treatmentAssessment.EmailTypeID == _referrerProjectTreatmentDocument.EmailTypeID)
                    && (treatmentAssessment.EmailTypeValueID != _referrerProjectTreatmentDocument.EmailTypeValueID)));

                if (emailType != null)
                {
                    _referrerProjectTreatmentDocument.EmailTypeValueID = emailType.EmailTypeValueID;
                    _referrerService.UpdateReferrerProjectTreatmentEmail(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentEmail>(_referrerProjectTreatmentDocument));
                }

            }

            return Json(GlobalResource.UpdatedSuccessfully);
        }

    }
}
