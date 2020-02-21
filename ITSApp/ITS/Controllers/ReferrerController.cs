using System.Collections.Generic;
using System.Web.Mvc;
using ITS.Domain.Models;
using AutoMapper;
using System;
using ITS.Infrastructure.Base;
using System.Configuration;
using System.Linq;
using ITS.Domain.ViewModels;
using ITS.Infrastructure.Global;
using ITS.Infrastructure.ApplicationFilters;



/*
 * Latest Version   : 1.3
 *
 * Modified By : Pardeep Kumar
 * Date        : 21-Nov-2012
 * Description : Added JsonResult method to return AllReferrer(GetAllReferrer)
 * Version     : 1.1
 * 
 * Modified By : Pardeep Kumar
 * Date        : 4-Dec-2012
 * Description : updated AddReferrer method to add referrer folder
 * Version     : 1.2
 * 
 * Updated By   : Anuj Batra
 * Date         : 11-dec-2012
 * Desc         : Added the method related to officelocationtab.
 * Version      : 1.3
 */


namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferrerController : BaseController
    {
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _ireferrerStorageService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IEncryption _iEncryptionService;
        private readonly ITSService.UserService.IUserService _userService;

        public ReferrerController(ITSService.ReferrerService.IReferrerService referrerService, ITS.Infrastructure.ApplicationServices.ReferrerStorageService referrerStorage
            , ITS.Infrastructure.ApplicationServices.Contracts.IEncryption iEncryptionService, ITSService.UserService.IUserService userService)
        {
            _referrerService = referrerService;
            _ireferrerStorageService = referrerStorage;
            _iEncryptionService = iEncryptionService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            ReferrerIndexViewModel referrerViewModel = new ReferrerIndexViewModel()
                {
                    Referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerModel.Referrer>>(_referrerService.GetReferrersRecentlyAdded())

                };
            foreach (var objResult in referrerViewModel.Referrers)
            {
                objResult.EncryptedReferrerID = EncryptString(objResult.ReferrerID.ToString());
            }

            return View(referrerViewModel);
        }

        [HttpGet]
        public ActionResult Detail(string referrerID)
        {
            int _Id = Convert.ToInt32(DecryptString(referrerID));
            if (_Id == null)
                return RedirectToAction(GlobalConst.Actions.ReferrerController.Index);

            ReferrerDetailViewModel viewModel = new ReferrerDetailViewModel();//populate supplier view model

            //GetByReferrerId method need to UpdateModel.
            viewModel.Referrer = Mapper.Map<ITS.Domain.Models.ReferrerModel.Referrer>(_referrerService.GetReferrerDetailsByReferrerID(_Id));
            viewModel.Referrer.EncryptedReferrerID = EncryptString(viewModel.Referrer.ReferrerID.ToString());
            viewModel.ReferrerLocations = Mapper.Map<List<ITS.Domain.Models.ReferrerModel.Location>>(_referrerService.GetReferrerLocationsByReferrerID(_Id));
            viewModel.ReferrerMainLocation = Mapper.Map<ITS.Domain.Models.ReferrerModel.Location>(_referrerService.GetReferrerMainLocation(_Id));
            viewModel.ReferrerProjects = Mapper.Map<List<ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryViewModel>>(_referrerService.GetReferrerProjectsByReferrerID(_Id));
            viewModel.ReferrerTreamentCategories = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerModel.TreatmentCategory>>(_referrerService.GetAllTreatmentCategory());
            viewModel.ReferrerGroupDetail = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerGroup>>(_referrerService.GetReferrerGroupUsersByreferrerID(_Id));
            viewModel.ReferrerProjects.ToList().ForEach(proj =>
            {
                proj.TreatmentCategories =
                Mapper.Map<IList<ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryPricingViewModel>>
                    (_referrerService.GetReferrerProjectTreatmentsByReferrerProjectID(proj.ReferrerProjectID));
                proj.TreatmentCategories.ToList().ForEach(treatmentCategory =>
                {
                    treatmentCategory.Pricing =
                        Mapper.Map<IList<ITS.Domain.Models.ReferrerModel.TreatmentPricing>>
                        (_referrerService.GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentIDAndTreatmentCategoryID(treatmentCategory.ReferrerProjectTreatmentID, treatmentCategory.TreatmentCategoryID));
                });
                
            });
            viewModel.ReferrerGroupDetail.ToList().ForEach(groupUser =>
            {
                groupUser.ReferrerData = Mapper.Map<IList<ITS.Domain.Models.ReferrerUserDetail>>(_referrerService.GetGroupUsersByReferrerIDAndName(groupUser.GroupName, groupUser.ReferrerID)); ;

            });

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetReferrerUsersByID(int _referrerID)
        {
            return Json(_userService.GetReferrerUsersByID(_referrerID), GlobalConst.Message.text_html);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(ITS.Domain.Models.ReferrerModel.Referrer referrer, ITS.Domain.Models.ReferrerModel.Location referrerLocation, string objIsPolicyDetail, string objIsEmploymentDetail, string objIsEmployeeDetail, string objIsDrugandAlcoholTest, string objIsRepresentation, string objIsAdditionalQuestion, string objIsJobDemand, string objIsPolicyDetailOpenOrDropdowns)
        {
            if (objIsPolicyDetail != null)
            {
                if (objIsPolicyDetail == "1")
                    referrer.IsPolicyDetail = true;
                else
                    referrer.IsPolicyDetail = false;
            }
            else
            {
                referrer.IsPolicyDetail = null;
            }


            if (objIsEmploymentDetail == "1")
                referrer.IsEmploymentDetail = true;
            else
                referrer.IsEmploymentDetail = false;
            if (objIsEmployeeDetail == "1")
                referrer.IsEmploeeDetail = true;
            else
                referrer.IsEmploeeDetail = false;
            if (objIsDrugandAlcoholTest == "1")
                referrer.IsDrugandAlcoholTest = true;
            else
                referrer.IsDrugandAlcoholTest = false;
            if (objIsRepresentation == "1")
                referrer.IsRepresentation = true;
            else
                referrer.IsRepresentation = false;
            if (objIsAdditionalQuestion == "1")
                referrer.IsAdditionalQuestion = true;
            else
                referrer.IsAdditionalQuestion = false;
            if (objIsJobDemand == "1")
                referrer.IsJobDemand = true;
            else
                referrer.IsJobDemand = false;
            referrer.IsPolicyDetailOpenOrDropdowns = objIsPolicyDetailOpenOrDropdowns == null ? "" : objIsPolicyDetailOpenOrDropdowns;
            referrer.IsNewPolicyTypes = referrer.IsNewPolicyTypes == null ? "" : referrer.IsNewPolicyTypes;

            referrer.ReferrerID = _referrerService.AddReferrerAndMainLocation(Mapper.Map<ITSService.ReferrerService.Referrer>(referrer), Mapper.Map<ITSService.ReferrerService.ReferrerLocation>(referrerLocation));
            bool referrerDirectoryCreated = _ireferrerStorageService.CreateReferrerFolder(referrer.ReferrerID, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]));
            return RedirectToAction(GlobalConst.Actions.ReferrerController.Detail, new { referrerID = EncryptString(referrer.ReferrerID.ToString()) });

        }

        [HttpPost]
        public ActionResult Search(ReferrerSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
        {
            switch (searchModel.ReferrerSearch.SearchCriteria)
            {
                case (int)GlobalConst.ReferrerSearchCriteria.ReferrerName:
                    var byNameResults = _referrerService.GetReferrerLocationReferrerLikeReferrerName(searchModel.ReferrerSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerModel.ReferrerSearchResult>>
                        (byNameResults.ReferrerLocationReferrers);
                    searchModel.TotalCount = byNameResults.ReferrerLocationReferrerCount;
                    break;
                default:
                     var nameResults = _referrerService.GetReferrerLocationReferrerLikeReferrerName(searchModel.ReferrerSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Referrers = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerModel.ReferrerSearchResult>>
                        (nameResults.ReferrerLocationReferrers);
                    searchModel.TotalCount = nameResults.ReferrerLocationReferrerCount;
                break;
            }

            foreach (var objResult in searchModel.Referrers)
            {
                objResult.EncryptedReferrerID = EncryptString(objResult.ReferrerID.ToString());
            }

            return View(searchModel);
        }

        #region Group
        [HttpPost]
        public ActionResult AddGroup(ReferrerGroup _referrerGroup)
        {
            UpdateReferrerGroup objdata = new UpdateReferrerGroup();
            objdata.GroupName = _referrerGroup.GroupName;
            objdata.ReferrerID = _referrerGroup.ReferrerID;
            objdata.UserID = String.Join(",", _referrerGroup.MultipleUserID.Select(p => p.ToString()).ToArray());
            if (_referrerGroup.UpdateCheck == 1)
            {                        
                   objdata.NewName = _referrerGroup.NewName;                                
                   var data = _referrerService.UpdateReferrerGroup(Mapper.Map<ITSService.ReferrerService.UpdateReferrerGroup>(objdata));
                   _referrerGroup.Msg = GlobalConst.Message.GroupUpdate;                
            }
            else
            {                
                   _referrerGroup.GroupID = _referrerService.AddReferrerGroup(Mapper.Map<ITSService.ReferrerService.UpdateReferrerGroup>(objdata));
                   if (_referrerGroup.GroupID == -1)
                   {
                       _referrerGroup.Msg = GlobalConst.Message.GroupExist;
                   }
                   else
                   {
                       _referrerGroup.Msg = GlobalConst.Message.GroupAdd; 
                   }                                
            }           
              _referrerGroup.EncryptedReferrerID = EncryptString(_referrerGroup.ReferrerID.ToString());
              return Json(_referrerGroup, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetUserDetail(int _id, string _name)
        {
            ReferrerUserDetailViewModel objdata = new ReferrerUserDetailViewModel();
            objdata.ReferrerDetailData = Mapper.Map<IEnumerable<ITS.Domain.Models.ReferrerUserDetail>>(_referrerService.GetGroupUsersByReferrerIDAndName(_name, _id));
           
            return Json(objdata);
        }

        [HttpPost]
        public JsonResult DeleteGroupUser(int groupIID)
        {
            _referrerService.DeleteGroupByID(groupIID);
            return Json(groupIID);
        } 
        #endregion
    }
}
