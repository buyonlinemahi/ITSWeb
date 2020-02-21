using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITS.Infrastructure.Global;
using AutoMapper;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Infrastructure.Base;
using ITS.Domain.Models;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class UserSharedController : BaseController
    {
        //
        // GET: /UserShared/
        private readonly ITSService.UserService.IUserService _userService;
         private readonly ITSService.ReferrerService.IReferrerService _referrerService;
         private readonly ITSService.SupplierService.ISupplierService _supplierService;
         public UserSharedController(ITSService.UserService.IUserService userService, ITSService.ReferrerService.IReferrerService referrerService, ITSService.SupplierService.ISupplierService supplierService)
        {
            _userService = userService;
            _referrerService = referrerService;
            _supplierService = supplierService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UserExists(string value, string field)
        {
            return Json(new { valid = !_userService.GetUserExistsByName(value), message = "User already exists", value = value });
        }
        [HttpPost]
        public ActionResult SearchUser(ITS.Domain.ViewModels.UserSearchResultViewModel searchModel)
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

            return Json(searchModel, "text/html");
        }

        //NewSection
        [HttpPost]
        public ActionResult GetLocationsByReferrerID(int referrerID)
        {
            var locations = Mapper.Map<IEnumerable<ReferrerLocation>>(_referrerService.GetReferrerLocationsByReferrerID(referrerID));
            return Json(locations);
        }
        [HttpPost]
        public ActionResult GetSuppliers()
        {
            var suppliers = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.SuppliersName>>(_supplierService.GetAllSupplier());
            return Json(suppliers);
        }
        [HttpPost]
        public ActionResult GetReferrers()
        {

            IEnumerable<ITS.Domain.Models.UserModel.ReferrersName> referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.UserModel.ReferrersName>>(_referrerService.GetAllReferrer());
            return Json(referrers);
        }
    }
}
