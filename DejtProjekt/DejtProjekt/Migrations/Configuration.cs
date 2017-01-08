namespace DejtProjekt.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DejtProjekt.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DejtProjekt.Models.DateContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DateContext context)
        {
            var UserModels = new List<UserModel>
            {
                new Models.UserModel {
                    Username = "DonaldTrump",
                NewPassword = "trump1",
                ConfirmPassword = "trump1",
                FirstName = "Donald",
                LastName = "Trump",
                Email = "Trump@hotmail.com",
                Phone = "123456789",
                Country = "USA",
                Gender = false,
                Hidden = false,
                LookingFor = 1,
                PersonalNumber = "197004051321"
                 
                },           
                new Models.UserModel {
                Username = "VladimirPutin",
                NewPassword = "putin1",
                ConfirmPassword = "putin1",
                FirstName = "Vladimir",
                LastName = "Putin",
                Email = "Putin@hotmail.com",
                Phone = "987654321",
                Country = "Ryssland",
                Gender = false,
                Hidden = false,
                LookingFor = 3,
                PersonalNumber = "197004051321"
                  
                },
                new UserModel
                {
                Username = "StefanLofven",
                NewPassword = "lofven1",
                ConfirmPassword = "lofven1",
                FirstName = "Stefan",
                LastName = "Lofven",
                Email = "Lofven@hotmail.com",
                Phone = "555555555",
                Country = "Sverige",
                Gender = false,
                Hidden = false,
                LookingFor = 1,
                PersonalNumber = "197004051321"
                  
                },

           new UserModel
            {
                Username = "AngelaMerkel",
                NewPassword = "merkel1",
                ConfirmPassword = "merkel1",
                FirstName = "Angela",
                LastName = "Merkel",
                Email = "Merkel@hotmail.com",
                Phone = "333333333",
                Country = "Tyskland",
                Gender = false,
                Hidden = false,
                LookingFor = 2,
                PersonalNumber = "197004051321"
                  

            },

          new UserModel
            {
                Username = "Xxx",
                NewPassword = "xxxxxx1",
                ConfirmPassword = "xxxxxx1",
                FirstName = "Alex",
                LastName = "Jansson",
                Email = "Alex@gmail.com",
                Phone = "1111111111",
                Country = "Sweden",
                Gender = false,
                Hidden = false,
                LookingFor = 3,
                PersonalNumber = "199404051321"
                
          }
            };
            UserModels.ForEach(s => context.UserModels.AddOrUpdate(p => p.Username, s));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Models.Post {
                    Message ="I really like does bug nukes of yours",
                    AuthorId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID,
                     WallId = UserModels.Single(s=> s.Username =="VladimirPutin").UserID
                },
                 new Models.Post {
                    Message ="You should build a wall",
                    AuthorId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID,
                     WallId = UserModels.Single(s=> s.Username =="AngelaMerkel").UserID
                },
                  new Models.Post {
                    Message ="I like my high horse",
                    AuthorId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID,
                     WallId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID

                },
                   new Models.Post {
                    Message ="Good election you had",
                    AuthorId = UserModels.Single(s=> s.Username =="VladimirPutin").UserID,
                     WallId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID
                },
                    new Models.Post {
                    Message ="Thank you",
                    AuthorId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID,
                     WallId = UserModels.Single(s=> s.Username =="VladimirPutin").UserID
                },
                     new Models.Post {
                    Message ="Dont bomb us please",
                    AuthorId = UserModels.Single(s=> s.Username =="StefanLofven").UserID,
                     WallId = UserModels.Single(s=> s.Username =="VladimirPutin").UserID
                },
                      new Models.Post {
                    Message ="Nato, nato, nato",
                    AuthorId = UserModels.Single(s=> s.Username =="StefanLofven").UserID,
                     WallId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID
                }
            };

            var friends = new List<Friend>
            {
                new Models.Friend {


                    UserId = UserModels.Single(s=> s.Username =="DonaldTrump").UserID,
                   FId =  UserModels.Single(s=> s.Username =="VladimirPutin").UserID
                },

                new Models.Friend {


                    UserId = UserModels.Single(s=> s.Username =="VladimirPutin").UserID,
                    FId =  UserModels.Single(s=> s.Username =="StefanLofven").UserID
                },
                new Models.Friend {

                    UserId = UserModels.Single(s=> s.Username =="StefanLofven").UserID,
                    FId =  UserModels.Single(s=> s.Username =="DonaldTrump").UserID
                },
                new Models.Friend {

                    UserId = UserModels.Single(s=> s.Username =="AngelaMerkel").UserID,
                    FId =  UserModels.Single(s=> s.Username =="DonaldTrump").UserID
                },
                new Models.Friend {

                    
                    UserId = UserModels.Single(s=> s.Username =="AngelaMerkel").UserID,
                    FId =  UserModels.Single(s=> s.Username =="StefanLofven").UserID
                }

            };


            foreach (Post e in posts)
            {
                var postInDataBase = context.Posts.Where(
                    s =>
                         s.Author.UserID == e.AuthorId &&
                         s.Wall.UserID == e.WallId).SingleOrDefault();
                if (postInDataBase == null)
                {
                    context.Posts.Add(e);
                }
            }

            foreach (Friend e in friends)
            {
                var friendInDataBase = context.Friends.Where(
                    s =>
                         s.User.UserID == e.UserId &&
                          s.F.UserID == e.FId).SingleOrDefault();
                if (friendInDataBase == null)
               {
                   context.Friends.Add(e);
                }
            }
            

        context.SaveChanges();



        }


    }
}