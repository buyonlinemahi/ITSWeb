using AutoMapper;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Case = ITSPublicApp.Domain.Models.Case;


#region comments

/*
 *
 * Latest Version : 1.01
 * 
 * Modified By : Pardeep Kumar
 * Date        : 26-June-2013
 * Description : Added ActionResult (DownloadTraigeAssessmentForm) to download BlankTriageAssessmentForm
 * Version     : 1.01
 * 
 * 
 */

#endregion


namespace ITSPublicApp.Web.Controllers
{
        [AuthorizedUserCheck]
    public class FileController : BaseController
    {
        private ICaseService _caseService;
        private readonly IReferrerStorage _referrerStorage;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IEncryption _encryptionService;
        //
        // GET: /File/

        public FileController(ICaseService caseService, IReferrerStorage referrerStorage, ISupplierStorage supplierStorageService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _referrerStorage = referrerStorage;
            _supplierStorageService = supplierStorageService;
            _encryptionService = encryptionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewCaseUploads(string UploadPath, string DocumentName)
        {
            var filePath = "";

            if (UploadPath.Contains("|"))
            {
                string[] strArr = null;
                char[] splitchar = { '|' };
                strArr = UploadPath.Split(splitchar);

                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);

                filePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath(int.Parse(strArr[1].ToString()), int.Parse(strArr[2].ToString()), int.Parse(strArr[3].ToString()), strArr[0].ToString(), storagePath);
            }
            else
            {
                filePath = Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, UploadPath));
            }
            if (Path.GetExtension(filePath) == ".doc" || Path.GetExtension(filePath) == ".docx")
                return File(filePath, "application/msword", DocumentName);
            else
                return File(filePath, "application/pdf", DocumentName);
        }

        [HttpGet]
        public ActionResult ViewReferral(string caseID)
        {
            int cid = Convert.ToInt32(DecryptString(caseID));
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(cid));
            var fileName = caseObj.CaseNumber.ToString();
            var filePath =
                string.Format(
                    "{0}.{1}",
                    _referrerStorage.GetReferrerReferralsStoragePath(caseObj.ReferrerID,
                                                                     caseObj.ReferrerProjectTreatmentID, cid,
                                                                     fileName,
                                                                     ConfigurationManager.AppSettings["StoragePath"]),
                    "pdf");
            return File(filePath, "application/pdf", caseObj.CaseNumber.ToString());
        }

        [HttpGet]
        public ActionResult DownloadTraigeAssessmentForm()
        {


            var filePath = ConfigurationManager.AppSettings["TriageAssessmentForm"];
            return File(filePath, "application/pdf", "TriageAssessmentForm");
        }
    }
}
