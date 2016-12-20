using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    //[Bind(Include = "Username, Password,FirstName,LastName,Email,Hidden, Image, Gender,Phone,Country,LookingFor")]
    public class UserModel
    {
        public int UserId { get; set; }  // Primary key
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public char? Hidden { get; set; }
        public byte?[] Image { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public char? LookingFor { get; set; }
        //public int? PostId { get; set; }  // Foreign key 
        //public int? FriendId { get; set; } // Foreign entity
    }
}