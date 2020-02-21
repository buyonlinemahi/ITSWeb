using System.Web.Mvc;
using ITS.Infrastructure.Base;


/*  
 Page Name:  PortalController.cs                         
  Version:  1.2                                                
  Purpose:  Add Action(CaseStoppedPartial,ReferralTasksDueTodayPartial) to generate Their Partial views 
  Revision History:                                          
                                                             
    1.0 – 10/27/2012 Robin Singh    

 * Edited By : Robin Singh  
 * Date : 29-Oct-2012  
 * Version : 1.1  
 * Description : Add Action(InitialAssessmentPartial,AuthorisationPartial, AuthorisationPartial,ReviewAssessmentPartial,FinalAssessmentPartial) to create  Partial views  
 * 
 * Edited By : Robin Singh  
 * Date : 31-Oct-2012  
 * Version : 1.2 
 * Description : Add Action(CaseStoppedPartial,ReferralTasksDueTodayPartial) to generate Their Partial views 
 */

namespace ITSWebApp.Areas.Internal.Controllers
{
    /// <summary>
    /// Everything will be a partial view except for the index.
    /// </summary>
    public class CasesSearchController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ReferralsPartial()
        {
            return PartialView("MainScreen/_ReferralsPartial");
        }
        public PartialViewResult InitialAssessmentPartial()
        {
            return PartialView("MainScreen/_InitialAssessmentPartial");
        }
        public PartialViewResult AuthorisationPartial()
        {
            return PartialView("MainScreen/_AuthorisationPartial");
        }
        public PartialViewResult ReviewAssessmentPartial()
        {
            return PartialView("MainScreen/_ReviewAssessmentPartial");
        }
        public PartialViewResult FinalAssessmentPartial()
        {
            return PartialView("MainScreen/_FinalAssessmentPartial");
        }
        public PartialViewResult CaseStoppedPartial()
        {
            return PartialView("MainScreen/_CaseStoppedPartial");
        }
        public PartialViewResult ReferralTasksDueTodayPartial()
        {
            return PartialView("MainScreen/_ReferralTasksDueTodayPartial");
        }

        
    }
}
