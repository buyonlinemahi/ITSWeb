using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.ReferrerViewModel;
using AutoMapper;
using ITS.Domain.Models.ReferrerProjectTreatmentModel;
using ITS.Domain.Models.ReferrerModel;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Configuration;
using System.Collections;
using ITS.Domain.Models;



namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferrerProjectTreatmentController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly IReferrerStorage _referrerReferrerStorage;
        public ReferrerProjectTreatmentController(ITSService.ReferrerService.IReferrerService referrerService, IReferrerStorage referrerReferrerStorage)
        {
            _referrerService = referrerService;
            _referrerReferrerStorage = referrerReferrerStorage;
        }

        enum DelegateType
        {
            Session = 1,
            Cost = 2
        }


        [HttpGet]
        public ActionResult Index(int? referrerProjectTreatmentID)
        {
            if (!referrerProjectTreatmentID.HasValue)
                return RedirectToAction(GlobalConst.Actions.ReferrerController.Index, GlobalConst.Controllers.Referrer);
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            int ReferrerId = 0;
            ReferrerId = _referrerService.GetReferrerIDbyReferrerProjectTreatmentID(Convert.ToInt32(referrerProjectTreatmentID));
            var referrerProjectTreamentIndexViewModel = new ReferrerProjectTreamentIndexViewModel()
            {
                ProjectTreatment = Mapper.Map<ProjectTreatment>
                (_referrerService.GetReferrerProjectTreatmentByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value)),

                ProjectTreatmentDelegateAuthorisation = Mapper.Map<IEnumerable<ProjectTreatmentDelegateAuthorisation>>
                (_referrerService.GetReferrerProjectTreatmentAuthorisationByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value)),

                ProjectTreatmentInvoiceMethod = Mapper.Map<ProjectTreatmentInvoiceMethod>
                (_referrerService.GetReferrerProjectTreatmentInvoiceMethodByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value)),

                ProjectTreatmentSLA = Mapper.Map<IEnumerable<ProjectTreatmentSLAName>>
                (_referrerService.GetProjectTreatmentSLAsNameByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value)),

                ProjectTreatmentEmailSetup = Mapper.Map<IEnumerable<ProjectTreatmentEmailTypeName>>
                (_referrerService.GetReferrerProjectTreatmentEmailsTypeNameByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value)),

                ProjectTreatmentDocumentSetup = Mapper.Map<IEnumerable<ProjectTreatmentDocumentSetup>>
                (_referrerService.GetReferrerProjectTreatmentDocumentSetupByReferrerProjectTreatmentID(referrerProjectTreatmentID.Value))
            
            };
            foreach (var _projectTreatmentDocumentSetup in referrerProjectTreamentIndexViewModel.ProjectTreatmentDocumentSetup)
            {
                if (_projectTreatmentDocumentSetup.UploadedFileName != null)
                    _projectTreatmentDocumentSetup.UploadedFilePath = _referrerReferrerStorage.GetProjectTreatmentReferralUploadFolderStoragePath(ReferrerId, Convert.ToInt32(referrerProjectTreatmentID), storagePath, _projectTreatmentDocumentSetup.UploadedFileName);
            }            
            referrerProjectTreamentIndexViewModel.IsTriage = _referrerService.GetReferrerProjectByProjectID(referrerProjectTreamentIndexViewModel.ProjectTreatment.ReferrerProjectID).IsTriage;

            referrerProjectTreamentIndexViewModel.ProjectTreatmentPricing = Mapper.Map<IList<ITS.Domain.Models.ReferrerModel.TreatmentPricing>>
                      (_referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentIDAndTreatmentCategoryID(referrerProjectTreatmentID.Value, referrerProjectTreamentIndexViewModel.ProjectTreatment.TreatmentCategoryID));
                        referrerProjectTreamentIndexViewModel.ProjectTreatmentDelegateAuthorisation.ToList().ForEach(o =>
            {
                if (o.DelegatedAuthorisationTypeID == 1) { o.Name = "Session"; } else { o.Name = "Cost"; }
            });
            return View(referrerProjectTreamentIndexViewModel);
        }
    }
}
