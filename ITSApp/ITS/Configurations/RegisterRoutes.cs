using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Web.Routing;
using System.Web.Mvc;

namespace ITSWebApp.Configurations
{
    public class RegisterRoutes : ITask
    {
        private readonly RouteCollection routes;
        public RegisterRoutes()
        {
            routes = RouteTable.Routes;
        }
        public void Run()
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            #region User Detail
            routes.MapRoute("UserDetail",
                "User/Detail/{userID}",
                new { controller = "User", action = "Detail" });
            #endregion
            #region Referrer Project Treatment Detail
            routes.MapRoute("ReferrerProjectTreatmentDetail",
                "Referrer/ProjectTreatment/{referrerProjectTreatmentID}", // URL with parameters
                new { controller = "ReferrerProjectTreatment", action = "Index" }
            );
            #endregion
            #region Referrer Detail
            routes.MapRoute(
                "ReferrerDetail",
                "Referrer/Detail/{referrerID}", // URL with parameters
                new { controller = "Referrer", action = "Detail" }
            );
            #endregion
            #region Referrer Project
            routes.MapRoute(
                "ReferrerProject",
                "Referrer/Detail/{referrerID}/Project/{referrerProjectID}", // URL with parameters
                new { controller = "ReferrerProject", action = "Index" }
            );
            #endregion
            #region Supplier Detail
            routes.MapRoute(
                "SupplierDetail",
                "Supplier/Detail/{supplierID}", // URL with parameters
                new { controller = "Supplier", action = "Detail" }
            );
            #endregion
            #region Practitioner Detail
            routes.MapRoute(
                "PractitionerDetail",
                "Practitioner/Detail/{id}", // URL with parameters
                new { controller = "Practitioner", action = "Detail" }
            );
            #endregion
            #region Case Detail
            routes.MapRoute(
                      "CaseDetail",
                      "CaseSearch/Detail/{caseID}", // URL with parameters
                       new { controller = "CaseSearch", action = "Detail" }
                        );
            #endregion
            #region Internal Task
            routes.MapRoute(
               "InternalTask", // Route name
               "InternalTasks/{action}/{id}", // URL with parameters
               new { controller = "InternalTasks", action = "Index", id = UrlParameter.Optional }
           );
            #endregion
            #region Default case
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            #endregion
            routes.MapRoute(
              "User", // Route name
              "{controller}/{action}/{id}/{cnt}", // URL with parameters
              new { controller = "User", action = "ResetPassword", id = UrlParameter.Optional, cnt = UrlParameter.Optional }, // Parameter defaults
              new string[] { "ITS.Web.Controllers" }
          );
        }
    }
}