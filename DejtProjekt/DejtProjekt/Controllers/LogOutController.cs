using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejtProjekt.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut();

            return View();
        }
    }
}