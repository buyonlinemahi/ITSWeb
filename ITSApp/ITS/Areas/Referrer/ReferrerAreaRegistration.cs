using System.Web.Mvc;

namespace ITSWebApp.Areas.Referrer
{
    public class ReferrerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Referrer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Referrer_default",
                "Portal/Referrer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "ITSWebApp.Areas.Referrer.Controllers" }

            );
        }
    }
}
