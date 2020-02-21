using System.Web.Mvc;

namespace ITSWebApp.Areas.Supplier
{
    public class SupplierAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Supplier";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Supplier_default",
                "Portal/Supplier/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
