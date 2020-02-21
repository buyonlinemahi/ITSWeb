using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Domain.Models.ReferrerModel;
using ITS.Domain.Models.ReferrerProjectTreatmentModel;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Domain.Models;
using System.Configuration;
using System.IO;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.Base;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferrerProjectTreatmentSharedController : BaseController
    {
        //
        // GET: /ReferrerProjectTreatmentShared/
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly IReferrerStorage _referrerStorage;
        public ReferrerProjectTreatmentSharedController(ITSService.ReferrerService.IReferrerService referrerService, IReferrerStorage referrerStorage)
        {
            _referrerService = referrerService;
            _referrerStorage = referrerStorage;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateDelegateAuthorisation(IEnumerable<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentDelegateAuthorisation> delegatedAuthorisations)
        {
            foreach (var item in delegatedAuthorisations)
            {
                    _referrerService.UpdateReferrerProjectTreatmentAuthorisation(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation>(item));
            }
            return Json(delegatedAuthorisations, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateTreatmentCategoryPricing(IEnumerable<ITS.Domain.Models.ReferrerModel.TreatmentPricing> pricing)
        {

            foreach (ITS.Domain.Models.ReferrerModel.TreatmentPricing treatmentPricing in pricing)
            {
                _referrerService.UpdateReferrerProjectTreatmentPricingByPricingID(
                    Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(treatmentPricing));
            }
            return Json(pricing, "text/html");
        }


        [HttpPost]
        public ActionResult UpdateReferrerProjectTreatment(ProjectTreatment projectTreatment)
        {
            _referrerService.UpdateReferrerProjectTreatment(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatment>(projectTreatment));
            return Json(projectTreatment, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateInvoiceMethod(ProjectTreatmentInvoiceMethod projectTreatmentInvoiceMethod)
        {
            _referrerService.UpdateReferrerProjectTreatmentInvoice(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentInvoice>(projectTreatmentInvoiceMethod));

            return Json(projectTreatmentInvoiceMethod, "text/html");
        }


        [HttpPost]
        public ActionResult UpdateProjectTreatmentSLA(IEnumerable<ProjectTreatmentSLAName> projectTreatmentSLAs)
        {
            foreach (ProjectTreatmentSLAName treatmentSLA in projectTreatmentSLAs)
            {
                _referrerService.UpdateProjectTreatmentSLAsByProjectTreatmentSLAID(Mapper.Map<ITSService.ReferrerService.ProjectTreatmentSLA>(treatmentSLA));
            }
            return Json(projectTreatmentSLAs, "text/html");
        }

        [HttpPost]
        public ActionResult UpdateReferrerProjectTreatmentEmail(IEnumerable<ProjectTreatmentEmailTypeName> projectTreatmentEmailSetups)
        {
            foreach (ProjectTreatmentEmailTypeName projectTreatmentEmailSetup in projectTreatmentEmailSetups)
            {
                _referrerService.UpdateReferrerProjectTreatmentEmail(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentEmail>(projectTreatmentEmailSetup));
            }

            return Json(projectTreatmentEmailSetups, "text/html");
        }

         [HttpPost]
        public ActionResult UpdateDocumentSetupProjectTreatment(IEnumerable<ReferrerProjectTreatmentDocumentSetUp> referrerProjectTreatmentDocumentSetUp)
        {
            int documenttype_id = 0;
            var filename = "";
            foreach (ReferrerProjectTreatmentDocumentSetUp document in referrerProjectTreatmentDocumentSetUp)
            {
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                string storagePathLocalhost = ConfigurationManager.AppSettings["StoragePath"];
                if (document.AssesmentFile != null)
                {
                    if (document.AssessmentServiceID == GlobalConst.AssessmentService.InitialAssessment)
                    {
                        documenttype_id = GlobalConst.CaseDocumentTypeId.InitialAssessmentCustom;
                        //filename = "InitialAssessmentCustom" + Path.GetExtension(document.AssesmentFile.FileName);
                        filename = Path.GetFileName(document.AssesmentFile.FileName);
                        document.UploadedFileName = filename;
                    }
                    else if (document.AssessmentServiceID == GlobalConst.AssessmentService.ReviewAssessment)
                    {
                        documenttype_id = GlobalConst.CaseDocumentTypeId.ReviewAssessmentCustom;
                        filename = Path.GetFileName(document.AssesmentFile.FileName);
                        //filename = "ReviewAssessmentCustom" + Path.GetExtension(document.AssesmentFile.FileName);
                        document.UploadedFileName = filename;
                    }
                    else if (document.AssessmentServiceID == GlobalConst.AssessmentService.FinalAssessment)
                    {
                        documenttype_id = GlobalConst.CaseDocumentTypeId.FinalAssessmentCustom;
                        filename = Path.GetFileName(document.AssesmentFile.FileName);
                        //filename = "FinalAssessmentCustom" + Path.GetExtension(document.AssesmentFile.FileName);
                        document.UploadedFileName = filename;
                    }
                   
                    int r_id = _referrerService.GetReferrerIDbyReferrerProjectTreatmentID(document.ReferrerProjectTreatmentID);
                    var pathcreate = _referrerStorage.CreateProjectTreatmentReferralUploadFolder(r_id, document.ReferrerProjectTreatmentID, storagePath);
                    var path = _referrerStorage.GetProjectTreatmentReferralUploadFolderStoragePath(r_id, document.ReferrerProjectTreatmentID, storagePath, filename);
                    var Localhostpath = _referrerStorage.GetProjectTreatmentReferralUploadFolderStoragePath(r_id, document.ReferrerProjectTreatmentID, storagePathLocalhost, filename);
                    document.UploadedFilePath = Localhostpath;
                    document.AssesmentFile.SaveAs(path);
                    int countFileExsit = _referrerService.GetReferrerDocumentsByReferrerIDDocumentTypeIDAndReferrerProjectTreatmentID(r_id, documenttype_id, document.ReferrerProjectTreatmentID).Count();
                   if (countFileExsit == 0)
                   {
                       _referrerService.AddReferrerDocumentCustom(new ITSService.ReferrerService.ReferrerDocument { UploadDate = DateTime.Now, UserID = ITSUser.UserID, UploadPath = filename, ReferrerID = r_id, DocumentTypeID = documenttype_id, ReferrerProjectTreatmentID = (int?)document.ReferrerProjectTreatmentID });

                   }
                   else
                   {
                         _referrerService.UpdateReferrerDocument(new ITSService.ReferrerService.ReferrerDocument { UploadDate = DateTime.Now, UserID = ITSUser.UserID, UploadPath = filename, ReferrerID = r_id, DocumentTypeID = documenttype_id, ReferrerProjectTreatmentID = (int?)document.ReferrerProjectTreatmentID });

                   }
                }
                _referrerService.UpdateReferrerProjectTreatmentDocumentSetup(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup>(document));
                document.AssesmentFile = null;
            }
            return Json(referrerProjectTreatmentDocumentSetUp, "text/html");
         }
    }
}
