using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Domain.Models.CaseSearch;
using AutoMapper;
using System.Configuration;
using ITS.Infrastructure.Global;
using System.IO;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.ApplicationServices;
using ITS.Domain.Models.SolicitorModel;
using ITS.Domain.ViewModels.CaseSearchViewModel;
using ITS.Infrastructure.ApplicationServices.Contracts;
using RTE;
using System.Web.UI.WebControls;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    public class CaseSearchSharedController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly EmailService _emailService;
        private readonly ITSService.SolicitorService.ISolicitorService _solicitorService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ISupplierStorage _supplierStorageService;

        public CaseSearchSharedController(ITSService.CaseService.ICaseService caseService, EmailService emailService, ITSService.SolicitorService.ISolicitorService solicitorService,
            ITSService.PatientService.IPatientService patientService, ISupplierStorage supplierStorageService)
        {
            _caseService = caseService;
            _emailService = emailService;
            _solicitorService = solicitorService;
            _patientService = patientService;
            _supplierStorageService = supplierStorageService;
        }

        public ActionResult Index()
        {
            return View();           
        }
        [HttpPost]
        public ActionResult AddCaseNote(CaseNoteUser caseNote)
        {
            caseNote.UserID = ITSUser.UserID;
            caseNote.FirstName = ITSUser.FirstName;
            caseNote.LastName = ITSUser.LastName;
            _caseService.AddCaseNote(Mapper.Map<ITSService.CaseService.CaseNote>(caseNote));
            caseNote.DateAdded = DateTime.Now;
            return Json(caseNote, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCaseUploads(CaseDocumentUser caseDocument, HttpPostedFileBase DocumentFileUpload)
        {

            if (DocumentFileUpload != null)
            {
                string filename = AddDocumentFile(DocumentFileUpload);
                string extensionfileName = filename.Substring(filename.LastIndexOf('.'));
                caseDocument.DocumentName = caseDocument.DocumentName + extensionfileName;
                caseDocument.UserID = ITSUser.UserID;
                caseDocument.DocumentTypeID = GlobalConst.SupplierDocumentTypeId.Misc;
                caseDocument.UploadDate = DateTime.Now;
                caseDocument.FirstName = ITSUser.FirstName;
                caseDocument.LastName = ITSUser.LastName;
                caseDocument.UploadPath = filename;
                caseDocument.CaseDocumentID = _caseService.AddCaseDocument(Mapper.Map<ITSService.CaseService.CaseDocument>(caseDocument));
               
            }
            return Json(caseDocument, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }


        private string AddDocumentFile(HttpPostedFileBase file)
        {

            if (!Directory.Exists(Server.MapPath(GlobalConst.CaseDocumentStoragePath)))
            {
                Directory.CreateDirectory(Server.MapPath(GlobalConst.CaseDocumentStoragePath));
            }
            var path = Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, Guid.NewGuid().ToString() + Path.GetFileName(file.FileName)));

            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }

        [HttpPost]
        public ActionResult ResendEmail(CaseCommunicationHistoryUser caseCommunicationHistory)
        {
            System.Net.Mail.Attachment obj = null;
            if (caseCommunicationHistory.UploadPath.Contains("_"))
            {
                string[] strArr = null;
                char[] splitchar = { '_' };
                strArr = caseCommunicationHistory.UploadPath.Split(splitchar);
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                string filePath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePath(int.Parse(strArr[1].ToString()), int.Parse(strArr[2].ToString()), int.Parse(strArr[3].ToString()), strArr[0].ToString(), storagePath);
                string UploadPath = strArr[0].ToString();
                obj = new System.Net.Mail.Attachment(filePath);
                bool result = _emailService.SendGeneralEmail(caseCommunicationHistory.SentTo, caseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), caseCommunicationHistory.Subject,
                         caseCommunicationHistory.Message, obj);
            }
            else
            {
                caseCommunicationHistory.UserID = ITSUser.UserID;
                
                if (caseCommunicationHistory.UploadPath != null)
                {
                    obj = new System.Net.Mail.Attachment(Server.MapPath(Path.Combine(GlobalConst.CaseDocumentStoragePath, caseCommunicationHistory.UploadPath)));
                }

                bool result = _emailService.SendGeneralEmail(caseCommunicationHistory.SentTo, caseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), caseCommunicationHistory.Subject,
                         caseCommunicationHistory.Message, obj);

                if (result)
                {
                    _caseService.AddCaseCommunicationHistory(Mapper.Map<ITSService.CaseService.CaseCommunicationHistory>(caseCommunicationHistory));
                }
                caseCommunicationHistory.FirstName = ITSUser.FirstName;
                caseCommunicationHistory.LastName = ITSUser.LastName;
            }
            return Json(caseCommunicationHistory, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }
        [HttpPost]
        public ActionResult UpdateSolicitor(Solicitor solicitor)
        {
            if (_solicitorService.UpdateSolicitorBySolicitorID(Mapper.Map<ITSService.SolicitorService.Solicitor>(solicitor)) > 0)
            {
                return Json("Successfully Updated", GlobalConst.ContentTypes.TextHTML);
            }
            return Json("Error occor please try again", GlobalConst.ContentTypes.TextHTML);


        }
       

        [HttpPost]
        public ActionResult SaveUploadCheckData(IEnumerable<CaseDocumentUser> CaseDocumentUsers)
        {
            CaseDocumentUser obj = new CaseDocumentUser();
            int Result;
            foreach(var cases in CaseDocumentUsers)
            {
                if (cases.SupplierCheck == null)
                {
                    cases.SupplierCheck = false;
                 }
                if (cases.ReferrerCheck == null)
                {
                    cases.ReferrerCheck = false;
                }
                obj = cases;
                Result = _caseService.UpdateCaseAndReferrerDocumentByCaseID(Mapper.Map<ITSService.CaseService.CaseDocumentUser>(obj));
            }
            return Json(obj);
        }
    }
}