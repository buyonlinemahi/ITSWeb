using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ITS.Domain.Models;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
/*
 * Latest Version 1.21
 * 
 * Author       : Harpreet Singh
 * Date         : 27-Oct-2012
 * Purpose      : Partial View and Auto complete
 * Version      : 1.0

 * Author       : Robin Singh
 * Date         : 16-Nov-2012
 * Purpose      : Adeded Method GetReferrerProjectsbyReferrerID  and AddReferrerProjects 
 * Version      : 1.1

 * Author       : Robin Singh
 * Date         : 22-Nov-2012
 * Purpose      : Adeded Method AddReferrerProjectTreatment 
 * Version      : 1.2

 * Author       : Anuj Batra
 * Date         : 27-Nov-2012
 * Purpose      : Adeded Method to add and delete the referrerLocation 
 * Version      : 1.3

 * Author       : Robin Singh 
 * Date         : 27-Nov-2012
 * Purpose      : Adeded Method GetPricingTypesByTreatmentCategoryID to get  PricingTypes Details.
 * Version      : 1.4
 
 * Author       : Robin Singh
 * Date         : 28-Nov-2012
 * Purpose      : Added Method UpdateReferrerProjects To Update ReferrerProjects ,AddReferrerProjectTreatmentPricing for AddPricings and AddProjectTreatmentSLA for AddTreatmentSLAs.
 * Version      : 1.5
 * 
 * Author       : Manjit Singh 
 * Date         : 29-Nov-2012
 * Purpose      : Added and Updated  AddReferrerProjectTreatmentAssessment for Assesment.
 * Version      : 1.6
  
 * Author       : Anuj Batra 
 * Date         : 30-Nov-2012
 * Purpose      : Added method for update referrer location information.
 * Version      : 1.7
 
 * Author       : Manjit Singh  
 * Date         : 01-Dec-2012
 * Purpose      : Added and Updated  AddReferrerProjectTreatmentAssessment for Assesment.
 * Version      : 1.8

 * Author       : Manjit Singh  
 * Date         : 03-Dec-2012
 * Purpose      : Validation to update if its already an existing assessment(AddReferrerProjectTreatmentAssessment for Assesment)
 * Version      : 1.9
 
 * Modified By : Pardeep Kumar
 * Date        : 4-Dec-2012
 * Description : updated AddReferrerProject method to add referrerProject folder
 * Version     : 1.10

 * Modified By : Anuj Batra
 * Date        : 4-Dec-2012
 * Description : updated AddReferrerProjectTreatmentAssessment method to add refferrer Assessment Service.
 * Version     : 1.11
 
 * 
 * Modified By : Vishal Sen
 * Date        : 6-Dec-2012
 * Description : Add, UpdateProjectTreatmentAuthorozation, GetallDelagateAuthorization
 * Version     : 1.12

 * Modified By : Vijay Kumar
 * Date        : 7-Dec-2012
 * Description : Create a method to GetAllEmailTypes
 * Version     : 1.13
 * 
 * Modified By : Robin Singh
 * Date        : 07-Dec-2012
 * Description : Create a method GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentID to Get Price Details.
 * Version     : 1.14

 * Modified By : Anuj Batra & Vijay
 * Date        : 08-Dec-2012
 * Description : Create a method GetAllReferrerProjectTreatmentDocumentsByReferrerProjectTreatmentID to Get documents details.Also modify the GetAllReferrerProjectTreatmentEmailsByReferrerProjectTreatmentID for the same functionality.
 * Version     : 1.15
 
 * Modified By :vishal
 * Date        : 08-Dec-2012
 * Description : modify method GetAllDelegatedAuthorisation And a view in the existiing data
 * Version     : 1.16
 
 * Modified By : Anuj Batra
 * Date        : 10-Dec-2012
 * Description : Create a method to update the project treatment document setup values.
 * Version     : 1.17
 
 * Modified By : Vijay & Anuj Batra
 * Date        : 10-Dec-2012
 * Description : Create a method to update the project treatment Email setup values.
 * Version     : 1.18

 * Modified By : Manjit Singh
 * Date        : 11-Dec-2012
 * Description : Created a method to update Assessment Services.
 * Version     : 1.19

 * Modified By : Robin Singh
 * Date        : 13-Dec-2012
 * Description : Created a method to Get GetProjectTreatmentSLAsByReferrerProjectTreatmentID.
               : Created a method UpdateReferrerProjectTreatment to Update Referrer Project Treatment
               : Created a method GetReferrerProjectTreatmentByReferrerProjectTreatmentID to Get ReferrerProjectTreatment By ReferrerProjectTreatmentID
 * Version     : 1.20

 * Updated By  : Anuj Batra
 * Date        : 14-Dec-2012
 * Description : Cleanup the code by deleting all unused actions. 
 * Version     : 1.21
 */

namespace ITSWebApp.Areas.Internal.Controllers
{

    public class ReferrerSetupController : BaseController
    {

        private readonly ITSService.ReferrerService.IReferrerService _referrerService;
        private readonly ITSService.UserService.IUserService _userService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _ireferrerStorageService;

