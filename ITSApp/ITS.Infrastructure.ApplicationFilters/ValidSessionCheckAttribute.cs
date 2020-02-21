using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using ITS.Infrastructure.Global;
using System.Web.Routing;
namespace ITS.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ValidSessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // If the browser session or authentication session has expired...
            if (ctx.Session[GlobalConst.SessionKeys.USER] == null && !ctx.Session.IsNewSession)
            {
                //ajax requests
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = "session expired"};
                }
                else
                {
                    //normal post/requests
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", GlobalConst.Controllers.Home },
                        { "Action", GlobalConst.Actions.HomeController.Index }
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
