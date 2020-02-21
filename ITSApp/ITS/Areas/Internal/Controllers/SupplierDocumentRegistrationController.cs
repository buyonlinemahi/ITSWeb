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
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Configuration;
using ITS.Infrastructure.Base;
using System.Text;
/*
 * Latest Version 1.1
 * 
 * Created By   : Robin Singh
 * Date         : 26-Dec-2012
 * Purpose      : Created action for GetSupplierDocumentBySupplierIDAndDocumentTypeID and AddSupplierDocuments.
 * Version      : 1.0
 *
 * Updated By   : Robin Singh
 * Date         : 02-Jan-2013
 * Purpose      : Implement Logic to Add SupplierDocumentsFiles And AddSupplierDocuments
 * Version      : 1.1
 * 
 * */
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierDocumentRegistrationController : BaseController
    {
        private readonly ITSService.UserService.IUserService _userService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ISupplierStorage _supplierStorage;

        public SupplierDocumentRegistrationController(ITSService.UserService.IUserService userService, ITSService.SupplierService.ISupplierService supplierService, ISupplierStorage supplierStorage)
        {
            _userService = userService;
            _supplierService = supplierService;
            _supplierStorage = supplierStorage;
        }

        [HttpPost]
        public JsonResult GetSupplierDocumentBySupplierIDAndDocumentTypeID(int SupplierID, int DocumentTypeID)
        {
            IEnumerable<SupplierDocumentUser> supplierdocuments = Mapper.Map<IEnumerable<SupplierDocumentUser>>(_supplierService.GetSupplierDocumentBySupplierIDAndDocumentTypeID(SupplierID, DocumentTypeID));
            foreach (SupplierDocumentUser supplierDocument in supplierdocuments)
            {
                supplierDocument.DocumentUrl = Url.Action(GlobalConst.Actions.Area.Internal.SupplierDocumentRegistrationController.GetRegistrationFile, GlobalConst.Controllers.Area.Internal.SupplierDocumentRegistration, new { fileName = supplierDocument.UploadPath, supplierID = supplierDocument.SupplierID, area = GlobalConst.Areas.Internal });
            }
            return Json(supplierdocuments);
        }



        [HttpPost]
        public ActionResult AddSupplierDocumentsFiles(SupplierDocument supplierDocument)
        {

            string globalMessage = string.Empty;
            if (supplierDocument.RegistrationDocumentFileUpload != null)
            {
                if (supplierDocument.RegistrationDocumentFileUpload.ContentLength > 0)
                {
                    var path = _supplierStorage.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierDocument.SupplierID, supplierDocument.RegistrationDocumentFileUpload.FileName, GlobalConst.SupplierDocumentType.Registration);

                    var fileName = Path.GetFileName(path);
                    var fileExtension = Path.GetExtension(fileName);
                    if (GlobalConst.FileExtensions.PDF.ToLower() == fileExtension.ToLower() || GlobalConst.FileExtensions.DOC.ToLower() == fileExtension.ToLower() ||
                        GlobalConst.FileExtensions.DOCX.ToLower() == fileExtension.ToLower() || GlobalConst.FileExtensions.JPEG.ToLower() == fileExtension.ToLower() ||
                        GlobalConst.FileExtensions.JPG.ToLower() == fileExtension.ToLower() || GlobalConst.FileExtensions.TIFF.ToLower() == fileExtension.ToLower()
                        || GlobalConst.FileExtensions.TIF.ToLower() == fileExtension.ToLower() 
                        )
                    {
                        supplierDocument.RegistrationDocumentFileUpload.SaveAs(path);
                        supplierDocument.UploadDate = DateTime.Now;
                        supplierDocument.UploadPath = fileName;
                        supplierDocument.UserID = ITSUser.UserID;
                        int supplierdocumetID = _supplierService.AddSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                        globalMessage = GlobalResource.AddedSuccessfully;
                    }
                    else
                    {
                        globalMessage = GlobalResource.AllowedFileExtentionsMessage;
                    }
                }
            }
            return Content(globalMessage);
        }
        [NonAction]
        public string GetSupplierDocumentRegistrationStoragePath(int supplierID, string fileName)
        {
            return _supplierStorage.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, fileName, GlobalConst.SupplierDocumentType.Registration);
        }


        public FileResult GetRegistrationFile(string fileName, int supplierID)
        {
            string uploadPath = GetSupplierDocumentRegistrationStoragePath(supplierID, fileName);
            return File(uploadPath, GlobalConst.ContentTypes.PDF, fileName);
        }


        
        [HttpPost]
        public JsonResult DeleteRegistrationDocument(ITS.Domain.Models.SupplierModel.Registration supplierDocument)
        {
            if (supplierDocument.SupplierDocumentID > 0)
            {
                System.IO.File.Delete(FullPath(supplierDocument.SupplierID, supplierDocument.UploadPath));
                _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierDocument.SupplierDocumentID);
            }

            return Json(GlobalResource.DeletedSuccessfully);
        }

        [NonAction]
        public string FullPath(int supplierId, string fileName)
        {
            return _supplierStorage.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierId, fileName, GlobalConst.SupplierDocumentType.Insurance);
        }

        [NonAction]
        public string AddSupplierRegistrationDocumentFile(HttpPostedFileBase file, int supplierID)
        {
            var path = _supplierStorage.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.Registration);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
    }
}
