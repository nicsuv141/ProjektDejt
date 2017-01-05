using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejtProjekt.Models;
using System.Security.Claims;
using System.Net;
using System.Data.Entity;

namespace DejtProjekt.Controllers
{
    [AllowAnonymous]
    public class LoggInController : Controller
    {




        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel account, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        account.Files = new List<File> { avatar };
                    }
                    using (OurDbContext db = new OurDbContext())
                    {
                        db.userModel.Add(account);
                        db.SaveChanges();
                    }
                    ModelState.Clear();
                    ViewBag.Message = account.FirstName + " " + account.LastName + "Succsess";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Login");
        }

        //Log
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(UserModel user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {
                    var usr = db.userModel.Where(u => u.Username == user.Username && u.NewPassword == user.NewPassword).FirstOrDefault();
                    if (usr != null)
                    {
                        Session["UserID"] = usr.UserID.ToString();
                        Session["Username"] = usr.Username.ToString();
                        var getUserName = db.userModel.Where(u => u.Username == user.Username).Select(u => u.Username);
                        var materializeUsername = getUserName.ToList();
                        var username = materializeUsername[0];
                        var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username),
                },
                        "ApplicationCookie");

                        var ctx = Request.GetOwinContext();
                        var accountManager = ctx.Authentication;

                        accountManager.SignIn(identity);
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or Password is wrong");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var accountManager = ctx.Authentication;

            accountManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoggedIn()
        {

            if (GetUserId() != 0) {

                int ID = GetUserId();

                return RedirectToAction("Details/" + ID, "UserModels");

            }
            else
            {
                return RedirectToAction("Login");
            }


        }

        public static int GetUserId() {
            using (var db = new OurDbContext())
            {
                Claim sessionName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name);
                string userName = sessionName.Value;
                if (userName != null)
                {
                    var getID = db.userModel.Where(u => u.Username == userName).Select(u => u.UserID);
                    var materializeID = getID.ToList();
                    var ID = materializeID[0];
                    return ID;
                }
            }
            return 0;

        }

       


    }


    /*public ActionResult LoggedIn()
    {

        string userId = Session["UserID"].ToString();
        if (Session["UserID"] != null)
        { 
            return RedirectToAction("Details/"+userId, "UserModels");

        }
        else
        {
            return RedirectToAction("Login");
        }

    } */

    /*   [HttpGet]
         public ActionResult Login(string returnUrl)
         {
             var model = new UserModel
             {
                 ReturnUrl = returnUrl
             };

             return View(model);
         } */



}

