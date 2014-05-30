using Castle.Windsor;
using Castle.Windsor.Mvc;
using Common.Logging;
using Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;
        public static ILog Log = LogManager.GetCurrentClassLogger();

        private static void BootstrapContainer()
        {
            Log.Info("initiating container");
            if (container == null)
                container = Bootstrapper.Initialize();

        }

        protected void Application_Start()
        {
            Log.Info("application start");
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();

        }

        protected void Application_End()
        {
            Log.Info("application end");
            container.Dispose();
        }
    }
}
