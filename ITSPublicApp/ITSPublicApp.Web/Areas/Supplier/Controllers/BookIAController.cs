using AutoMapper;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationFilters;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.CaseService;
using ITSPublicApp.Web.ITSService.PatientService;
using ITSPublicApp.Web.ITSService.ReferrerService;
using ITSPublicApp.Web.ITSService.UserService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Case = ITSPublicApp.Domain.Models.Case;
using CasePatient = ITSPublicApp.Domain.Models.CasePatient;
using CasePatientContactAttempt = ITSPublicApp.Domain.Models.CasePatientContactAttempt;

namespace ITSPublicApp.Web.Areas.Supplier.Controllers
{
    [AuthorizedUserCheck("Supplier")]
    public class BookIAController : BaseController
    {
        private readonly ICaseService _caseService;
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;
        private readonly IReferrerStorage _referrerStorage;
        private readonly IReferrerService _referrerService;
         
        private readonly IEncryption _encryptionService;

        private readonly EmailService _emailService;

        public BookIAController(EmailService emailService, ICaseService caseService, IPatientService patientService, IReferrerStorage referrerStorage, IReferrerService referrerService, IUserService userService, IEncryption encryptionService)
        {
            _emailService = emailService;
            _caseService = caseService;
            _patientService = patientService;
            _referrerStorage = referrerStorage;
            _referrerService = referrerService;
            _userService = userService;
            _encryptionService = encryptionService;
        }

        //[HttpGet]
        public ActionResult Index(string id)
        {
            return View((object)id);
        }

        [HttpPost]
        public ActionResult GetBookIA(string caseID)
        {
            int _caseID = Convert.ToInt32(DecryptString(caseID));
            CasePatient casePatient = Mapper.Map<CasePatient>(_caseService.GetBookIACasePatientByCaseID(_caseID));
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(_caseID));
            var _caseNotes = _caseService.GetCaseNoteByCaseIDAndWorkflowID(_caseID, (int)GlobalConst.WorkFlows.ReferralAccepted);
            BookIAViewModel viewModel = new BookIAViewModel
            {
                CasePatient = casePatient,
                InnovateNote = (_caseNotes != null) ? _caseNotes.Note : "",
                SuppliarAssignedUserID = caseObj.SupplierAssignedUser == null ? 0 : caseObj.SupplierAssignedUser.Value,
                ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { area = "", caseID }),
                IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, caseObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]),
                IsFirstAppointmentOffered = _referrerService.GetReferrerProjectFirstAppointmentOfferedByReferrerProjectTreatmentID(caseObj.ReferrerProjectTreatmentID),
                SuppliarAssignedUsers = Mapper.Map<IEnumerable<ITSUser>>(_userService.GetAllSupplierUserBySupplierID(ITSUser.SupplierID.Value)),
                CasePatientContactAttempts = Mapper.Map<IEnumerable<CasePatientContactAttempt>>(_caseService.GetPatientContactAttemptsByCaseID(_caseID))
            };
            return Json(viewModel);
        }


        [HttpPost]
        public ActionResult SaveUnsuccessfullDates(ITSPublicApp.Web.ITSService.CaseService.CasePatientContactAttempt CasePatientContactAttempts)
        {
            BookIAViewModel viewModel = new BookIAViewModel();
            var PatientContacts = _caseService.AddPatientContactAttempt(CasePatientContactAttempts);
            viewModel.CasePatientContactAttempts = Mapper.Map<IEnumerable<CasePatientContactAttempt>>(_caseService.GetPatientContactAttemptsByCaseID(CasePatientContactAttempts.CaseID));
            return Json(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteUnsuccessfullDates(int _CaseContactAttemptID, int _CaseID)
        {
            BookIAViewModel viewModel = new BookIAViewModel();
            var DeletePatientContacts = _caseService.DeletePatientContactAttempt(_CaseContactAttemptID);
            viewModel.CasePatientContactAttempts = Mapper.Map<IEnumerable<CasePatientContactAttempt>>(_caseService.GetPatientContactAttemptsByCaseID(_CaseID));
            return Json(viewModel);
        }


        [HttpPost]
        public ActionResult Confirm(BookIAViewModel viewModel)
        {
            try
            {
                string _AppointmentDateAndTimeResult = viewModel.CaseAppointmentDate.AppointmentDateTime.ToString();
                var _AppointmentDateAndTime = _AppointmentDateAndTimeResult.Split(' ');
                int response = _caseService.AddBookIA(viewModel.CaseID,
                                       viewModel.PatientContactDate,
                                       viewModel.SuppliarAssignedUserID.Value.ToString() == "" ? 0 : viewModel.SuppliarAssignedUserID.Value,
                                       Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(viewModel.CaseAppointmentDate),
                                         viewModel.InnovateNote);
                string[] _userEmails = _userService.GetAllUserEmailsByCaseContactByCaseID(viewModel.CaseID);
                string ReplyToEmails = "";
                bool result;

                if (_userEmails.Length > 1)
                {
                    foreach (string _email in _userEmails)
                        ReplyToEmails += _email + ",";
                }
                else if (_userEmails.Length == 1)
                    ReplyToEmails = _userEmails[0];

                var _caseTreatment = _patientService.GetPatientAndCaseByCaseID(viewModel.CaseID);
                var _CasePatientReferrer = _caseService.GetCasePatientReferrerByCaseID(viewModel.CaseID);
                string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/BookIAEmailTemplate.html");
                System.IO.StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                string _patientName = _caseTreatment.FirstName + " " + _caseTreatment.LastName;
                MailText = MailText.Replace("##ReferrerName##", _CasePatientReferrer.ReferrerName);
                MailText = MailText.Replace("##CaseReferrerReferenceNumber##", _caseTreatment.CaseReferrerReferenceNumber);
                MailText = MailText.Replace("##ReferenceNumber##", _caseTreatment.CaseNumber);
                MailText = MailText.Replace("##PatientName##", _patientName);
                MailText = MailText.Replace("##ServiceCategory##", _caseTreatment.TreatmentCategoryName);
                MailText = MailText.Replace("##ReferrerName##", _CasePatientReferrer.ReferrerName);

                MailText = MailText.Replace("##Appointmentdate##", _AppointmentDateAndTime[0]);
                MailText = MailText.Replace("##Appointmenttime##", _AppointmentDateAndTime[1].Substring(0, 5).ToString());                
                MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                MailText = MailText.Replace("##EmailLogo2Path##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                MailText = MailText.Replace("##EmailLogo3Path##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                
                result = _emailService.SendMultipleEmail(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"], ReplyToEmails, " Initial Assessment Booked", MailText);

                if (response > 0 )
                {
                    int userID = ITSUser.UserID;
                    _caseService.UpdateCaseWorkFlow(viewModel.CaseID, userID);
                }
                return Json(response);
                
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
