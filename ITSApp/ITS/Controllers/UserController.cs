using System.Collections.Generic;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using System.ServiceModel;
using ITS.Infrastructure.Base;
using System;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.ApplicationServices.Contracts;


namespace ITSWebApp.Controllers
{
    //[AuthorizedUserCheck]
    //[ValidSessionCheck]
    public class UserController : BaseController
    {
        private readonly ITSService.UserService.IUserService _userService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        public readonly IEncryption _encryptionService;
        public UserController(ITSService.UserService.IUserService userService, ITSService.SupplierService.ISupplierService supplierService,
            ITSService.ReferrerService.IReferrerService referrerService, IEncryption encryptionService)
        {
            _userService = userService;
            _referrerService = referrerService;
            _supplierService = supplierService;
            _encryptionService = encryptionService;
        }

        [HttpGet]
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
                return Json("OTPNotMatch", GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);

            PasswordHistoryViewModel objpas = new PasswordHistoryViewModel();
            ITS.Domain.Models.PasswordHistory objPassword = new ITS.Domain.Models.PasswordHistory();
            objpas.PasswordHistoryDetails = Mapper.Map<IEnumerable<ITSService.UserService.PasswordHistory>, IEnumerable<ITS.Domain.Models.PasswordHistory>>(_userService.GetPasswordHistoryByUserID(UID));

