//using DejtProjekt.Identity;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace DejtProjekt.Controllers
//{
//    public class LoginController : Controller
//    {
//        OurUserManager mgr = new OurUserManager();

//        public ActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Index(string username, string password)
//        {
//            var user = mgr.FindByName(username);
//            if (user != null)
//            {
//                if (mgr.CheckPassword(user, password))
//                {
//                    var ci = mgr.CreateIdentity(user, "Cookie");

//                    var ctx = Request.GetOwinContext();
//                    ctx.Authentication.SignIn(ci);

//                    return Redirect("~/Home");
//                }
//            }


//            ModelState.AddModelError("", "Invalid username or password");
//            return View();
//        }
//    }
//}

using DejtProjekt.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DejtProjekt.Controllers
{
    public class LoginController : Controller
    {
        OurUserManager mgr = new OurUserManager();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var user = mgr.FindByName(username);
            if (user != null)
            {
                if (mgr.CheckPassword(user, password))
                {
                    var ci = mgr.CreateIdentity(user, "Cookie");

                    var ctx = Request.GetOwinContext();
                    ctx.Authentication.SignIn(ci);

                    return Redirect("~/Home");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        #region external
        public void External(string provider)
        {
            Request.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action("Callback")
            }, provider);
        }

        public async Task<ActionResult> Callback()
        {
            var ctx = Request.GetOwinContext();
            var result = await ctx.Authentication.AuthenticateAsync("External");
            if (result == null) return Redirect("~/Home");

            var idClaim = result.Identity.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            var provider = idClaim.Issuer;
            var providerId = idClaim.Value;

            var user = mgr.Find(new UserLoginInfo(provider, providerId));
            if (user == null)
            {
                // rather than this, build a UI to prompt user for name, etc...
                user = new User
                {
                    UserName = result.Identity.Name
                };
                mgr.Create(user);
            }

            var id = mgr.CreateIdentity(user, "Cookie");
            ctx.Authentication.SignIn(id);
            ctx.Authentication.SignOut("External");
            return Redirect("~/Home");
        }
    }
    #endregion

}