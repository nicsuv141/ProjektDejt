using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DejtProjekt.Models
{
    public class Post
    {
        [Key]
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int AuthorId { get; set; }
        public int WallId { get; set; }

        public virtual UserModel Author { get; set; }

        public virtual UserModel Wall { get; set; }


    }
}