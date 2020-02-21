using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Web.ITSService.SupplierService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Model = ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class InitialAssessmentCustomController : BaseController
    {
        //
        // GET: /Supplier/InitialAssessmentCustom/

        private readonly IPatientService _patientService;
        private readonly ICaseService _caseService;
        private readonly IReferrerService _referrerService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly IReferrerStorage _referrerReferrerStorage;
        private readonly SupplierDocument _supplierDocument;
        private readonly ISupplierService _supplierService;
        private readonly IEncryption _encryptionService;

        public InitialAssessmentCustomController(ICaseService caseService, IReferrerService referrerService, IPatientService patientService, ISupplierStorage supplierStorageService, IReferrerStorage referrerReferrerStorage, SupplierDocument supplierDocument, ISupplierService supplierService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _patientService = patientService;
            _supplierStorageService = supplierStorageService;
            _referrerReferrerStorage = referrerReferrerStorage;
            _supplierDocument = supplierDocument;
            _referrerService = referrerService;
            _supplierService = supplierService;
            _encryptionService = encryptionService;
        }

        public ActionResult Index(string id) 
        {
            int cid = Convert.ToInt32(DecryptString(id));
            InitialAssessmentCustomViewModel initialAssessmentCustomViewModel = new InitialAssessmentCustomViewModel();
            initialAssessmentCustomViewModel.CasePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(cid));
            initialAssessmentCustomViewModel.CaseService = Mapper.Map<Model.Case>(_caseService.GetCaseByCaseID(cid));

            initialAssessmentCustomViewModel.ReferrerDocument = Mapper.Map<IEnumerable<Model.ReferrerDocuments>>(_referrerService.GetReferrerDocumentsByCaseId(cid, (int)GlobalConst.ReferrerDocumentTypeID.InitialAssessmentCustom));
            initialAssessmentCustomViewModel.CasePatientTreatment.CaseID = cid;
            initialAssessmentCustomViewModel.CasePatientTreatment.EncryptCaseID = id;
            var SupplierDocment = initialAssessmentCustomViewModel.ReferrerDocument.FirstOrDefault();
            initialAssessmentCustomViewModel.InitialAssessmentCustomURLPath = SupplierDocment.UploadPath; //filestoragePath;
            return View(initialAssessmentCustomViewModel);
        }
        public ActionResult SaveInitialAssessmentCustom(InitialAssessmentCustomViewModel viewModel)
        {

            int userID = ITSUser.UserID;
            int CaseID = viewModel.CasePatientTreatment.CaseID;
            int ReferrerProjectTreatmentID = viewModel.CaseService.ReferrerProjectTreatmentID;

            if (viewModel.InitialAssessmentCustomUploadFile != null)
            {
                string UploaedDocmentName = (string)GlobalConst.SupplierDocumentType.InitialAssessmentSupplierCustom.ToString() + "_" + DateTime.Now.ToString("MMddyyhhmmss") + Path.GetExtension(viewModel.InitialAssessmentCustomUploadFile.FileName).ToString();
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                string filestoragePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath((int)viewModel.CaseService.SupplierID.Value, ReferrerProjectTreatmentID, CaseID, UploaedDocmentName.Trim(), storagePath);

                viewModel.InitialAssessmentCustomUploadFile.SaveAs(filestoragePath);
                // save the file path 
                _supplierDocument.SupplierID = (int)viewModel.CaseService.SupplierID.Value; ;
                _supplierDocument.ReferrerProjectTreatmentID = (int?)ReferrerProjectTreatmentID;
                _supplierDocument.UserID = userID;
                _supplierDocument.DocumentName = (string)GlobalConst.SupplierDocumentType.InitialAssessmentSupplierCustom;
                _supplierDocument.UploadPath = UploaedDocmentName;
                _supplierDocument.UploadDate = DateTime.Now;
                _supplierDocument.DocumentTypeID = (int)GlobalConst.SupplierrDocumentTypeID.InitialAssessmentSupplierCustom;
                _supplierDocument.CaseId = CaseID;
                int inerted = _supplierService.AddSupplierDocumentCustom(_supplierDocument);
                _caseService.UpdateCaseWorkflowCustomByCaseID(CaseID, ITSUser.UserID, (int)GlobalConst.WorkFlows.InitialAssessmentSubmittedtoInnovateCustom);
            }
            viewModel.InitialAssessmentCustomUploadFile = null;
            return Json(viewModel, "text/html");
        }

        public FileResult Download(string _caseID, string _path)
        {
            int caseID = int.Parse(DecryptString(_caseID).ToString());
            var _resCase = _caseService.GetCaseByCaseID(caseID);
            string strStoragePath = ConfigurationManager.AppSettings["StoragePath"];
            string filestoragePath =  Server.MapPath(_referrerReferrerStorage.GetReferralUploadFolderStoragePath(_resCase.ReferrerID, _resCase.ReferrerProjectTreatmentID, _path, strStoragePath.Trim()));

            WebClient client = new WebClient();
            return File(client.DownloadData(filestoragePath), "application/word", _path);
        }

        [HttpPost]
        public bool IsImage()
        {
            try
            {
                Stream stream = Request.Files[0].InputStream;

                //Read an image from the stream...
                var i = Image.FromStream(stream);
                //Move the pointer back to the beginning of the stream
                stream.Seek(0, SeekOrigin.Begin);

                if (ImageFormat.Jpeg.Equals(i.RawFormat) || ImageFormat.Png.Equals(i.RawFormat))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public bool IsXlsx()
        {
            string tempUpload = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]) + "/tempUpload/";
            if (!Directory.Exists(tempUpload))
                Directory.CreateDirectory(tempUpload);

            string tempPath = tempUpload + ITSUser.UserIDEncry + Request.Files[0].FileName;
            Request.Files[0].SaveAs(tempPath);

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(tempPath, false))
                {
                    spreadsheetDocument.Close();
                    if (System.IO.File.Exists(tempPath))
                        System.IO.File.Delete(tempPath);
                }
                return true;
            }
            catch 
            {
                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);
                return false;
            }
        }

        //To check if uploaded file has pdf content
        [HttpPost]
        public bool IsPdf()
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            Stream strFile = Request.Files[0].InputStream;

            while (remaining > 0)
            {
                var amtRead = strFile.Read(buf, pos, remaining);
                if (amtRead == 0) return false;
                remaining -= amtRead;
                pos += amtRead;
            }
            return pdfBytes.SequenceEqual(buf);
        }

        [HttpPost]
        public bool IsDocx()
        {
            string tempUpload = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]) + "/tempUpload/";
            if (!Directory.Exists(tempUpload))
                Directory.CreateDirectory(tempUpload);

            string tempPath = tempUpload + ITSUser.UserIDEncry + Request.Files[0].FileName;
            Request.Files[0].SaveAs(tempPath);

            try
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(tempPath, false))
                {
                    wordprocessingDocument.Close();
                    if (System.IO.File.Exists(tempPath))
                        System.IO.File.Delete(tempPath);
                }
                return true;
            }
            catch  
            {
                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);
                return false;
            }
        }
    }
}
