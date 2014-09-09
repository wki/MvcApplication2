using Common.Logging;
using Microsoft.Owin;
using System.Threading.Tasks;
using Owin;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.Use<WkComponent>("A Special Header for you");
            app.UseBasicAuthentication("API", ValidateUser);
        }

        private Task<IEnumerable<Claim>> ValidateUser(string id, string secret)
        {
            if (id == secret)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, id),
                    new Claim(ClaimTypes.Role, "Foo")
                };

                Log.Debug("Basic Authentication Succeeded");

                return Task.FromResult<IEnumerable<Claim>>(claims);
            }

            return Task.FromResult<IEnumerable<Claim>>(null);
        }
    }

    /// <summary>
    /// a stupid simple middleware adding a response header.
    /// </summary>
    public class WkComponent : OwinMiddleware
    {
        private string WkHeader { get; set; }

        public WkComponent(OwinMiddleware next, string header)
            : base(next)
        {
            WkHeader = header;
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Response.Headers["X-Modified-WK"] = WkHeader;

            return Next.Invoke(context);
        }
    }
}
