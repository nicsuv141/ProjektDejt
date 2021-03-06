﻿using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DejtProjekt.Models;
using System.Data.Entity.Infrastructure;
using System;

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

        [HttpGet]
        public ActionResult Index(string searchString)
        {
            var users = from m in db.userModel
                        select m;
            try
            {

                /*  var firstName = searchString.Split(' ')[0].Trim();
                var lastName = "0";
                int checkSubString = searchString.IndexOf(' ');


                    if (checkSubString != -1)
                    {
                        lastName = searchString.Substring(searchString.IndexOf(' ') + 1).Trim();
                    }
                    else if (checkSubString == -1)
                    {

                        users = users.Where(s => s.FirstName.Contains(firstName) && s.Hidden == false);

                    }
                    else if (checkSubString == -1)
                    {

                        users = users.Where(s => s.LastName.Contains(firstName) && s.Hidden == false);

                    }

                    if (!String.IsNullOrEmpty(searchString) && lastName == "0")
                    {

                        users = users.Where(s => s.FirstName.Contains(firstName) && s.Hidden == false);
                    }

                    if (!String.IsNullOrEmpty(searchString) && lastName == "0")
                    {

                        users = users.Where(s => s.FirstName.Contains(firstName) && s.Hidden == false);
                    }

                    if (!String.IsNullOrEmpty(searchString) && lastName != "0")
                    {

                        users = users.Where(s => s.FirstName.Contains(firstName) && s.LastName.Contains(lastName) && s.Hidden == false);
                    }

                } */
                var search = searchString.Split(' ')[0].Trim();
                
                var checkFirstName = users.Where(s => s.FirstName.Contains(search) && s.Hidden == false);

                var checkFirstNameList = checkFirstName.ToList();

                bool isEmptyFirst = !checkFirstNameList.Any();

                var checkLastName = users.Where(s => s.LastName.Contains(search) && s.Hidden == false);

                var checkLastNameList = checkLastName.ToList();

                bool isEmptyLast = !checkLastNameList.Any();

                var checkUserName = users.Where(s => s.Username.Contains(search) && s.Hidden == false);

                var checkUserNameList = checkUserName.ToList();

                bool isEmptyUser = !checkUserNameList.Any();

                if (!isEmptyFirst)
                {
                    users = users.Where(s => s.FirstName.Contains(search) && s.Hidden == false);
                }

                else if (!isEmptyLast)
                {
                    users = users.Where(s => s.LastName.Contains(search) && s.Hidden == false);
                }
                else if (!isEmptyUser)
                {
                    users = users.Where(s => s.Username.Contains(search) && s.Hidden == false);
                }

             
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(users);
        }


        //if ((users = users.Where(s => s.FirstName.StartsWith(firstName) && s.Hidden == false)) != null) { users = users.Where(s => s.FirstName.StartsWith(firstName) && s.Hidden == false }
        //           else {
        //               users = users.Where(s => s.FirstName.EndsWith(firstName) && s.Hidden == false);
        //           }
        //       }


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
            catch (Exception e)
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
            catch (Exception e)
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
                var loggedInUser = LoggInController.GetUserId();/*(HttpContext.Current.User.Identity.Name);*/
                if (id != loggedInUser)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }                
               else if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }


               else if (userModel == null)
                {
                    return HttpNotFound();
                }

            
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        [HttpPost, ActionName("Edit")]
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
            catch (Exception e)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        public ViewResult ShowFriends()
        {
            int id = LoggInController.GetUserId();

            var joinQuery = from user in db.userModel
                            join friend in db.Friend on user.UserID equals friend.UserId
                            where friend.RequestAccepted == true && friend.UserId == id
                            select new { user.FirstName,user.LastName,user.Username, user.UserID};

            var users = new List<UserModel>();
            foreach (var t in joinQuery)
            {
                users.Add(new UserModel()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Username = t.Username,
                    UserID = t.UserID,
                    });
            }
            return View(users);

        }

        public ActionResult showFriend(int id)
        {
            return RedirectToAction("Details/" + id, "UserModels");
        }

        public ActionResult showFriendRequest()
        {

            int id = LoggInController.GetUserId();
            var joinQuery = from user in db.userModel
                            join friend in db.Friend on user.UserID equals friend.UserId
                            where friend.RequestAccepted == false && friend.Fid == id
                            select new { user.FirstName, user.LastName, user.Username, user.UserID};

           

            var users = new List<UserModel>();
            foreach (var t in joinQuery)
            {
                users.Add(new UserModel()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Username = t.Username,
                    UserID = t.UserID,
                });
            }


            return View(users);
        }

        public ActionResult AcceptFriendRequest(int id)
        {


            var a = from f in db.Friend
                    where f.FriendId == id
                    select new { f.Fid };

            if (a.Equals(LoggInController.GetUserId()))
            {

                Friend friend = db.Friend.Find(id);
                friend.RequestAccepted = true;

                try
                {

                    db.Entry(friend).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            
            else{
                return new HttpStatusCodeResult(404, "Done");
            }


            return RedirectToAction("showFriendRequest");

        }

        public ActionResult DeniedFriendRequest(int id)
        {

            Friend friend = db.Friend.Find(id);
            try
            {
                db.Friend.Remove(friend);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return RedirectToAction("showFriendRequest");
        }

        [HttpGet]
        public string AddFriend(int friendId)
        {
            OurDbContext db = new OurDbContext();

            int Uid = LoggInController.GetUserId();

            var getFriendConnection = db.Friend.Where(u => u.Fid == friendId && u.UserId == Uid).Select(u => u.UserId);
            var materializeFriend = getFriendConnection.ToList();
            bool isEmptyFriend = !materializeFriend.Any();

            try
            {
                if (Uid == friendId)
                {
                    //return new HttpStatusCodeResult(@ViewBag.Message = "Du kan inte vara vän med sig själv");
                    //return (ViewBag.Message = "Du kan inte vara vän med dig själv");
                    //@ViewBag.Message = "Du kan inte vara vän med dig själv.";
                    return ViewBag.Friend = "Du kan inte vara vän med dig själv";
                }

                else if (isEmptyFriend)
                {

                    var userToUpdate = db.userModel.Find(Uid);

                    var oneFriend = new Friend
                    {
                        UserId = Uid,
                        Fid = friendId,
                        RequestAccepted = false
                        
                    };


                    userToUpdate.Friends.Add(oneFriend);
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return ViewBag.Friend = "Ni är nu vänner";
                    //return new HttpStatusCodeResult(404, "Done");
                }

                else if (!isEmptyFriend)
                {
                    return ViewBag.Friend = "Ni är redan vänner";
                    //return new HttpStatusCodeResult(404, "You are already friends");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return ViewBag.Friend = "Hejhej";
            //return View("~/Home");
            //return Redirect("~/Home");
            //return new HttpStatusCodeResult(404, "Hejsan");
            //return ViewBag.Message = "Hejsan hoppsan";
        }

        [HttpPost]
        public ActionResult checkFReq()
        {

            int id = LoggInController.GetUserId();

            int Request = (from user in db.userModel
                           join friend in db.Friend on user.UserID equals friend.UserId
                           where friend.RequestAccepted == false && friend.Fid == id
                           select user).Count();
            

            if (Request > 0)
            {
                return Json("Success");
            }

            return Json("An Error Has occoured");

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

