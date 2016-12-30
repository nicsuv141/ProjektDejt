using DejtProjekt.Models;
using System;
using System.Collections.Generic;
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
        Post[] posts = new Post[]
        {
            new Post { MessageId = 1, Message = "Tomato Soup", AuthorId = 2, WallId = 3 },
            new Post { MessageId = 2, Message = "Yo-yo", AuthorId = 1, WallId = 2 },
            new Post { MessageId = 3, Message = "Hammer", AuthorId = 1, WallId = 1 }
        }

        public IEnumerable<Post> GetAllProducts()
        {
            return posts;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var post = posts.FirstOrDefault((p) => p.MessageId == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
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
