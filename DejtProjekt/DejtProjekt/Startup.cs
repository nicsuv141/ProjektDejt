﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DejtProjekt.Startup))]
namespace DejtProjekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
