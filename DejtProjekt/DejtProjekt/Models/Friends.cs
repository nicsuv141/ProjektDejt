using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class Friends
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public virtual UserModel User { get; set; }
        public virtual UserModel Friend { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}