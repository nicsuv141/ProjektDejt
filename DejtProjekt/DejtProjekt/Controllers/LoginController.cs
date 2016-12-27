using DejtProjekt.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}