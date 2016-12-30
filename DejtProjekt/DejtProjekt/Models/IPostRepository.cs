using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejtProjekt.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get();
        bool TryGet(int id, out Post message);
        Post Add(Post message);
        bool Delete(int id);
        bool Update(Post message);
    }
}
