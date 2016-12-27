using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Identity
{
    public class OurUserManager : UserManager<User>
        
    {
        public OurUserManager()
            : base(new UserStore<User>(new OurCtx("B")))
        {
            this.UserValidator = new OurUserValidator(this);
            this.PasswordValidator = new OurPwValidator();
            this.ClaimsIdentityFactory = new OurClaimsIdentityFactory();
        } 
    }
}