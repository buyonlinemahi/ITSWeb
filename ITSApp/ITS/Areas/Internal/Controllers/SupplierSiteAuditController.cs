using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using System.IO;
using ITS.Infrastructure.ApplicationServices;
using System.Web.Configuration;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Configuration;

/*
 * Latest Version 1.7
 * 
 * Added By     : Manjit Singh
 * Date         : 27-Dec-2012
 * Purpose      : to get GetSupplierSiteAuditBySupplierID and GetUserByUserID
 * Version      : 1.0
 * 
 * Updated By   : Manjit Singh
 * Date         : 28-Dec-2012
 * Purpose      : Method to add record for SupplierSiteAudit
 * Version      : 1.1
 * 
 * Updated By   : Manjit Singh
 * Date         : 29-Dec-2012
 * Purpose      : Methods to delete and update Supplier Site Audit
 * Version      : 1.2
 * 
 *Updated By    : Manjit Singh
 *Date          : 3-Jan-2013
 *Version       : 1.3
 *Purpose       : Functionality to get users by user type ID
 
 *Updated By    : Manjit Singh
 *Date          : 7-Jan-2013
 *Version       : 1.4
 *Purpose       : Functionality view upload and methods changed for GetSupplierSiteAuditBySupplierID and DeleteSupplierSiteAuditBySupplierSiteAuditID
 
 *Updated By    : Manjit Singh
 *Date          : 11-Feb-2013
 *Version       : 1.5
 *Purpose       : View Model Changed to get Users by Type ID
 
 *Updated By    : Manjit Singh
 *Date          : 15-Feb-2013
 *Version       : 1.6
 *Purpose       : SupplierDocument View Model Replaced with SupplierDocumentUser to get GetSupplierSiteAuditBySupplierID,
                  some functionality changed for upload file, View Upload method replaced with GetSiteAuditFile
 
 *Updated By    : Manjit Singh
 *Date          : 19-Feb-2013
 *Version       : 1.7
 *Purpose       : Methods Chnage to AddSupplierSiteAuditFile, AddSupplierSiteAudit, UpdateSupplierSiteAuditBySupplierSiteAuditID
 
 * */
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierSiteAuditController : BaseController
    {
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.UserService.IUserService _userService;
        private readonly ISupplierStorage _supplierStorage;
        public SupplierSiteAuditController(ITSService.SupplierService.ISupplierService supplierService, ITSService.UserService.IUserService userService, ISupplierStorage supplierStorage)
        {
            _supplierService = supplierService;
            _userService = userService;
            _supplierStorage = supplierStorage;
        }

        [HttpPost]
        public JsonResult GetSupplierSiteAuditBySupplierID(int supplierId, int documentTypeID)
        {
            IEnumerable<SupplierSiteAudit> supplierSiteAudits = Mapper.Map<IEnumerable<SupplierSiteAudit>>(_supplierService.GetSupplierSiteAuditBySupplierID(supplierId));
            IEnumerable<SupplierDocumentUser> supplierDocuments = Mapper.Map<IEnumerable<SupplierDocumentUser>>(_supplierService.GetSupplierDocumentBySupplierIDAndDocumentTypeID(supplierId, documentTypeID));
            foreach (SupplierSiteAudit supplierSiteAudit in supplierSiteAudits)
            {
                SupplierDocumentUser supplierDocumentUser = supplierDocuments.SingleOrDefault(supplierDocumentRecord => supplierDocumentRecord.SupplierDocumentID == supplierSiteAudit.SupplierDocumentID);
                supplierSiteAudit.UploadPath = supplierDocumentUser == null ? string.Empty : supplierDocumentUser.UploadPath;
                supplierSiteAudit.DocumentName = supplierDocumentUser == null ? string.Empty : supplierDocumentUser.DocumentName;
                supplierSiteAudit.UploadDate = supplierDocumentUser.UploadDate;
                supplierSiteAudit.DocumentUrl = Url.Action(GlobalConst.Actions.Area.Internal.SupplierSiteAuditController.GetSiteAuditFile, GlobalConst.Controllers.Area.Internal.SupplierSiteAudit, new { fileName = supplierDocumentUser.UploadPath, supplierID = supplierDocumentUser.SupplierID, area = GlobalConst.Areas.Internal });
            }
            return Json(supplierSiteAudits);
        }


        [HttpPost]
        public JsonResult GetUsersByUserTypeID(int userTypeID)
        {
            IEnumerable<SupplierAuditor> users = Mapper.Map<IEnumerable<SupplierAuditor>>(_userService.GetUserByUserTypeID(userTypeID));
            return Json(users);
        }

        [NonAction]
        public string AddSupplierSiteAuditFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorage.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.SupplierAudit);
            var fileName = Path.GetFileName(path);
            var fileExtension = Path.GetExtension(fileName);

            file.SaveAs(path);
            return fileName;
        }

        [HttpPost]
        public ActionResult AddSupplierSiteAudit(SupplierSiteAudit supplierSiteAudit, SupplierDocument supplierDocument)
        {
            string returnString = string.Empty;
            if (supplierSiteAudit.FileUploadSiteAudit != null)
            {
                supplierDocument.UploadPath = AddSupplierSiteAuditFile(supplierSiteAudit.FileUploadSiteAudit, supplierSiteAudit.SupplierID);
                supplierDocument.UploadDate = DateTime.Now;
                int SupplierSiteAuditId = _supplierService.AddSupplierSiteAuditAndDocument(Mapper.Map<ITSService.SupplierService.SupplierSiteAudit>(supplierSiteAudit), Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                if (SupplierSiteAuditId >= 1) { returnString = GlobalResource.AddedSuccessfully; }
                else { returnString = GlobalResource.UnableToCompleteAction; }
            }
            else { returnString = GlobalResource.UnableToCompleteAction; }
            return Content(returnString);
        }

        [HttpPost]
        public JsonResult DeleteSupplierSiteAuditBySupplierSiteAuditID(int supplierSiteAuditID, int supplierDocumentID, string fileName, int supplierID)
        {
            string msg = string.Empty;
            int deletedRecordSiteAudit = _supplierService.DeleteSupplierSiteAuditBySupplierSiteAuditID(supplierSiteAuditID);
            if (deletedRecordSiteAudit == 1)
            {
                int deletedResult = _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierDocumentID);
                if (deletedResult == 1)
                {
                    string uploadPath = SupplierSiteAuditStoragePath(supplierID, fileName);
                    System.IO.File.Delete(uploadPath);
                    msg = GlobalResource.DeletedSuccessfully;
                }
            }
            else { msg = GlobalResource.UnableToCompleteAction; }
            return Json(msg);
        }

        [HttpPost]
        public ActionResult UpdateSupplierSiteAuditBySupplierSiteAuditID(SupplierSiteAudit supplierSiteAudit, SupplierDocument supplierDocument)
        {
            var msg = string.Empty;
            if (supplierSiteAudit.FileUploadSiteAudit != null)
            {
                supplierDocument.UploadDate = DateTime.Now;
                supplierDocument.UploadPath = AddSupplierSiteAuditFile(supplierSiteAudit.FileUploadSiteAudit, supplierSiteAudit.SupplierID);
                string uploadPath = SupplierSiteAuditStoragePath(supplierSiteAudit.SupplierID, supplierSiteAudit.OldUploadPath);
                if (System.IO.File.Exists(uploadPath) == true) { System.IO.File.Delete(uploadPath); }
            }
            int effectedRecordSiteAudit = _supplierService.UpdateSupplierSiteAuditBySupplierSiteAuditID(Mapper.Map<ITSService.SupplierService.SupplierSiteAudit>(supplierSiteAudit));
            if (effectedRecordSiteAudit == 1)
            {
                int effectedRecordSupplerDocument = _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                if (effectedRecordSupplerDocument == 1) { msg = GlobalResource.UpdatedSuccessfully; }
            }
            else { msg = GlobalResource.UnableToCompleteAction; }
            return Content(msg);
        }

        [NonAction]
        public string SupplierSiteAuditStoragePath(int supplierID, string fileName)
        {
            return _supplierStorage.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, fileName, GlobalConst.SupplierDocumentType.SupplierAudit);
        }

        public FileResult GetSiteAuditFile(string fileName, int supplierID)
        {
            string uploadPath = SupplierSiteAuditStoragePath(supplierID, fileName);
            return File(uploadPath, GlobalConst.ContentTypes.PDF, fileName);
        }
    }
}
