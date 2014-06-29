using Microsoft.Owin;
using System.Threading.Tasks;
using Owin;
using System.Collections;
using System;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app); // copied from AuthApp -- activate when ready.
            app.Use<WkComponent>(new string[] { "A Special Header for you" });
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