        public ReferrerSetupController(ITSService.UserService.IUserService userService, ITSService.ReferrerService.IReferrerService referrerService, ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage)
        {
            _userService = userService;
            _referrerService = referrerService;
            _ireferrerStorageService = referrerStorage;
        }

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ReferrerPartial()
        {
            return PartialView();
        }
        public PartialViewResult ProjectControlPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AddReferrerProject(ReferrerProject referrerproject)
        {
            int referrerProjectID = _referrerService.AddReferrerProject(Mapper.Map<ITSService.ReferrerService.ReferrerProject>(referrerproject));

            foreach (ReferrerProjectTreatment referrerProjectTeatment in referrerproject.TreatmentCategories)
            {
                ITSService.ReferrerService.ReferrerProjectTreatment projectTreatment = Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatment>(referrerProjectTeatment);
                projectTreatment.ReferrerProjectID = referrerProjectID;

                projectTreatment.AccountReceivableChasingPoint = 0;
                projectTreatment.AccountReceivableCollection = 0;
                referrerProjectTeatment.ReferrerProjectID = referrerProjectID;
                referrerProjectTeatment.ReferrerProjectTreatmentID = _referrerService.AddReferrerProjectTreatment(projectTreatment);


                bool folderCreated = _ireferrerStorageService.CreateReferrerProjectFolder(Convert.ToInt16(referrerproject.ReferrerID), referrerProjectTeatment.ReferrerProjectTreatmentID, Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]));
            }

            return Json(referrerproject.TreatmentCategories.Where(treatment => treatment.Enabled));
        }

        [HttpPost]
        public JsonResult AddReferrerProjectTreatment(ReferrerProjectTreatment referrerProjectTreatment)
        {
            int ReferrerProjetTreatmentID = _referrerService.AddReferrerProjectTreatment(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatment>(referrerProjectTreatment));
            return Json(ReferrerProjetTreatmentID);
        }

        [HttpPost]
        public JsonResult AddProjectTreatmentSLA(IEnumerable<ProjectTreatmentSLA> projectTreatmentSLAs)
        {
            foreach (ProjectTreatmentSLA projectTreatmentSLA in projectTreatmentSLAs)
            {
                _referrerService.AddProjectTreatmentSLA(Mapper.Map<ITSService.ReferrerService.ProjectTreatmentSLA>(projectTreatmentSLA));
            }
            return Json(GlobalResource.AddedSuccessfully);
        }

        [HttpPost]
        public JsonResult AddReferrerProjectTreatmentPricing(IEnumerable<ReferrerProjectTreatmentPricing> referrerProjectTreatmentPricings)
        {
            foreach (ReferrerProjectTreatmentPricing referrerTreatmentPricing in referrerProjectTreatmentPricings)
            {
                _referrerService.AddReferrerProjectTreatmentPricing(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatmentPricing>(referrerTreatmentPricing));
            }
            return Json(referrerProjectTreatmentPricings);
        }

        [HttpPost]
        public JsonResult GetReferrerProjectByReferrerProjectID(int referrerProjectID)
        {
            ReferrerProject referrerProject = Mapper.Map<ReferrerProject>(_referrerService.GetReferrerProjectByProjectID(referrerProjectID));
            IEnumerable<TreatmentCategories> treatment = Mapper.Map<IEnumerable<TreatmentCategories>>(_referrerService.GetAllTreatmentCategory());
            referrerProject.TreatmentCategories = Mapper.Map<IEnumerable<ReferrerProjectTreatment>>(_referrerService.GetReferrerProjectTreatmentsByReferrerProjectID(referrerProjectID));
            foreach (TreatmentCategories item in treatment)
            {
                referrerProject.TreatmentCategories.Where(treatmentItem => treatmentItem.TreatmentCategoryID == item.TreatmentCategoryID).SingleOrDefault().TreatmentCategoryName = item.TreatmentCategoryName;
            }
            return Json(referrerProject);
        }

        [HttpPost]
        public JsonResult GetReferrerProjectsbyReferrerID(int referrerID)
        {
            IEnumerable<ReferrerProject> referrerprojects = Mapper.Map<IEnumerable<ReferrerProject>>(_referrerService.GetReferrerProjectsByReferrerID(referrerID));
            return Json(referrerprojects);
        }

        [HttpPost]
        public JsonResult UpdateReferrerProject(ReferrerProject referrerProject)
        {
            int result = _referrerService.UpdateReferrerProject(Mapper.Map<ITSService.ReferrerService.ReferrerProject>(referrerProject));
            
            foreach (ReferrerProjectTreatment referrerProjectTreatment in referrerProject.TreatmentCategories)
            {
                int result_ = _referrerService.UpdateReferrerProjectTreatments(Mapper.Map<ITSService.ReferrerService.ReferrerProjectTreatment>(referrerProjectTreatment));

            }
            _referrerService.UpdateProjectStatus(referrerProject.ReferrerProjectID, referrerProject.IsTriage);
            return Json(referrerProject.TreatmentCategories.Where(treatmeantCategory => treatmeantCategory.Enabled));

        }
        [HttpPost]
        public JsonResult GetAllTreatmentCategories()
        {
            return Json(Mapper.Map<IEnumerable<TreatmentCategories>>(_referrerService.GetAllTreatmentCategory()));
        }

    }
}
