using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DejtProjekt.Identity
{
    public class OurUserValidator : UserValidator<User>
    {
        public OurUserValidator(UserManager<User> mgr)
            : base(mgr)
        {
            this.RequireUniqueEmail = false;
            this.AllowOnlyAlphanumericUserNames = true;
        }

        public async override Task<IdentityResult> ValidateAsync(User user)
        {
            var result = await base.ValidateAsync(user);

            var errors = new List<string>();
            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors);
            }

            if (user.Age < 0)
            {
                errors.Add("Du måste fylla i en ålder");
            }

            if (errors.Any())
            {
                return new IdentityResult(errors);
            }

            return IdentityResult.Success;
        }
    }
}