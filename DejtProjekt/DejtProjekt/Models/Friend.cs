using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class Friend
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public int Fid { get; set; }
        public bool RequestAccepted { get; set; }

        public virtual UserModel User { get; set; }
        public virtual UserModel TheFriend { get; set; }

       
    }
}