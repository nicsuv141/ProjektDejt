using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class PostRepository : IPostRepository
    {
        int nextID = 0;
        Dictionary<int, Post> messages = new Dictionary<int, Post>();

        public Post Add(Post message)
        {
            message.MessageId = nextID++;
            messages[message.MessageId] = message;
            return message;
        }

        public bool Delete(int id)
        {
            return messages.Remove(id);
        }

        public IEnumerable<Post> Get()
        {
            throw new NotImplementedException();
        }

        public bool TryGet(int id, out Post post)
        {
            throw new NotImplementedException();
        }

        public bool Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}