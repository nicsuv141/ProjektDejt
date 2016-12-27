using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DejtProjekt.Identity
{
    public class OurClaimsIdentityFactory : ClaimsIdentityFactory<User>
    {
        public OurClaimsIdentityFactory()
        {
            base.UserIdClaimType = "subject";
            base.UserNameClaimType = "username";
            base.RoleClaimType = "role";
        }

        public async override Task<ClaimsIdentity> CreateAsync(UserManager<User, string> manager, User user, string authenticationType)
        {
            var id = await base.CreateAsync(manager, user, authenticationType);

            id.AddClaim(new Claim("first_name", user.First));
            id.AddClaim(new Claim("last_name", user.Last));
            id.AddClaim(new Claim("age", user.Age.ToString()));

            return id;
        }
    }
}