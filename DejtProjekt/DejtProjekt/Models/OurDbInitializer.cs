using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class OurDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<OurDbContext>
    {
        protected override void Seed(OurDbContext context)
        {
            context.userModel.Add(new UserModel
            {
                Username = "DonaldTrump",
                Password = "trump",
                FirstName = "Donald",
                LastName = "Trump",
                Email = "Trump@hotmail.com",
                Phone = "123456789",
                Country = "USA"
            });

            context.userModel.Add(new UserModel
            {
                Username = "VladimirPutin",
                Password = "putin",
                FirstName = "Vladimir",
                LastName = "Putin",
                Email = "Putin@hotmail.com",
                Phone = "987654321",
                Country = "Ryssland"
            });

            context.userModel.Add(new UserModel
            {
                Username = "StefanLofven",
                Password = "lofven",
                FirstName = "Stefan",
                LastName = "Lofven",
                Email = "Lofven@hotmail.com",
                Phone = "555555555",
                Country = "Sverige"
            });
            base.Seed(context);

        }
    }
}