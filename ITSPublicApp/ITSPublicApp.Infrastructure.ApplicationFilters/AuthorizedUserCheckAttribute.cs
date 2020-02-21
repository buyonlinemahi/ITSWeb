using ITSPublicApp.Infrastructure.Global;
using System;
using System.Web;
using System.Web.Mvc;

namespace ITSPublicApp.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizedUserCheckAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;
        private readonly string _userType;

        public AuthorizedUserCheckAttribute()
        {
            _authorize = true;
        }

        public AuthorizedUserCheckAttribute(string userType)
        {
            _authorize = true;
            _userType = userType;
        }

        public AuthorizedUserCheckAttribute(bool authorize)
        {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var UserSession = (ITSPublicApp.Domain.Models.ITSUser)httpContext.Session[GlobalConst.SessionKeys.USER];

            if (_authorize)
            {
                
                if (_userType == "Supplier")
                    return (UserSession != null && (UserSession.UserTypeID == 3));
                
                if (_userType == "Referrer")
                    return (UserSession != null && (UserSession.UserTypeID == 2));
                
                if (string.IsNullOrEmpty(_userType))
                    return httpContext.Session[GlobalConst.SessionKeys.USER] != null;

            }

            return false;
        }
    }
}
