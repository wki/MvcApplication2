using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthApp.Startup))]
namespace AuthApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
