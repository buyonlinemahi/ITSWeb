using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
/*
 * Latest Version 1.2
 * 
 * Updated By   : Anuj Batra
 * Date         : 19-Dec-2012
 * Purpose      : Created action for GetSupplierComplaintBySupplierID.
 * Version      : 1.0
 
 * Updated By   : Anuj Batra
 * Date         : 20-Dec-2012
 * Purpose      : Created action for AddSupplierComplaint and UpdateSupplierComplaintByComplaindID.
 * Version      : 1.1
 
 * Updated By   : Anuj Batra
 * Date         : 21-Dec-2012
 * Purpose      : Created action for DeleteSupplierComplaintBySupplierComplaintID.
 * Version      : 1.2
 * */
namespace ITSWebApp.Areas.Internal.Controllers
{
    public class SupplierSetupComplaintController : BaseController
    {

        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        //
        // GET: /Internal/SupplierSetupComplaint/
        public SupplierSetupComplaintController(ITSService.SupplierService.ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddSupplierComplaint(ITS.Domain.Models.SupplierComplaint suppliercomplaint)
        {
            int returnValue = _supplierService.AddSupplierComplaint(Mapper.Map<ITSService.SupplierService.SupplierComplaint>(suppliercomplaint));
            if (returnValue >= 1) { return Json(GlobalResource.AddedSuccessfully); } return Json(GlobalResource.UnableToCompleteAction);
        }

        [HttpPost]
        public JsonResult GetSupplierComplaintsBySupplierID(int supplierId)
        {
            IEnumerable<ComplaintStatus> ComplaintsStatus = Mapper.Map<IEnumerable<ComplaintStatus>>(_supplierService.GetAllComplaintstatus());
            IEnumerable<ComplaintType> ComplaintTypes = Mapper.Map<IEnumerable<ComplaintType>>(_supplierService.GetAllComplaintType());
            IEnumerable<SupplierComplaint> SupplierComplaints = Mapper.Map<IEnumerable<SupplierComplaint>>(_supplierService.GetSupplierComplaintBySupplierID(supplierId));
            foreach (SupplierComplaint supplierComplaint in SupplierComplaints)
            {
                ComplaintStatus complaintStatus = ComplaintsStatus.SingleOrDefault(Complaint => Complaint.ComplaintStatusID == supplierComplaint.ComplaintStatusID);
                ComplaintType complaintType = ComplaintTypes.SingleOrDefault(Complaint => Complaint.ComplaintTypeID == supplierComplaint.ComplaintTypeID);
                

                supplierComplaint.ComplaintStatusName = complaintStatus == null ? "" : complaintStatus.ComplaintStatusName;
                supplierComplaint.ComplaintTypeName = complaintType == null ? "" : complaintType.ComplaintTypeName;
            }

            return Json(SupplierComplaints);
        }
        

        [HttpPost]
        public JsonResult UpdateSupplierComplaintByComplaindID(ITS.Domain.Models.SupplierComplaint suppliercomplaint)
        {
            int returnValue = _supplierService.UpdateSupplierComplaintBySupplierComplaintID(Mapper.Map<ITSService.SupplierService.SupplierComplaint>(suppliercomplaint));
            if (returnValue == 1) { return Json(GlobalResource.UpdatedSuccessfully); } return Json(GlobalResource.UnableToCompleteAction);
        }

        [HttpPost]
        public JsonResult DeleteSupplierComplaintBySupplierComplaintID(int supplierComplaintID)
        {
            int returnValue = _supplierService.DeleteSupplierComplaintBySupplierComplaintID(supplierComplaintID);
            if (returnValue == 1) { return Json(GlobalResource.DeletedSuccessfully); } return Json(GlobalResource.UnableToCompleteAction);
        }

    }
}
