using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DejtProjekt.Models
{
    [Bind(Include = "Username, Password,FirstName,LastName,Email,Hidden, Image, Gender,Phone,Country,LookingFor")]
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }  // Primary key
        [Required(ErrorMessage = "Skriv in ett användarnamn.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Skriv in ett lösenord.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Skriv in ditt förnamn.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Skriv in ditt efternamn")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Skriv in en mejladress.")]
        public string Email { get; set; }
        //public string UserName { get; set; }
        public char? Hidden { get; set; }
        public byte?[] Image { get; set; }
        public FileType FileType { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public char? LookingFor { get; set; }
        //public int? PostId { get; set; }  // Foreign key 
        //public int? FriendId { get; set; } // Foreign entity

        public virtual ICollection<File> Files { get; set; }
    }
}