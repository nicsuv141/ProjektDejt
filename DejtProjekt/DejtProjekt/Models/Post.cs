using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DejtProjekt.Models
{
    public class Post
    {
        [Key]
        public int MessageId { get; set; }
        public string Message { get; set; }
        [ForeignKey("Author")]
        public virtual int AuthorId { get; set; }
        [ForeignKey("Wall")]
        public virtual int WallId { get; set; }

        public virtual UserModel Author { get; set; }

        public virtual UserModel Wall { get; set; }
      
    }
}