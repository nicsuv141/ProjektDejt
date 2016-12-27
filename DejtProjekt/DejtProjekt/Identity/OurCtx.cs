using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejtProjekt.Identity
{
    public class OurCtx  : IdentityDbContext<User> 
    
        {
            public OurCtx(string connectionString)
                : base(connectionString)
            {
            }
        } 
    }
