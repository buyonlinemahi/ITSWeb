using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Domain.Models;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Configuration;
using ITS.Infrastructure.Base;

namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierInsuredController : BaseController
    {
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ISupplierStorage _supplierStorage;

        public SupplierInsuredController(ITSService.SupplierService.ISupplierService supplierService, ISupplierStorage supplierStorage)
        {
            _supplierService = supplierService;
            _supplierStorage = supplierStorage;
        }

        [HttpPost]
        public String AddSupplierInsurance(SupplierInsurance supplierInsurance, SupplierDocument supplierDocument)
        {
            supplierDocument.UserID = ITSUser.UserID;
            string globalMessage = string.Empty;
            supplierDocument.UploadDate = DateTime.Now.Date;
            if (supplierInsurance.InsuredDocumentFile != null)
            {
                supplierDocument.UploadPath = AddSupplierInsuranceFile(supplierInsurance.InsuredDocumentFile, supplierInsurance.SupplierID);
                int supplierInsID = _supplierService.AddSupplierInsuranceAndDocument(Mapper.Map<ITSService.SupplierService.SupplierInsurance>(supplierInsurance), Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
                globalMessage = GlobalResource.AddedSuccessfully;
            }
            else
            {
                globalMessage = GlobalResource.UnableToCompleteAction;
            }

            return globalMessage;

        }

        [HttpPost]
        public String UpdateSupplierInsurance(SupplierInsurance supplierInsurance, SupplierDocument supplierDocument)
        {
            supplierDocument.UserID = ITSUser.UserID;
            string globalMessage = string.Empty;
            supplierDocument.UploadDate = DateTime.Now.Date;
            if (supplierInsurance.InsuredDocumentFile != null)
            {
                System.IO.File.Delete(FullPath(supplierDocument.SupplierID, supplierDocument.UploadPath));
                supplierDocument.UploadPath = AddSupplierInsuranceFile(supplierInsurance.InsuredDocumentFile, supplierInsurance.SupplierID);
                globalMessage = UpdateMethod(supplierInsurance, supplierDocument);

            }
            globalMessage = UpdateMethod(supplierInsurance, supplierDocument);

            return globalMessage;



        }

        protected string UpdateMethod(SupplierInsurance supplierInsurance, SupplierDocument supplierDocument)
        {
            int insuranceUpdateResult = _supplierService.UpdateSupplierInsurance(Mapper.Map<ITSService.SupplierService.SupplierInsurance>(supplierInsurance));
            int documentUpdateResult = _supplierService.UpdateSupplierDocument(Mapper.Map<ITSService.SupplierService.SupplierDocument>(supplierDocument));
            return GlobalResource.UpdatedSuccessfully;
        }


        [NonAction]
        public string AddSupplierInsuranceFile(HttpPostedFileBase file, int supplierID)
        {

            var path = _supplierStorage.SetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierID, file.FileName, GlobalConst.SupplierDocumentType.Insurance);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }


        [HttpPost]
        public JsonResult RemoveFile(SupplierInsurance supplierInsurance, SupplierDocument supplierDocument)
        {

            System.IO.File.Delete(FullPath(supplierInsurance.SupplierID, Path.GetFileName(supplierInsurance.UploadPath)));
            _supplierService.DeleteSupplierInsuranceBySupplierInsuredID(supplierInsurance.SupplierInsuredID);
            _supplierService.DeleteSupplierDocumentBySupplierDocumentID(supplierInsurance.SupplierDocumentID);
            return Json(GlobalResource.DeletedSuccessfully);
        }

        [HttpPost]
        public JsonResult GetSupplierInsured(int supplierId, int documentTypeID)
        {
            IEnumerable<SupplierInsurance> supplierInsurances = Mapper.Map<IEnumerable<SupplierInsurance>>(_supplierService.GetSupplierInsuranceBySupplierID(supplierId));
            IEnumerable<SupplierDocumentUser> supplierDocuments = Mapper.Map<IEnumerable<SupplierDocumentUser>>(_supplierService.GetSupplierDocumentBySupplierIDAndDocumentTypeID(supplierId, documentTypeID));

            foreach (SupplierInsurance supplierInsurance in supplierInsurances)
            {
                SupplierDocumentUser supplierDocument = supplierDocuments.SingleOrDefault(x => x.SupplierDocumentID == supplierInsurance.SupplierDocumentID);
                supplierInsurance.UploadPath = supplierDocument.UploadPath;// this need to remove from model aand controler and model
                supplierInsurance.DocumentName = supplierDocument.DocumentName;
                //  supplierInsurance.UploadDate = supplierDocument.UploadDate;
                supplierInsurance.InsuredDocumentUrl = Url.Action(GlobalConst.Actions.Area.Internal.SupplierInsuranceController.GetSupplierInsuranceFile, GlobalConst.Controllers.Area.Internal.SupplierInsured, new { fileName = supplierDocument.UploadPath, supplierID = supplierDocument.SupplierID, area = GlobalConst.Areas.Internal });
            }
            return Json(supplierInsurances);
        }


        public FileResult GetSupplierInsuranceFile(string fileName, int supplierID)
        {
            string uploadPath = FullPath(supplierID, fileName);
            return File(uploadPath, "application/pdf", fileName);
        }

        [NonAction]
        public string FullPath(int supplierId, string fileName)
        {
            return _supplierStorage.GetSupplierStoragePath(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), supplierId, fileName, GlobalConst.SupplierDocumentType.Insurance);
        }
    }
}
