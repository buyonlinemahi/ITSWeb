using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using ITS.Infrastructure.Global;

namespace ITS.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizedUserCheckAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;

	    public AuthorizedUserCheckAttribute()
	    {
		    _authorize = true;
	    }

        public AuthorizedUserCheckAttribute(bool authorize)
	    {
		    _authorize = authorize;
	    }

	    protected override bool AuthorizeCore(HttpContextBase httpContext)
	    {
            if (_authorize)
                return httpContext.Session[GlobalConst.SessionKeys.USER] != null;


             return false;
	    }
    }
}
