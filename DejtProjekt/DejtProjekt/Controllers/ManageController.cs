using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DejtProjekt.Models;
using System.Security.Claims;
using DejtProjekt.Identity;

namespace DejtProjekt.Controllers
{

    public class ManageController : Controller
    {
        OurUserManager mgr = new OurUserManager();

        public ActionResult Index(string filter = null)
        {
            var users =
                from u in mgr.Users
                select u;

            if (filter != null)
            {
                users =
                    from u in users
                    where u.UserName.Contains(filter)
                    select u;
            }

            ViewData["filter"] = filter;
            ViewBag.Users = users.ToArray();

            return View("Index");
        }

        public ActionResult Edit(string id)
        {
            var user = mgr.FindById(id);
            if (user == null)
            {
                return Redirect("~/Manage");
            }

            return View("Edit", user);
        }

        [HttpPost]
        public ActionResult Delete(string userid)
        {
            var user = mgr.FindById(userid);
            if (user != null)
            {
                var result = mgr.Delete(user);
                if (result.Succeeded)
                {
                    ViewData["message"] = "Delete Successful";
                    return Index();
                }
                ModelState.AddModelError("", result.Errors.First());
            }
            else
            {
                ModelState.AddModelError("", "Invalid User ID");
            }

            return Index();
        }

        [HttpPost]
        public ActionResult Update(UserM model)
        {
            var user = mgr.FindById(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid User ID");
                return Index();
            }


            user.First = model.First;
            user.Last = model.Last;
            user.Age = model.Age;





            var result = mgr.Update(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            else
            {
                ViewData["message"] = "Update Successful";
            }

            return Edit(model.UserId);
        }

        [HttpPost]
        public ActionResult ChangePassword(string userid, string oldpassword, string newpassword)
        {
            var result = mgr.ChangePassword(userid, oldpassword, newpassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            else
            {
                ViewData["message"] = "Password Changed";
            }

            return Edit(userid);
        }

    }
}