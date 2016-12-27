using DejtProjekt.Identity;
using DejtProjekt.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejtProjekt.Controllers
{
    public class RegisterController : Controller
    {
        OurUserManager mgr = new OurUserManager();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            
                var user = new User
            {
                    UserName = model.Username,
                    First = model.First,
                    Last = model.Last,
                    Age = model.Age,

                };
            /* if (upload != null && upload.ContentLength > 0)
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
                model.Files = new List<File> { avatar };
            }
            */

            var result = mgr.Create(user, model.Password);
            if (result.Succeeded)
            {
                return View("Success");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View();
        }
    }
}


