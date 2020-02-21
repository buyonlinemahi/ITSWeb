using System.Web.Mvc;
using AutoMapper;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Text;
using ITS.Infrastructure.ApplicationServices;
using System;
using System.Web;
using System.Collections.Generic;
using AutoMapper;
using System.ServiceModel;
using ITS.Infrastructure.ApplicationFilters;

/// <summary>
/// Latest Version : 1.3
/// Decription: Validations to check user login Details
/// 
///  Revision History:                                        
/// 1.0 – 10/26/2012  Pardeep Kumar 
/// Purpose: Craeted Action Index to display login Index view
/// 
/// Edited By : Harpreet Singh
/// Task #3 : Craeted the Wcf Call to It service and Checked the Login Details
/// version : 1.1
/// Date :  02-Nov-2012
/// 
/// Edited By : Robin Singh
/// Description : Validations to check user login details
/// version : 1.2
/// Date : 02-Nov-2012
/// 
/// 1.3 – 11/05/2012 Anuj Batra
/// Purpose: Updated Index(HttpPost) method to implement the login functionality i.e 5 failed login attempt's logic.    
/// </summary>

namespace ITSWebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITSService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;
        private readonly EmailService _emailService;
       
        public HomeController(ITSService.UserService.IUserService userService, IEncryption encryptionService, EmailService emailService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _emailService = emailService;
           
        }

        [HttpGet]
        public ActionResult Index(string ReturnUrl)
        {            
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            ModelState.Clear();
            ITSUser user = Mapper.Map<ITSUser>(_userService.GetUserByUserName(model.User.UserName));
            if (user != null)
            {
                if (user.IsLocked)
                {
                    model.InvalidMsg = GlobalResource.UserIsLockMessage;
                    return View(GlobalConst.Views.Home.Index, model);
                }
                var _us = _userService.GetUserById(user.UserID);
                double TotalDay = (_us.PasswordExpirationDate.Value - System.DateTime.Now).TotalDays;
                if ((TotalDay < 90) && (!TotalDay.ToString().Contains("-")))
                {
                    if (model.User.UserName == user.UserName && _encryptionService.VerifyHashedPassword(model.User.Password, user.Password))
                    {
                        ITSUser = user;
                        _userService.ResetUserFailedAttemptCount(ITSUser.UserID);
                        _userService.UpdateUserSessionIDByUserID(ITSUser.UserID, this.Session.SessionID);
                        switch (user.UserTypeID)
                        {
                            case (int)GlobalConst.UserType.Internal:
                                {
                                    return RedirectToAction(GlobalConst.Actions.InternalTasksController.Index, GlobalConst.Controllers.InternalTasks, new { area = "" });
                                }
                            default:
                                model.InvalidMsg = GlobalResource.InvalidUserName;
                                break;
                        }
                    }
                    else
                    {
                        model.InvalidMsg = GlobalResource.InvalidPassword;
                        _userService.UpdateUserFailedAttemptCount(user.UserID);
                    }
                }
                else
                {
                    model.InvalidMsg = GlobalResource.PasswordExpired + ". A new link has been sent to your registered Email ID.";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Hello  " + user.FirstName + " " + user.LastName + "");
                    sb.Append("<br><br>");
                    sb.Append("Your password has expired. Please follow the link below and change your password.");
                    sb.Append("<br><br>");
                    if (user.UserTypeID == 1)
                    {
                        // Internal
                        sb.Append("<a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "Home/ResetPassword/" + user.UserID + @"'>Reset Password</a>");
                    }
                    else
                    {
                        // Referrer & Supplier
                        sb.Append("<a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "User/ResetPassword/" + user.UserID + @"'>Reset Password</a>");
                    }
                    sb.Append("<br><br>");
                    sb.Append("Thank You,<br>Innovate Team");
                    _emailService.SendGeneralEmail(user.Email, "", "Reset Password", sb.ToString());
                    sb.Clear();
                }
            }
            else
                model.InvalidMsg = GlobalResource.InvalidUserName;
            return View(GlobalConst.Views.Home.Index, model);
        }

        [HttpGet]
        public ActionResult ResetPassword(int id)
        {
            ViewBag.UserID = id;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string UserPassword, int UserID)
        {
            try
            {                
                _userService.ResetUserPassword(UserID, _encryptionService.HashPassword(UserPassword));
                ViewBag.ReturnMessage = GlobalConst.Message.PasswordUpdatedSuccessfully;
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.ReturnMessage = "Error : " + ex.Message;
                return View();
            }            
        }

        public ActionResult LogOut()
        {
            string sessionId = this.Session.SessionID;
            if (ITSUser != null)
            {
                _userService.ResetUserSessionID(ITSUser.UserID);
                Session.Clear();
                Session.Abandon();
            }
            return RedirectToAction(GlobalConst.Actions.HomeController.Index);
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
        public JsonResult ValidateLoginUserSession(string UserName)
        {
            var _res =_userService.GetUserByUserName(UserName);
            if (_res != null)
                return Json((_res.UserSessionID == null ? "na" : EncryptString(_res.UserID.ToString())), JsonRequestBehavior.AllowGet);
            else
                return Json("na", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Error()
        {
            //string sessionId = this.Session.SessionID;
            //_userService.ResetUserSessionID(sessionId); 
           return View();
        }

       
        public JsonResult ResetUserpassword(int uid)
        {
            var user = _userService.GetUserById(uid);
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
                sb.Append("Hello " + user.FirstName + " " + user.LastName + ",");
                sb.Append("<br><br>");
                sb.Append("This is your One Time Code: " + _strOTP);
                sb.Append("<br><br>");
                sb.Append("Please follow this link to reset your password.");
                sb.Append("<br>");
                sb.Append("Link: <a href='" + System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + "User/ResetPassword/" + encryptedData + @"/" + EncryptString("0").ToString() + "'>Reset Password</a><br><br>");
                sb.Append("One Time Password and Link will expire in 24 Hours " + x60MinsLater);
                sb.Append("<br><br>");
                sb.Append("Thank You,");
                sb.Append("<br><br>");
                sb.Append("Innovate Support Team");
                _emailService.SendGeneralEmail(user.Email, "", "Forgot Password", sb.ToString());
                sb.Clear();
                return Json(GlobalConst.Message.EmailSentToRegisteredEmail, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(GlobalConst.Message.EmailNotExists, JsonRequestBehavior.AllowGet);
        }

    }
}

