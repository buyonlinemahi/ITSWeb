using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using System.IO;
using System.Web.Configuration;
using ITS.Infrastructure.ApplicationServices;
using System.Configuration;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using System.Web.Script.Serialization;

#region Comments
/*
 * Latest Version 1.0
 * Author       : Pardeep Kumar
 * Date         : 01-Jan-2013
 * Version      : 1.0
 * 
 */
#endregion

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierClinicalAuditController : BaseController
    {
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.UserService.IUserService _userService;
        private readonly ITSWebApp.ITSService.PatientService.IPatientService _patientService;

        private readonly ITSWebApp.ITSService.CaseService.ICaseService _caseService;

        private readonly ISupplierStorage _supplierStorage;

        public static string fileName = "";

        public SupplierClinicalAuditController(ITSService.SupplierService.ISupplierService supplierService, ITSService.UserService.IUserService userService, ISupplierStorage supplierStorage, ITSWebApp.ITSService.PatientService.IPatientService patientService,
            ITSService.CaseService.ICaseService caseService
            )
        {
            _supplierService = supplierService;
            _userService = userService;
            _supplierStorage = supplierStorage;
            _patientService = patientService;
            _caseService = caseService;
        }

        [HttpPost]
        public JsonResult GetPatientNameByPatientId(int patientId)
        {
            Patient patient = Mapper.Map<Patient>(_patientService.GetPatientByPatientID(patientId));
            string patientName = patient.FirstName + "  " + patient.LastName;
            return Json(patientName);
        }

        [HttpPost]
        public JsonResult GetUserByUserTypeID(int userTypeID)
        {
            IEnumerable<ITSUser> itsUsers = Mapper.Map<IEnumerable<ITSUser>>(_userService.GetUserByUserTypeID(userTypeID));
            return Json(itsUsers);
        }

        [HttpPost]
        public JsonResult GetCaseByLikeCaseNumber(string caseNumber)
        {
            IEnumerable<Case> @case = Mapper.Map<IEnumerable<Case>>(_caseService.GetCaseByLikeCaseNumber(caseNumber));
            return Json(@case);
        }


        [HttpPost]
        public JsonResult GetPatientNameByCaseId(int caseId)
        {
            Case @case = Mapper.Map<Case>(_caseService.GetCaseByCaseID(caseId));
            return GetPatientNameByPatientId(@case.PatientID);
        }


        [HttpPost]
        public JsonResult GetSupplierClinicalAuditBySupplierID(int supplierId)
        {
            IEnumerable<SupplierClinicalAudit> supplierClinicalAudits = Mapper.Map<IEnumerable<SupplierClinicalAudit>>(_supplierService.GetSupplierClinicalAuditBySupplierID(supplierId));
            IEnumerable<SupplierDocumentUser> supplierDocumentUser = Mapper.Map<IEnumerable<SupplierDocumentUser>>(_supplierService.GetSupplierDocumentBySupplierIDAndDocumentTypeID(supplierId, 4));
            foreach (SupplierClinicalAudit supplierClinicalAudit in supplierClinicalAudits)
            {
                SupplierDocumentUser supplierDocument = supplierDocumentUser.SingleOrDefault(x => x.SupplierDocumentID == supplierClinicalAudit.SupplierDocumentID);
                supplierClinicalAudit.CaseNumber = Mapper.Map<Case>(_caseService.GetCaseByCaseID(supplierClinicalAudit.CaseID)).CaseNumber.ToString();
                supplierClinicalAudit.UploadPath = supplierDocument == null ? "" : supplierDocument.UploadPath;
                supplierClinicalAudit.DocumentName = supplierDocument == null ? "" : supplierDocument.DocumentName;
                supplierClinicalAudit.DocumentUrl = Url.Action(GlobalConst.Actions.Area.Internal.SupplierClinicalAuditController.ViewClinicalFile, GlobalConst.Controllers.Area.Internal.SupplierClinicalAudit,
                    new { fileName = supplierDocument.UploadPath, supplierID = supplierDocument.SupplierID, area = GlobalConst.Areas.Internal });
            }
            return Json(supplierClinicalAudits);
        }
        // method to Add new SupplierClinical
    public ActionResult AddSupplierClinicalAudit(SupplierClinicalAudit supplierClinicalAudit, SupplierDocument supplierDocument)
        {

            string returnMessage = "";
            supplierDocument.UploadDate = DateTime.Now.Date;
            if (supplierClinicalAudit.ClinicalAuditDocumentFileUpload != null)
            {
                supplierDocument.UploadPath = AddSupplierClinicalAuditFile(supplierClinicalAudit.ClinicalAuditDocumentFileUpload, supplierClinicalAudit.SupplierID);
                int id = _supplierService.AddSupplierClinicalAuditAndDocument(Mapper.Map<ITSService.SupplierService.SupplierClinicalAudit>(supplierClinicalAudit), Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                returnMessage = GlobalResource.AddedSuccessfully;
            }
            else
            {
                returnMessage = GlobalResource.UnableToCompleteAction;
            }
            return Content(returnMessage);

        }


        // method to Update SupplierClinical
        [HttpPost]
        public ActionResult UpdateSupplierClinicalAudit(SupplierClinicalAudit supplierClinicalAudit, SupplierDocument supplierDocument)
        {
            supplierDocument.UploadDate = DateTime.Now.Date;
            if (supplierClinicalAudit.ClinicalAuditDocumentFileUpload != null)
            {
                supplierDocument.UploadPath = AddSupplierClinicalAuditFile(supplierClinicalAudit.ClinicalAuditDocumentFileUpload, supplierClinicalAudit.SupplierID);
                System.IO.File.Delete(GetSupplierClinicalFileStoragePath(supplierClinicalAudit.SupplierID, supplierClinicalAudit.ClinicalAuditOldFileName));
            }
            int id = _supplierService.UpdateSupplierClinicalAuditBySupplierClinicalAuditID(Mapper.Map<ITSService.SupplierService.SupplierClinicalAudit>(supplierClinicalAudit));
            _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
            return Content(GlobalResource.UpdatedSuccessfully);
        }

        // method to Insert New File
        [NonAction]
        public string AddSupplierClinicalAuditFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorage.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.SupplierClinicalAudit);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }




        [HttpPost]
        public JsonResult DeleteSupplierClinicalAuditBySupplierClinicalAuditID(int supplierClinicalAuditID, int supplierDocumentID, string fileToDelete, int SupplierID)
        {
            _supplierService.DeleteSupplierClinicalAuditBySupplierClinicalAuditID(supplierClinicalAuditID);
            _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierDocumentID);
            if (fileToDelete != "" && fileToDelete != String.Empty)
            {
                var path = GetSupplierClinicalFileStoragePath(SupplierID, fileToDelete);
                System.IO.File.Delete(path);
            }
            return Json(GlobalResource.DeletedSuccessfully);
        }

        [NonAction]
        public string GetSupplierClinicalFileStoragePath(int supplierID, string fileName)
        {
            return _supplierStorage.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, fileName, GlobalConst.SupplierDocumentType.SupplierClinicalAudit);
        }

        public FileResult ViewClinicalFile(string fileName, int supplierID)
        {
            string uploadPath = GetSupplierClinicalFileStoragePath(supplierID, fileName);
            return File(uploadPath, GlobalConst.ContentTypes.PDF, fileName);
        }
    }
}
