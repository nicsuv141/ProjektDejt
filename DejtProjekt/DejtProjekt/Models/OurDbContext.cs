﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DejtProjekt.Models
{
    public class OurDbContext : DbContext
    {
        public DbSet<UserModel> userModel {get; set; }
        public DbSet<File> Files { get; set; }


    }
}