            foreach (ITS.Domain.Models.PasswordHistory phDetails in objpas.PasswordHistoryDetails)
            {
                if (_encryptionService.VerifyHashedPassword(_password, phDetails.Password))
                {
                    return Json("Matched", GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
                }
            }
            if (UID > 0)
            {
                _userService.ResetUserPassword(UID, _encryptionService.HashPassword(_password));
                objPassword.UserID = UID;
                objPassword.Password = _encryptionService.HashPassword(_password);
                objPassword.CreatedOn = System.DateTime.Now;
                var PasswordHistoryID = _userService.AddPasswordHistory(Mapper.Map<ITSService.UserService.PasswordHistory>(objPassword));
                return Json("Yes", GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("No", GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }

        #region New User Methods
        public ActionResult Index()
        {

            var _res = _userService.GetUsersRecentlyAdded();

            IEnumerable<ITS.Domain.Models.UserModel.User> users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.User>>(_userService.GetUsersRecentlyAdded());

            return View(users);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(ITS.Domain.Models.UserModel.User user)
        {

            ITSWebApp.ITSService.UserService.UserProject userpro = new ITSService.UserService.UserProject();
            PasswordHistory objPassword = new PasswordHistory();

            user.Password = _encryptionService.HashPassword(user.Password);
            if (user.SupplierTypes == null)
            {
                user.SupplierTypes = "";
            }
            if (user.ReferrerTypes == null)
            {
                user.ReferrerTypes = "";
            }
            user.UserID = _userService.AddUser(Mapper.Map<ITSService.UserService.User>(user));
            objPassword.UserID = user.UserID;
            objPassword.Password = user.Password;
            objPassword.CreatedOn = System.DateTime.Now;
            var PasswordHistoryID = _userService.AddPasswordHistory(Mapper.Map<ITSService.UserService.PasswordHistory>(objPassword));
            if (user.ReferrerProjectID != null)
            {
                if (user.ReferrerProjectID.Length > 0)
                {
                    userpro.ReferrerProjectID = user.ReferrerProjectID;
                    userpro.UserID = user.UserID;
                    _userService.AddUserProject(userpro); // adding projects for User
                }
            }
            return RedirectToAction(GlobalConst.Actions.UserController.Detail, new { userID = user.UserID });
        }
        public ActionResult Detail(int? userID)
        {
            if (!userID.HasValue)
            {
                return RedirectToAction(GlobalConst.Actions.UserController.Index);
            }

            UserDetailViewModel viewModel = new UserDetailViewModel();
            viewModel.User = Mapper.Map<ITS.Domain.Models.UserModel.User>(_userService.GetUserById(userID.Value));
            viewModel.User.Password = null;
            if (viewModel.User.ReferrerID != null)
            {
                if (viewModel.User.ReferrerTypes == "Referrer Project User")
                {
                    viewModel.ReferrerProjects = Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerCompleteProjectsByReferrerID(viewModel.User.ReferrerID.Value));
                }
                else
                {
                    viewModel.ReferrerProjects = Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerProjectNotAssignedToUser(viewModel.User.ReferrerID.Value, viewModel.User.UserID));
                }

                viewModel.UserProjects = Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerProjectAssignedToUser(viewModel.User.ReferrerID.Value, viewModel.User.UserID));
            }

            if (viewModel.User.UserTypeID == (int)GlobalConst.UserType.Referrer)
            {
                viewModel.Referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.ReferrersName>>(_referrerService.GetAllReferrer());
                viewModel.ReferrerLocations = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerLocation>>(_referrerService.GetReferrerLocationsByReferrerID(viewModel.User.ReferrerID.Value));
            }
            else if (viewModel.User.UserTypeID == (int)GlobalConst.UserType.Supplier)
            {
                viewModel.Suppliers = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.SuppliersName>>(_supplierService.GetAllSupplier());
            }

            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(ITS.Domain.ViewModels.UserSearchResultViewModel searchModel)
        {
            switch (searchModel.UserSearch.SearchCriteria)
            {
                case (int)GlobalConst.UserSearchCriteria.Name:
                    var byNameResults = _userService.GetUserTypeUsersLikeName(searchModel.UserSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.UserSearchResult>>(byNameResults.userTypeUser);
                    searchModel.TotalCount = byNameResults.TotalCount;
                    break;
                case (int)GlobalConst.UserSearchCriteria.UserName:
                    var byUserNameResults = _userService.GetUserTypeUsersLikeUsername(searchModel.UserSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.UserSearchResult>>(byUserNameResults.userTypeUser);
                    searchModel.TotalCount = byUserNameResults.TotalCount;
                    break;
                case (int)GlobalConst.UserSearchCriteria.UserTypeName:
                    var byUserTypeResults = _userService.GetUserTypeUsersLikeUserType(searchModel.UserSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.UserSearchResult>>(byUserTypeResults.userTypeUser);
                    searchModel.TotalCount = byUserTypeResults.TotalCount;
                    break;
                case (int)GlobalConst.UserSearchCriteria.ReferrerName:


                    var byReferrerNameResults = _userService.GetUserTypeUsersLikeReferrerName(searchModel.UserSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.UserSearchResult>>(byReferrerNameResults.userTypeUser);
                    searchModel.TotalCount = byReferrerNameResults.TotalCount;
                    break;
                case (int)GlobalConst.UserSearchCriteria.SupplierName:


                    var bySupplierNameResults = _userService.GetUserTypeUsersLikeSupplierName(searchModel.UserSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Users = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.UserSearchResult>>(bySupplierNameResults.userTypeUser);
                    searchModel.TotalCount = bySupplierNameResults.TotalCount;
                    break;
            }

            return View(searchModel);
        }
        [HttpPost]
        public JsonResult Update(ITS.Domain.Models.UserModel.User user)
        {
            //if (user.IsPasswordDirty)
            //{
            //    user.Password = _encryptionService.HashPassword(user.Password);
            //}

            user.Password ="no";
            
            if (user.SupplierTypes == null)
            {
                user.SupplierTypes = "";
            }
            if (user.ReferrerTypes == null)
            {
                user.ReferrerTypes = "";
            }
            
            _userService.UpdateUser(Mapper.Map<ITSService.UserService.User>(user));
            if (user.ReferrerTypes == "Referrer Project Admin" && user.OldUserProjectSingleValue != null)
            {
                _userService.DeleteUserProject(user.OldUserProjectSingleValue.Value, user.UserID);
            }
            ITSWebApp.ITSService.UserService.UserProject userpro = new ITSService.UserService.UserProject();
            userpro.ReferrerProjectID = user.ReferrerProjectID;
            userpro.UserID = user.UserID;
            if (user.ReferrerProjectID != null)
            {
                if (user.ReferrerProjectID.Length > 0)
                {
                    userpro.ReferrerProjectID = user.ReferrerProjectID;
                    userpro.UserID = user.UserID;
                    _userService.AddUserProject(userpro);
                }
            }
            _userService.UpdateUserLock(user.UserID, user.IsLocked);

            return Json(user, "text/html");
        }
        #endregion
        [HttpPost]
        public JsonResult GetReferrerProjectsByReferrerID(int referrerID)
        {
            return Json(Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerCompleteProjectsByReferrerID(referrerID)));
        }
        [HttpPost]
        public JsonResult GetReferrerProjectAssignedToUser(int referrerID, int userID)
        {
            return Json(Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerProjectAssignedToUser(referrerID, userID)));
        }
        [HttpPost]
        public JsonResult DeleteUserProject(int referrerProjectID, int userID)
        {
            return Json(_userService.DeleteUserProject(referrerProjectID, userID));
        }
        #region User Methods Used in Areas
        [HttpPost]
        public JsonResult GetUser(int userID)
        {
            ITSUser user = Mapper.Map<ITSUser>(_userService.GetUserById(userID));
            return Json(user);
        }
        [HttpPost]
        public JsonResult AddUser(ITSUser user)
        {
            Mapper.CreateMap<ITSUser, ITSService.UserService.User>();
            ITSService.UserService.User _adduser = Mapper.Map<ITSService.UserService.User>(user);
            _userService.AddUser(_adduser);

            return Json(user);
        }
        [HttpPost]
        public JsonResult UserNameAutoComplete(string searchKey)
        {
            IEnumerable<ITSUser> users = Mapper.Map<IEnumerable<ITSUser>>(_userService.GetUserByUserNameAutoComplete(searchKey));
            return Json(users);
        }

       
        #endregion
    }
}
