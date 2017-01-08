using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class Friend
    {
        [Key]
        public int FriendId { get; set; }


        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        [ForeignKey("F")]
        public virtual int FId { get; set; }

        public virtual UserModel User { get; set; }
        public virtual UserModel F { get; set; }

    }
}