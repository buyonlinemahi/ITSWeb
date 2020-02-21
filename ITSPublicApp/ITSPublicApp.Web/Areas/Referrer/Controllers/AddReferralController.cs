using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.LookUpService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Web.ITSService.SupplierService;
using ITSPublicApp.Web.ITSService.UserService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ITSPublicApp.Web.ITSService.PatientService;
using Case = ITSPublicApp.Domain.Models.Case;
using CaseContact = ITSPublicApp.Domain.Models.CaseContact;
using EmployeeDetail = ITSPublicApp.Domain.Models.EmployeeDetail;
using Employment = ITSPublicApp.Domain.Models.Employment;
using InjuredPartyRepresentative = ITSPublicApp.Domain.Models.InjuredPartyRepresentative;
using Patient = ITSPublicApp.Domain.Models.Patient;
using Policie = ITSPublicApp.Domain.Models.Policie;
using PolicyOpenDetail = ITSPublicApp.Domain.Models.PolicyOpenDetail;
using ReferrerAssignedUser = ITSPublicApp.Domain.Models.ReferrerAssignedUser;
using ReferrerLocation = ITSPublicApp.Domain.Models.ReferrerLocation;
using ReferrerProject = ITSPublicApp.Domain.Models.ReferrerProject;
using ReferrerProjectTreatmentName = ITSPublicApp.Domain.Models.ReferrerProjectTreatmentName;
using ReferrerProjectTreatmentTreatmentType = ITSPublicApp.Domain.Models.ReferrerProjectTreatmentTreatmentType;
using Solicitor = ITSPublicApp.Domain.Models.Solicitor;
using Model = ITSPublicApp.Domain.Models;
namespace ITSPublicApp.Web.Areas.Referrer.Controllers
{
    [AuthorizedUserCheck("Referrer")]
    public class AddReferralController : BaseController
    {
        private readonly ICaseService _caseService;
        private readonly IUserService _userService;
        private readonly IReferrerService _referrerService;
        private readonly ISupplierService _supplierService;
        private readonly IReferrerStorage _referrerStorage;
        private readonly ILookUpService _lookupService;
        private readonly EmailService _emailService;
        private readonly IEncryption _encryptionService;
        private readonly ITSService.PatientService.IPatientService _patientService;

        public AddReferralController(ICaseService caseService, IReferrerService referrerService,IPatientService patientService, ISupplierService supplierService, IUserService userService, IReferrerStorage referrerStorage
            , ILookUpService lookupService, EmailService emailService, IEncryption encryptionService)
        {
            _caseService = caseService;
            _referrerService = referrerService;
            _supplierService = supplierService;
            _userService = userService;
            _referrerStorage = referrerStorage;
            _emailService = emailService;
            _lookupService = lookupService;
            _encryptionService = encryptionService;
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int referrerID = ITSUser.ReferrerID.Value;

            AddReferralViewModel model = new AddReferralViewModel()
            {
                ITSUser = ITSUser,
                ReferrerProjects = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProject>, IEnumerable<ReferrerProject>>(_referrerService.GetReferrerCompleteProjectsByReferrerID(referrerID)),
                ReferrerAssignedUsers = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerAssignedUser>, IEnumerable<ReferrerAssignedUser>>(_referrerService.GetReferrerAssignedUserByReferrerID(referrerID)),
                ReferrerLocations = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerLocation>, IEnumerable<ReferrerLocation>>(_referrerService.GetReferrerLocationsByReferrerID(referrerID)),
                ReferrerDocumentTypes = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerDocumentType>, IEnumerable<ITSPublicApp.Domain.Models.ReferrerDocumentType>>(_referrerService.GetReferrerDocumentType()),
                EmploymentTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.EmploymentType>>(_lookupService.GetAllEmploymentType()),
                PolicyTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.PolicyType>>(_lookupService.GetAllPolicyType()),
                TypeCovers = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.TypeCover>>(_lookupService.GetAllTypeCover()),
                PolicyCriterias = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.PolicyCriteria>>(_lookupService.GetAllPolicyCriteria()),
                FitForWorks = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.FitForWork>>(_lookupService.GetAllFitForWork()),
                Admitteds = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.Admitted>>(_lookupService.GetAllAdmitted()),
                Referrer = Mapper.Map<ITSPublicApp.Domain.Models.Referrer>(_referrerService.GetReferrerDetailsByReferrerID(referrerID)),
                WorkTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.WorkType>>(_lookupService.GetAllWorkType()),
                RoleTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.RoleType>>(_lookupService.GetAllRoleType()),
                Reinsurers = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.Reinsurer>>(_lookupService.GetAllReinsurer()),
                GenderTypes = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.Gender>>(_lookupService.GetAllGenderTypes()),
                AdditionalTests = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.AdditionalTest>>(_lookupService.GetAllAdditionalTest()),
                ReasonForReferral = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.ReasonForReferral>>(_lookupService.GetAllReasonForReferral()),
                NetworkRailStandardApplicable = Mapper.Map<IEnumerable<ITSPublicApp.Domain.Models.NetworkRailStandardApplicable>>(_lookupService.GetAllNetworkRailStandardApplicable())                
            };
            ITSUser.EncryptUserID = EncryptString(ITSUser.UserID.ToString());
            ITSUser.EyptReferrerLocationID = EncryptString(_userService.GetUserById(ITSUser.UserID).ReferrerLocationID.Value.ToString());
            foreach (ReferrerLocation rl in model.ReferrerLocations)
                rl.EncryptReferrerLocationID = EncryptString(rl.ReferrerLocationID.ToString());
            foreach (ReferrerAssignedUser ra in model.ReferrerAssignedUsers)
                ra.EncryptUSERID = EncryptString(ra.USERID.ToString());
            return View(model);
        }

