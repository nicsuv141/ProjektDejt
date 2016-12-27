using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string First { get; set; }
        public string Last { get; set; }

        public int Age { get; set; }
        /* public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public char? Hidden { get; set; }

        public bool? Gender { get; set; }

        public string Phone { get; set; }

        public string Country { get; set; }

        public char? LookingFor { get; set; }

        public string PersonalNumber { get; set; } */

        // public virtual ICollection<File> Files { get; set; }

    }
}