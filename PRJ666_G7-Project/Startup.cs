using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PRJ666_G7_Project.Startup))]

namespace PRJ666_G7_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
