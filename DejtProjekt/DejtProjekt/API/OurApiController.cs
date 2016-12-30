using DejtProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;


namespace DejtProjekt.API
{
    //[Authorize] 
    public class OurApiController : ApiController
    {
        IPostRepository repository;

        public OurApiController(IPostRepository repository)
        {
            this.repository = repository;
        }

        #region Get
        [Queryable]
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
        #endregion
        
    }
}
