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
            return messages.Values.OrderBy(message => message.MessageId);
        }

        public bool TryGet(int id, out Post message)
        {
            return messages.TryGetValue(id, out message);
        }

        public bool Update(Post message)
        {
            bool update = messages.ContainsKey(message.MessageId);
            messages[message.MessageId] = message;
            return update;
        }
    }
}