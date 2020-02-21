using AutoMapper;
using ITSPublicApp.Domain.Models;
using ITSPublicApp.Domain.ViewModels;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using ITSPublicApp.Infrastructure.Base;
using ITSPublicApp.Infrastructure.Global;
using ITSPublicApp.Web.ITSService.UserService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ITSPublicApp.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        private readonly ITSService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;
        public UserController(ITSService.UserService.IUserService userService, IEncryption encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
        }

        public ActionResult ResetPassword(string id, string cnt)
        {
            string[] DecryptData = new string[1];
            DecryptData[0] = DecryptString2(id);
            var _data = DecryptData[0].Split(',');
            string _userId = _data[0];

            

            DateTime _datetime = Convert.ToDateTime(_data[1]);
            DateTime date1 = _datetime;
            DateTime date2 = System.DateTime.Now;
            TimeSpan ts = date2 - date1;    
            ITSUser user = new ITSUser();

            user.PasswordOTPDB = _userService.GetUserById(int.Parse(_userId)).PasswordOTP;

            user.UserIDEncry = EncryptString(_userId);
            user.Cnt = int.Parse(DecryptString(cnt)); 
            user.PasswordOTP = EncryptString(_data[2]);
            if (ts.TotalHours > 1)
                user.Message = "Expired";

            user.Email = null;
            user.UserName = null;
            user.Password = null;
            user.UserID = 0;
            return View(user);
        }

        [HttpPost]
        public JsonResult ResetPassword(string _userID, string _password, string _passwordValidateOTP, string _passwordOTP)
        {
            int UID = Convert.ToInt32(DecryptString(_userID));

            if (_passwordValidateOTP.Trim() != _passwordOTP.Trim())
                return Json("OTPNotMatch", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);

            PasswordHistoryViewModel objpas = new PasswordHistoryViewModel();
            ITSPublicApp.Domain.Models.PasswordHistory objPassword = new ITSPublicApp.Domain.Models.PasswordHistory();
            objpas.PasswordHistoryDetails = Mapper.Map<IEnumerable<ITSService.UserService.PasswordHistory>, IEnumerable<ITSPublicApp.Domain.Models.PasswordHistory>>(_userService.GetPasswordHistoryByUserID(UID));

            foreach (ITSPublicApp.Domain.Models.PasswordHistory phDetails in objpas.PasswordHistoryDetails)
            {
                if (_encryptionService.VerifyHashedPassword(_password, phDetails.Password))
                {
                    return Json("Matched", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
                }
            }
            if (UID > 0)
            {
                _userService.ResetUserPassword(UID, _encryptionService.HashPassword(_password));
                objPassword.UserID = UID;
                objPassword.Password = _encryptionService.HashPassword(_password);
                objPassword.CreatedOn = System.DateTime.Now;
                var PasswordHistoryID = _userService.AddPasswordHistory(Mapper.Map<ITSService.UserService.PasswordHistory>(objPassword));
                return Json("Yes", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("No", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUserByID(string UserId)
        {
            var _Users = Mapper.Map<User>(_userService.GetUserById(Convert.ToInt32(DecryptString(UserId))));
            _Users.Email = null;
            _Users.UserName = null;
            _Users.Password = null;
            return Json(_Users);
        }
    }
}
