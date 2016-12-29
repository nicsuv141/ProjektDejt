using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejtProjekt.Models;
using System.Security.Claims;

namespace DejtProjekt.Controllers
{
    [AllowAnonymous]
    public class LoggInController : Controller
    {
        

        // GET: LoggIn
        //public ActionResult Index()
        //{
        //    using (OurDbContext db = new OurDbContext())
        //    {

        //        return View(db.userModel.ToList());

        //    }
        //}


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
            return View();
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

        //    [HttpGet]
        //    public ActionResult Login(string returnUrl)
        //    {
        //        var model = new UserModel
        //        {
        //            ReturnUrl = returnUrl
        //        };

        //        return View(model);
        //    }

        //    [HttpPost]
        //    public ActionResult LogIn(UserModel model)
        //    {
        //        if (!ModelState.IsValid) //Checks if input fields have the correct format
        //        {
        //            return View(model); //Returns the view with the input values so that the user doesn't have to retype again
        //        }

        //        //Checks whether the input is the same as those literals. Note: Never ever do this! This is just to demo the validation while we're not yet doing any database interaction
        //        if (model.Email == "admin@admin.com" & model.NewPassword == "123456")
        //    {
        //            var identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name, "Xtian"),
        //            new Claim(ClaimTypes.Email, "xtian@email.com"),
        //            new Claim(ClaimTypes.Country, "Philippines")
        //    }, 
        //        "ApplicationCookie");

        //        var ctx = Request.GetOwinContext();
        //        var authManager = ctx.Authentication;
        //        authManager.SignIn(identity);

        //        return Redirect(GetRedirectUrl(model.ReturnUrl));
        //    }

        //        ModelState.AddModelError("", "Invalid email or password");
        //        return View(model);
        //}

        //    private string GetRedirectUrl(string returnUrl)
        //    {
        //        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
        //        {
        //            return Url.Action("index", "home");
        //        }
        //        return returnUrl;
        //    }


        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}