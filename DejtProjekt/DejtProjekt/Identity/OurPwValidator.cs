using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DejtProjekt.Identity
{
    public class OurPwValidator : PasswordValidator
    {
            public OurPwValidator()
            {
                this.RequireLowercase = false;
                this.RequireUppercase = false;
                this.RequireDigit = false;
                this.RequireNonLetterOrDigit = false;
                this.RequiredLength = 2;
            }

            public override Task<IdentityResult> ValidateAsync(string password)
            {
                return base.ValidateAsync(password);
            }
        }
    }

