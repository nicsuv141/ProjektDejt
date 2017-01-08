using System.Collections.Generic;
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
        private DateContext db = new DateContext();



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
            var users = from m in db.UserModels
                        select m;
            try
            {
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

                if (search == "")
                {
                    
                    return new HttpStatusCodeResult(404, "The search field can not be empty");
                }

                else if (!isEmptyFirst)
                {
                    users = users.Where(s => s.FirstName.Contains(search) && s.Hidden == false);
                    return View(users);
                }

                else if (!isEmptyLast)
                {
                    users = users.Where(s => s.LastName.Contains(search) && s.Hidden == false);
                    return View(users);
                }
                else if (!isEmptyUser)
                {
                    users = users.Where(s => s.Username.Contains(search) && s.Hidden == false);
                    return View(users);
                }

                else
                {

                    return new HttpStatusCodeResult(404, "Sorry we could not find anything");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpStatusCodeResult(404, "Something didn't go as expected, try again");

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
            UserModel userModel = db.UserModels.Include(s => s.Files).SingleOrDefault(s => s.UserID == id);
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
                    db.UserModels.Add(userModel);
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
            UserModel userModel = db.UserModels.Include(s => s.Files).SingleOrDefault(s => s.UserID == id);
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

        // POST: UserModels/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.UserModels.Find(id);
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
            UserModel userModel = db.UserModels.Find(id);
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
            UserModel userModel = db.UserModels.Find(id);
            try
            {
                db.UserModels.Remove(userModel);
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

            var friends = db.UserModels.Include(i => i.Friend);
            return View(friends.ToList());

            

        }

        public ActionResult showFriend(int id)
        {
            return RedirectToAction("Details/" + id, "UserModels");
        }

        [HttpGet]
        public ActionResult AddFriend(int friendId)
        {
            DateContext db = new DateContext();

            int Uid = LoggInController.GetUserId();

            var getFriendConnection = db.Friends.Where(u => u.FriendId == friendId && u.UserId == Uid).Select(u => u.UserId);
            var materializeFriend = getFriendConnection.ToList();
            bool isEmptyFriend = !materializeFriend.Any();

            try
            {
                if (Uid == friendId)
                {

                    return View("SelfFriend");
                }

                else if (isEmptyFriend)
                {

                    var userToUpdate = db.UserModels.Find(Uid);

                    var oneFriend = new Friend
                    {
                        UserId = Uid,
                        FId = friendId

                    };


                    userToUpdate.Friend = new List<Friend> { oneFriend };
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return View("");
                }

                else if (!isEmptyFriend)
                {

                    return new HttpStatusCodeResult(404, "You are already friends");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return new HttpStatusCodeResult(404, "Something didn't go as expected, try again");
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