        [HttpPost]
        public bool IsImage()
        {
            try
            {
                Stream stream = Request.Files[0].InputStream;
                var i = Image.FromStream(stream);
                stream.Seek(0, SeekOrigin.Begin);
                if (ImageFormat.Jpeg.Equals(i.RawFormat) || ImageFormat.Png.Equals(i.RawFormat) || ImageFormat.Bmp.Equals(i.RawFormat))
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
            var pdfBytes = System.Text.Encoding.ASCII.GetBytes(pdfString);
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
        public bool IsDoc()
        {               
            string tempUpload = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]) + "/tempUpload/";

            if (!Directory.Exists(tempUpload))
                Directory.CreateDirectory(tempUpload);
            string tempPath = tempUpload + ITSUser.UserIDEncry + Request.Files[0].FileName;
            Request.Files[0].SaveAs(tempPath);

            try
            {
                using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(tempPath, true))
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

        [HttpPost]
        public ActionResult SaveReferrerDocument(string docName, string docDate, string refDocType, int refDocTypeID)
        {
            try
            {
                string tempCaseID = "tempCaseID" + Guid.NewGuid();
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + Request.Files[0].FileName;
                string path = _referrerStorage.SetTempReferralStorage(storagePath, tempCaseID) + fileName;
                Request.Files[0].SaveAs(path);

                ITSPublicApp.Domain.Models.ReferrerDocuments _referrerDocument = new ITSPublicApp.Domain.Models.ReferrerDocuments();
                _referrerDocument.UploadPath = fileName;
                _referrerDocument.DocumentName = docName;
                _referrerDocument.DocumentDate = DateTime.Parse(docDate);
                _referrerDocument.ReferrerID = ITSUser.ReferrerID.Value;
                _referrerDocument.UserID = ITSUser.UserID;
                _referrerDocument.ReferrerDocumentType = refDocType;
                _referrerDocument.ReferrerDocumentTypeID = refDocTypeID;
                _referrerDocument.CaseID = 0;
                _referrerDocument.TempCaseID = tempCaseID;
                return Json(_referrerDocument);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddReferrerDocument(string docName, string docDate, string refDocType, int refDocTypeID, string CaseID)
        {
            try
            {
                int caseID = Convert.ToInt16(DecryptString(CaseID.ToString()));
                string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "-" + Request.Files[0].FileName;
                var _case = _caseService.GetCaseByCaseID(caseID);
                ITSPublicApp.Domain.ViewModels.CaseDetailViewModel viewModel = new ITSPublicApp.Domain.ViewModels.CaseDetailViewModel();
                viewModel.CasePatientTreatment = Mapper.Map<Model.CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID));
                string path = _referrerStorage.SetReferrerReferralsStoragePath(_case.ReferrerID, _case.ReferrerProjectTreatmentID, _case.CaseID, fileName, storagePath);
                Request.Files[0].SaveAs(path);
                ITSPublicApp.Domain.Models.ReferrerDocuments _referrerDocument = new ITSPublicApp.Domain.Models.ReferrerDocuments();
                _referrerDocument.ReferrerID = ITSUser.ReferrerID.Value;
                _referrerDocument.DocumentName = docName;
                _referrerDocument.DocumentDate = DateTime.Parse(docDate);
                _referrerDocument.DocumentTypeID = 5;
                _referrerDocument.UploadDate = DateTime.Now;
                _referrerDocument.UserID = ITSUser.UserID;
                _referrerDocument.UploadPath = fileName;
                _referrerDocument.ReferrerDocumentTypeID = refDocTypeID;
                _referrerDocument.ReferrerDocumentTypeDesc = refDocType;
                _referrerDocument.CaseID = caseID;
                _referrerDocument.ReferrerCheck = true;
                _referrerDocument.SupplierCheck = false;
                _referrerDocument.EncryptedCaseID = EncryptString(caseID.ToString()); 
                var _referrerDocumentDetails = _referrerService.AddReferrerDocument(Mapper.Map<ITSService.ReferrerService.ReferrerDocument>(_referrerDocument));
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/RefererrDocumentUploadTemplate.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("##FirstName##", viewModel.CasePatientTreatment.FirstName);
                MailText = MailText.Replace("##LastName##", viewModel.CasePatientTreatment.LastName);
                MailText = MailText.Replace("##CaseReferenceNumber##", viewModel.CasePatientTreatment.CaseReferrerReferenceNumber);
                bool result = _emailService.SendGeneralEmail(System.Configuration.ConfigurationManager.AppSettings["ReferrerUploadDocumtEmail"], null, "New Document Ready to View", MailText);
                return Json(_referrerDocument);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AddReferralViewModel viewModel)
        {
         
            viewModel.Case.ReferrerAssignedUser = Convert.ToInt32(DecryptString(viewModel.Case.EncryptReferrerAssignedUser.ToString()));
            if (viewModel.Case.EncryptedOfficeLocationID != null)
                viewModel.Case.OfficeLocationID = Convert.ToInt16(DecryptString(viewModel.Case.EncryptedOfficeLocationID));
            else
                viewModel.Case.EncryptedOfficeLocationID = "";

            viewModel.Case.ReferrerID = ITSUser.ReferrerID.Value;
            viewModel.Case.SendInvoiceTo = "Assigned User"; // as pe task 3502 point 1a.
            viewModel.Case.SendInvoiceName = "NULL"; // as pe task 3502 point 1a.
            viewModel.Case.SendInvoiceEmail = "NULL"; // as pe task 3502 point 1a.
            if (viewModel.Case.NewPolicyReferenceNumber == null)
            {
                viewModel.Case.NewPolicyReferenceNumber = " ";
            }
            if (viewModel.Policie.ReferenceNo == null)
                viewModel.Policie.ReferenceNo = " ";
            if (viewModel.Policie.PolicyWording == null)
                viewModel.Policie.PolicyWording = " ";
            viewModel.injuredPartyRepresentative.ReferralID = ITSUser.ReferrerID.Value;
            string _patientName = viewModel.Patient.FirstName + ' ' + viewModel.Patient.LastName;
            var _referrer = _referrerService.GetReferrerDetailsByReferrerID(ITSUser.ReferrerID.Value);
            string _referrerName = _referrer.ReferrerContactFirstName + " " + _referrer.ReferrerContactLastName;
            if (viewModel.Case.SendInvoiceTo == "ReferrerProjectUser")
            {
                viewModel.Case.SendInvoiceName = "";
                viewModel.Case.SendInvoiceEmail = "";
                viewModel.Case.SupplierAssignedUser = 0;
            }
            if (viewModel.Case.SupplierAssignedUser == null)
                viewModel.Case.SupplierAssignedUser = 0;
            int userID = ITSUser.UserID;
            viewModel.Case.IsTriage = viewModel.ReferrerProject.IsTriage;           
            if (viewModel.Referrer.IsPolicyDetailOpenOrDropdowns == "Dropdowns")
            {
               viewModel.policyOpenDetail = null;
            }            
            //adding to case table
          int newCaseID = _caseService.AddReferral(Mapper.Map<Case, ITSService.CaseService.Case>(viewModel.Case),
           Mapper.Map<Patient, ITSService.CaseService.Patient>(viewModel.Patient),
           userID,
           Mapper.Map<Solicitor, ITSService.CaseService.Solicitor>(viewModel.Solicitor),
           Mapper.Map<InjuredPartyRepresentative, ITSService.CaseService.InjuredPartyRepresentative>(viewModel.injuredPartyRepresentative),
           Mapper.Map<Employment, ITSService.CaseService.Employment>(viewModel.Employment),
           Mapper.Map<Policie, ITSService.CaseService.Policie>(viewModel.Policie),
           Mapper.Map<EmployeeDetail, ITSService.CaseService.EmployeeDetail>(viewModel.employeeDetail),
           Mapper.Map<PolicyOpenDetail, ITSService.CaseService.PolicyOpenDetail>(viewModel.policyOpenDetail),
           Mapper.Map<ITSPublicApp.Domain.Models.DrugTest, ITSService.CaseService.DrugTest>(viewModel.DrugTest),
           Mapper.Map<ITSPublicApp.Domain.Models.JobDemand, ITSService.CaseService.JobDemand>(viewModel.JobDemand)
          );
            //adding multiple assigned user 
            foreach (ITSPublicApp.Domain.Models.CaseReferrerAssignedUser _caseReferrerAssignedUser in viewModel.CaseReferrerAssignedUser)
            {
                _caseReferrerAssignedUser.UserID = Int32.Parse(DecryptString(_caseReferrerAssignedUser.EncUserID));
                _caseReferrerAssignedUser.CaseID = newCaseID;
                _caseService.AddCaseReferrerAssignedUser(Mapper.Map<ITSService.CaseService.CaseReferrerAssignedUser>(_caseReferrerAssignedUser));
            }

            string caseNumber = _caseService.GetCaseByCaseID(newCaseID).CaseNumber;
            string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
            if (viewModel.PatientConsentFileUpload != null)
            {
                var patientfilename = viewModel.PatientConsentFileUpload.FileName;
                var patientpath = _referrerStorage.SetReferrerReferralsStoragePath(viewModel.Case.ReferrerID, viewModel.Case.ReferrerProjectTreatmentID, newCaseID, patientfilename, storagePath);
                viewModel.PatientConsentFileUpload.SaveAs(patientpath);
                _referrerService.AddReferrerDocument(new ITSService.ReferrerService.ReferrerDocument { UploadDate = DateTime.Now, UserID = ITSUser.UserID, UploadPath = patientfilename, ReferrerID = ITSUser.ReferrerID.Value, ReferrerDocumentTypeID = 3, CaseID = newCaseID, DocumentDate = DateTime.Now, DocumentName = patientfilename,SupplierCheck=false,ReferrerCheck=true });
            }

            if (viewModel.ReferrerDocuments != null)
            {
                foreach (ReferrerDocuments refDoc in viewModel.ReferrerDocuments)
                {
                    var filename = caseNumber + Path.GetExtension(refDoc.UploadPath);
                    var path = _referrerStorage.SetReferrerReferralsStoragePath(viewModel.Case.ReferrerID, viewModel.Case.ReferrerProjectTreatmentID, newCaseID, refDoc.UploadPath, storagePath);
                    System.IO.File.Move(_referrerStorage.GetCompleteTempReferralStorage(storagePath, refDoc.TempCaseID) + refDoc.UploadPath, path);
                    RemoveReferrerDocument(refDoc.TempCaseID);
                    refDoc.UploadDate = DateTime.Now;
                    refDoc.UserID = ITSUser.UserID;
                    refDoc.CaseID = newCaseID;
                    refDoc.ReferrerID = ITSUser.ReferrerID.Value;
                    refDoc.ReferrerCheck = true;
                    refDoc.SupplierCheck = false;
                    _referrerService.AddReferrerDocument(Mapper.Map<Domain.Models.ReferrerDocuments, ITSService.ReferrerService.ReferrerDocument>(refDoc));
                }
            }
            //TODO : need to review this for 1 pass only
            foreach (var contact in viewModel.CaseContacts)
            {
                contact.CaseID = newCaseID;
                _caseService.AddCaseContact(Mapper.Map<CaseContact, ITSService.CaseService.CaseContact>(contact));
            }
            string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/AddRefererrEmailTemplate.html");
            System.IO.StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("##ReferenceNumber##", viewModel.Case.CaseReferrerReferenceNumber);
            MailText = MailText.Replace("##PatientName##", _patientName);
            MailText = MailText.Replace("##CaseReferenceNumber##", caseNumber);
            MailText = MailText.Replace("##ReferrerName##", _referrerName);
            MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
            bool result = _emailService.SendGeneralEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Referrals sent by Customer", MailText);
            string successMessage = string.Format(GlobalResource.CaseSubmittedSuccessfullyMessage, caseNumber);
            return PartialView("_ConfirmationBoxPartial", new ConfirmationBoxViewModel { MessageBody = successMessage });
        }

        [HttpPost]
        public int RemoveReferrerDocument(string tempCaseID)
        {
            string path = _referrerStorage.GetTempReferralStorage(Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]), tempCaseID);
            var dir = new DirectoryInfo(path);
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return 1;
        }


