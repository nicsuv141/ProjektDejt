using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DejtProjekt.Models
{
    
    public class UserM
    {

        public string UserId { get; set; }

        public string First { get; set; }
        public string Last { get; set; }

        public int Age { get; set; }





    }
}
