using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DejtProjekt.Models
{
    public class File
    {
        public int FileId { get; set; }
        [Required(ErrorMessage = "Måste fylla i ett namn")]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int UserId { get; set; }
        public virtual UserModel User { get; set; }
    }
}