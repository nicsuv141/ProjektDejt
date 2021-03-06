﻿using DejtProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData;



namespace DejtProjekt.API
{
    //[Authorize] 
    public class OurApiController : ApiController
    {
        private OurDbContext db = new OurDbContext();

        /* public IEnumerable<Post> GetAllPosts()
         {
             return posts;
         }

         public IHttpActionResult GetPost(int id)
         {
             var post = posts.FirstOrDefault((p) => p.MessageId == id);
             if (post == null)
             {
                 return NotFound();
             }
             return Ok(post);
         }
         */  

        // GET api/Posts/5    
        public IHttpActionResult GetPost(int id)
        {
            //var posts = from m in db.Posts
            //           select m;
            //var users = from m in db.userModel
            //            select m;
            //posts = posts.Where(s => s.WallId.Equals(id));
            //users = users.Where(s => s.UserID.Equals(posts.Select(a => a.AuthorId)));
            //var checkPostsList = posts.ToList();


            //var authorid = from AuthorID in checkPostsList
            //                 select new { AuthorID };

            //var checkusersList = users.ToList();
            var joinQuery = from user in db.userModel
                            join post in db.Posts on user.UserID equals post.AuthorId
                            where post.WallId == id
                         select new { post.Message, user.Username };




            //var posts = new List<Post>();
            //foreach (var u in joinQuery)
            //{
            //    posts.Add(new Post()
            //    {
            //        Message = u.Message,
            //        Username = u.Username,
            //        UserID = u.UserID,
            //        Username = u.Username
            //    });



            //    if (posts == null)
            //{
            //    return NotFound();
            //}
            return Ok(joinQuery);
        }



        // PUT api/Posts/5    
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (ModelState.IsValid && id == post.MessageId)
            {
                db.Entry(post).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST api/Posts   
        [HttpPost]
        public IHttpActionResult PostPost(Post post)
         {
             if (ModelState.IsValid)
             {
                 db.Posts.Add(post);
                 db.SaveChanges();
                 var uri = new Uri( Url.Link(                    
                     "DefaultApi",                   
                     new { id = post.MessageId }));
                 return Created(uri, post);
             }
             else
             {
                 return BadRequest(ModelState);
             }
         }

           



        // DELETE api/Posts/5    
        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            db.Posts.Remove(post);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return Ok(post);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        /*  IPostRepository repository;

       public OurApiController(IPostRepository repository)
        {
            this.repository = repository;
        }

        #region Get
        [EnableQuery]
        public IQueryable<Post> GetPosts()
        {
            return repository.Get().AsQueryable();
        }

        public Post GetPost(int id)
        {
            Post post;
            if (!repository.TryGet(id, out post))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return post;
        }
        #endregion

        #region POST 
        public HttpResponseMessage PostMessage(Post post)
        {
            post = repository.Add(post);
            var response = Request.CreateResponse<Post>(HttpStatusCode.Created, post);
            response.Headers.Location = new Uri(Request.RequestUri, "/api/comments/" + post.MessageId.ToString());
            return response;
        }
        #endregion

        #region DELETE 
        public Post DeleteComment(int id)
        {
            Post post;
            if (!repository.TryGet(id, out post))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            repository.Delete(id);
            return post;
        }
        #endregion

        #region Paging GET 
        public IEnumerable<Post> GetPost(int pageIndex, int pageSize)
        {
            return repository.Get().Skip(pageIndex * pageSize).Take(pageSize);
        }
        #endregion */

    }
}
