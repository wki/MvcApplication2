using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Auth4.Startup))]
namespace Auth4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
