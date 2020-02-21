using AutoMapper;
using ITS.Infrastructure.ApplicationServices;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using System;
using System.Text;
using System.Web.Mvc;


namespace ITSPublicApp.Web.Controllers
{

    public class HomeController : BaseController
    {
        private readonly ITSService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;
        private readonly IITSCommonService _IITSCommonService;
        private readonly EmailService _emailService;
        public HomeController(ITSService.UserService.IUserService userService, IITSCommonService IITSCommonService, IEncryption encryptionService, EmailService emailService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _IITSCommonService = IITSCommonService;
            _emailService = emailService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            ITSUser user = Mapper.Map<ITSUser>(_userService.GetUserByUserName(model.User.UserName));
            try
            {

                if (user != null)
                {
                    if (user.IsLocked)
                    {
                        model.InvalidMsg = GlobalResource.UserIsLockMessage;
                        return View(GlobalConst.Views.Home.Index, model);
                    }

                    var _us = _userService.GetUserById(user.UserID);

                    double TotalDay = (_us.PasswordExpirationDate.Value - System.DateTime.Now).TotalDays;

                    user.UserIDEncry = EncryptString(user.UserID.ToString());
                    if ((TotalDay < 90) && (!TotalDay.ToString().Contains("-")))
                    {

                        if (model.User.UserName == user.UserName && _encryptionService.VerifyHashedPassword(model.User.Password, user.Password) && user.UserTypeID != (int)GlobalConst.UserType.Internal)
                        {
                            user.Password = null;
                            // user.Email = null;
                            user.UserName = null;
                            ITSUser = user;
                            _userService.ResetUserFailedAttemptCount(ITSUser.UserID);
                            _userService.UpdateUserSessionIDByUserID(ITSUser.UserID, this.Session.SessionID);
                            switch (user.UserTypeID)
                            {
                                case (int)GlobalConst.UserType.Referrer:
                                    return RedirectToAction(
                                        GlobalConst.Actions.Area.Referrer.HomeController.Index,
                                        GlobalConst.Controllers.Area.Referrer.Home,
                                        new { area = GlobalConst.Areas.Referrer });
                                case (int)GlobalConst.UserType.Supplier:
                                    return RedirectToAction(
                                        GlobalConst.Actions.Area.Supplier.HomeController.Index,
                                        GlobalConst.Controllers.Area.Supplier.Home,
                                        new { area = GlobalConst.Areas.Supplier });
                            }

                        }
                        else
                        {
                            _userService.UpdateUserFailedAttemptCount(user.UserID);
                            model.InvalidMsg = GlobalResource.InvalidPassword;
                            user.Password = null;
                            user.Email = null;
                            user.UserName = null;
                        }
                    }
                    else
                    {
                        model.InvalidMsg = GlobalResource.PasswordExpired + ". A new link has been sent to your registered Email ID.";
                        Random Generator = new Random();
                        String _strOTP = Generator.Next(0, 999999).ToString("D6");
                        _userService.UpdatePasswordOTPByID(_us.UserID, _strOTP);
                        string x60MinsLater = System.DateTime.Now.ToString();
                        string _concatinate = user.UserID.ToString() + "," + x60MinsLater + "," + _strOTP;
                        string encryptedData = EncryptString(_concatinate);
                        StringBuilder sb = new StringBuilder();
                        sb.Append("**THIS IS AN AUTOMATED MESSAGE - PLEASE DO NOT REPLY DIRECTLY TO THIS EMAIL**");
                        sb.Append("<br><br>");
                        sb.Append("Hello " + user.FirstName + " " + user.LastName + ",");
                        sb.Append("<br><br>");
                        sb.Append("Your password has expired. Please use the following link and One Time Password to update your account.");
                        sb.Append("<br><br>");
                        sb.Append("One Time Password: " + _strOTP);
                        sb.Append("<br>");
                        sb.Append("Link: <a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "User/ResetPassword/" + encryptedData + @"/" + EncryptString("1").ToString() + "'>Reset Password</a><br><br>");
                        sb.Append("One Time Password and Link will expire in " + x60MinsLater);
                        sb.Append("<br><br>");
                        sb.Append("If you did not request this please disregard this email.");
                        sb.Append("<br><br>");
                        sb.Append("Thank You,");
                        sb.Append("<br><br>");
                        sb.Append("Innovate Support Team");
                        _emailService.SendGeneralEmail(user.Email, "", "Reset Password", sb.ToString());
                        sb.Clear();
                    }
                }
                else
                {
                    model.InvalidMsg = GlobalResource.InvalidUserName;
                    user.Password = null;
                    user.Email = null;
                    user.UserName = null;
                }
                model.User.Password = null;
                model.User.Email = null;
                model.User.UserName = null;
                model.User.UserID = 0;                
            }
            catch (Exception ex)
            {
                _IITSCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                Response.Redirect("/Home/Error");
            }
            return View(GlobalConst.Views.Home.Index, model);
        }
        [HttpPost]
        public ActionResult EmailSendByEmailID(string email)
        {
            ITSUser user = Mapper.Map<ITSUser>(_userService.GetUserRecordByEmailID(email));
            try
            {
                if (user != null)
                {


                    dynamic showMessageString = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Hello ");
                    sb.Append("<br><br>");
                    sb.Append("Your username is " + user.UserName + ", ");
                    sb.Append("Please click the link below and login to your portal.");
                    sb.Append("<br>");
                    sb.Append("To login in your account, please follow this link: <a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + @"'>login</a>.");
                    sb.Append("<br><br>");
                    sb.Append("<br><br>");
                    sb.Append("Thank You,<br>Innovate Team");
                    _IITSCommonService.SendGeneralEmail(email, "", "Forgot UserName", sb.ToString());
                    sb.Clear();
                    TempData["Message"] = "Email Sent Successfully";
                }
                else
                {
                    TempData["Message"] = "Email not exists";
                }
               

            }
            catch(Exception ex)
            {
                _IITSCommonService.CreateErrorLog(ex.Message, ex.StackTrace);
                Response.Redirect("/Home/Error");
                //TempData["Message"] = "Error occured please contact to administrator";
                //return RedirectToAction(GlobalConst.Views.Home.Index);

            }
            return RedirectToAction(GlobalConst.Views.Home.Index);
        }

        [HttpPost]
        public JsonResult Resetpassword(string email, string username)
        {
            ITSUser user = Mapper.Map<ITSUser>(_userService.GetUserRecordByEmailAndUserName(email, username));
            if (user != null)
            {
                Random Generator = new Random();
                String _strOTP = Generator.Next(0, 999999).ToString("D6");
                _userService.UpdatePasswordOTPByID(user.UserID, _strOTP);
                string _datetime = System.DateTime.Now.ToString();
                DateTime currentTime = DateTime.Now;
                DateTime x60MinsLater = currentTime.AddMinutes(60);

                string _concatinate = user.UserID.ToString() + "," + _datetime + "," + _strOTP;
                string encryptedData = EncryptString(_concatinate);
                StringBuilder sb = new StringBuilder();
                sb.Append("**THIS IS AN AUTOMATED MESSAGE - PLEASE DO NOT REPLY DIRECTLY TO THIS EMAIL**");
                sb.Append("<br><br>");
                sb.Append("Hello " + user.FirstName + " " + user.LastName + ",");
                sb.Append("<br><br>");
                sb.Append("Please use the following link and One Time Password to update your account.");
                sb.Append("<br><br>");
                sb.Append("One Time Password: " + _strOTP);
                sb.Append("<br>");
                sb.Append("Link: <a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "User/ResetPassword/" + encryptedData + @"/" + EncryptString("0").ToString() + "'>Reset Password</a><br><br>");
                sb.Append("One Time Password and Link will expire in " + x60MinsLater);
                sb.Append("<br><br>");
                sb.Append("If you did not request this please disregard this email.");
                sb.Append("<br><br>");
                sb.Append("Thank You,");
                sb.Append("<br><br>");
                sb.Append("Innovate Support Team");
                _IITSCommonService.SendGeneralEmail(email, "", "Forgot Password", sb.ToString());
                sb.Clear();
                return Json(GlobalConst.Message.EmailSentToRegisteredEmail, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(GlobalConst.Message.EmailNotExists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CleardLoginSession(string userID)
        {
            _userService.ResetUserSessionID(Int32.Parse(DecryptString(userID)));
            Session.Clear();
            Session.Abandon();
            return Json("LoginSessoinCleared");
        }

        [HttpPost]
        public JsonResult CleardLoginSessionIdle()
        {
            if (ITSUser != null)
            {
                _userService.ResetUserSessionID(ITSUser.UserID);
                Session.Clear();
                Session.Abandon();
            }
            return Json("LoginSessoinCleared");
        }

        [HttpPost]
        public JsonResult ValidateLoginUserSession(string UserName)
        {
            var _res = _userService.GetUserByUserName(UserName);
            if (_res != null)
                return Json((_res.UserSessionID == null ? "na" : EncryptString(_res.UserID.ToString())), JsonRequestBehavior.AllowGet);
            else
                return Json("na", JsonRequestBehavior.AllowGet);
        }


        public ActionResult LogOut()
        {
            if (ITSUser != null)
            {
                _userService.ResetUserSessionID(ITSUser.UserID);
                Session.Clear();
                Session.Abandon();
            }
            return RedirectToAction(GlobalConst.Actions.HomeController.Index);
        }

        public ActionResult Error()
        {
            if (ITSUser != null)
            {
                if (ITSUser.UserTypeID == 2)
                    ViewBag.Message = 2;
                else
                    ViewBag.Message = 1;
            }
            return View();
        }
    }
}
