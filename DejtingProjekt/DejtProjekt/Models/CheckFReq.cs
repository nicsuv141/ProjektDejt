using DejtProjekt.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Models
{
    public class CheckFReq
    {

        private OurDbContext db = new OurDbContext();

        public int checkFReq()
        {

        int id = LoggInController.GetUserId();





            int Request = (from user in db.userModel
                            join friend in db.Friend on user.UserID equals friend.UserId
                            where friend.RequestAccepted == false && friend.Fid == id select user).Count();


            if (Request>0) {
                return 1;
            }

            

                return 0;
            

            

        }
    }
}