using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DejtProjekt.Models;
using System.Data.Entity.Infrastructure;



namespace DejtProjekt.Controllers
{
    public class UserModelsController : Controller
    {
        private OurDbContext db = new OurDbContext();



        //public ActionResult Index(string searchString)
        //{
        //    string firstName = searchString.Split(' ')[0].Trim();
        //    string lastName = searchString.Substring(searchString.IndexOf(' ') + 1);

        //    var users = from m in db.userModel
        //                select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        users = users.Where(s => s.FirstName.Contains(firstName) && lastName.Contains(lastName));
        //    }

        //    return View(users);

        //}

        
        public ActionResult Index(string searchString)
        {
            var users = from m in db.userModel
                        select m;
            try
            {
                

                if (!String.IsNullOrEmpty(searchString))
                {
                    var firstName = searchString.Split(' ')[0].Trim();
                    var lastName = searchString.Substring(searchString.IndexOf(' ') + 1).Trim();
                    users = users.Where(s => s.FirstName.Contains(firstName) && lastName.Contains(lastName));
                }
                

            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(users);
        }
    




        // GET: UserModels/Details/5
        //[@Authorize]
        public ActionResult Details(int? id)
        {
            UserModel userModel = db.userModel.Include(s => s.Files).SingleOrDefault(s => s.UserID == id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                

                if (userModel == null)
                {
                    return HttpNotFound();
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View(userModel);
        }

        // GET: UserModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.userModel.Add(userModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return View(userModel);
        }

        // GET: UserModels/Edit/5
        public ActionResult Edit(int? id)
        {
            UserModel userModel = db.userModel.Include(s => s.Files).SingleOrDefault(s => s.UserID == id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                

                if (userModel == null)
                {
                    return HttpNotFound();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.userModel.Find(id);
            if (TryUpdateModel(userToUpdate, "",
                new string[] { "Username", "FirstName", "LastName", "Email", "Gender", "Phone", "Country", "LookingFor", "Hidden", "PersonalNumber" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (userToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(userToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
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
                        userToUpdate.Files = new List<File> { avatar };
                    }
                    db.Entry(userToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Kan inte uppdatera just nu, kontakta admin om problemet kvarstår");
                }
            }
            return View(userToUpdate);
        }

        // GET: UserModels/Delete/5
        public ActionResult Delete(int? id)
        {
            UserModel userModel = db.userModel.Find(id);
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //UserModel userModel = db.userModel.Find(id);
                if (userModel == null)
                {
                    return HttpNotFound();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.userModel.Find(id);
            try
            {
                db.userModel.Remove(userModel);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
