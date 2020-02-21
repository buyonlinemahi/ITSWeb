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
Purpous :created saprate controller for Delegated authorisation  tab and added the methods from referrersetupcontroller to delegated authorisation controller  
Version : 1.0
================================================
*/
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class ReferrerDelegateAuthorisationController : BaseController
    {
        
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public ReferrerDelegateAuthorisationController(ITSService.ReferrerService.IReferrerService referrerService)
        {
            _referrerService = referrerService;
        }
       
        [HttpPost]
        public JsonResult GetAllDelegatedAuthorisation(int referrerProjectTreatmentID)
        {
            IEnumerable<ReferrerProjectTreatmentAuthorisation> referrerProjectTreatmentAuthorisations = Mapper.Map<IEnumerable<ReferrerProjectTreatmentAuthorisation>>(_referrerService.GetReferrerProjectTreatmentAuthorisationByReferrerProjectTreatmentID(referrerProjectTreatmentID));
            IEnumerable<DelegatedAuthorisation> delegatedAuthorisations = Mapper.Map<IEnumerable<DelegatedAuthorisation>>(_referrerService.GetAllDelegatedAuthorisation());
            foreach (DelegatedAuthorisation delegateAuthorisation in delegatedAuthorisations)
            {
                ReferrerProjectTreatmentAuthorisation referrerProjectTreatmentAuthorisation = referrerProjectTreatmentAuthorisations.SingleOrDefault(treatmentAuthorisation => treatmentAuthorisation.TreatmentCategoryID == delegateAuthorisation.DelegatedAuthorisationID);
                delegateAuthorisation.TreatmentCategoryID = referrerProjectTreatmentAuthorisation == null ? 0 : referrerProjectTreatmentAuthorisation.DelegatedAuthorisationTypeID;
                delegateAuthorisation.Amount = referrerProjectTreatmentAuthorisation == null ? 0 : referrerProjectTreatmentAuthorisation.Amount;
                delegateAuthorisation.ReferrerProjectTreatmentAuthorisationID = referrerProjectTreatmentAuthorisation == null ? 0 : referrerProjectTreatmentAuthorisation.ReferrerProjectTreatmentAuthorisationID;
            }
            return Json(delegatedAuthorisations);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProjectTreatmentAuthorisation(IEnumerable<ReferrerProjectTreatmentAuthorisation> referrerProjectTreatmentAuthorisations)
        {
            ReferrerProjectTreatmentAuthorisation referrerProjectTreatmentAuthorisation = referrerProjectTreatmentAuthorisations.Single(authorisation => authorisation.ReferrerProjectTreatmentAuthorisationID != 0);
            _referrerService.UpdateReferrerProjectTreatmentAuthorisation(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation>(referrerProjectTreatmentAuthorisation));
            return Json(GlobalResource.UpdatedSuccessfully);
        }

    }
}