        [HttpPost]
        public JsonResult GetReferrerProjects(int referrerID)
        {
            IEnumerable<ReferrerProject> referrerProjects = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProject>, IEnumerable<ReferrerProject>>(_referrerService.GetReferrerProjectsByReferrerID(referrerID));
            return Json(referrerProjects);
        }
        [HttpPost]
        public JsonResult GetTreatmentCategories(int referrerProjectID)
        {
            if (referrerProjectID != 0)
            {
                IEnumerable<ReferrerProjectTreatmentName> referrerProjectTreatmentCategories = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProjectTreatmentName>,
                    IEnumerable<ReferrerProjectTreatmentName>>(_referrerService.GetReferrerEnabledProjectTreatmentNamesByReferrerProjectID(referrerProjectID));
                return Json(referrerProjectTreatmentCategories);
            }
            return null;
        }
        [HttpPost]
        public JsonResult GetTreatmentTypes(int referrerProjectTreatmentID)
        {
            if (referrerProjectTreatmentID != 0)
            {
                IEnumerable<ReferrerProjectTreatmentTreatmentType> treatmentCategoriesTreatmentTypes = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProjectTreatmentTreatmentType>,
                            IEnumerable<ReferrerProjectTreatmentTreatmentType>>(_referrerService.GetReferrerProjectTreatmentTreatmentTypeByReferrerProjectTreatmentTypeID(referrerProjectTreatmentID));
                return Json(treatmentCategoriesTreatmentTypes);
            }
            return null;
        }
        [HttpPost]
        public JsonResult GetUserContacts()
        {
           // IEnumerable<ITSService.UserService.User> dtoUsers = _userService.GetReferrerUsersByReferrerLocationID(Convert.ToInt32(DecryptString(referrerLocationID)));
            IEnumerable<ITSService.UserService.User> dtoUsers = _userService.GetReferrerUsersByID(ITSUser.ReferrerID.Value);
            IEnumerable<ITSUser> users = Mapper.Map<IEnumerable<ITSService.UserService.User>, IEnumerable<ITSUser>>(dtoUsers);
            return Json(users);
        }
        [HttpGet]
        public JsonResult GetAllInjuredRepresentativeOption()
        {
            return Json(_referrerService.GetAllInjuredRepresentativeOption(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllPrimaryCondition()
        {
            return Json(_referrerService.GetAllPrimaryCondition(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllGips()
        {
            return Json(_caseService.GetAllGips(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllIips()
        {
            return Json(_caseService.GetAllIips(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllTpds()
        {
            return Json(_caseService.GetAllTpds(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllElRehabs()
        {
            return Json(_caseService.GetAllElRehabs(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
       
    }
}