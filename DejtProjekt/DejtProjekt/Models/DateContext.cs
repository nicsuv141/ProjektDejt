using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DejtProjekt.Models
{
    public class DateContext : DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Post> Posts { get; set; } 
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

           
        }
    }
}