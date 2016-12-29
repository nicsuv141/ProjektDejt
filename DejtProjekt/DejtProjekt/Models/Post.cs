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
        
        public int MessageId { get; set; }
        public string Message { get; set; }

        
        public virtual UserModel Wall { get; set; }
    }
}