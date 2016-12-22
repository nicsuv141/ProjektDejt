using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejtProjekt.Models
{
    public class Authorize : AuthorizeAttribute
    { 

        public override void OnAuthorization(AuthorizationContext filterContext)
    {
        
        if (this.AuthorizeCore(filterContext.HttpContext))
        {
            base.OnAuthorization(filterContext);
    }
        else
        {
            // Otherwise redirect to your specific authorized area
            filterContext.Result = new RedirectResult("~/LoggIn/LogIn");
}
    }
}