using Castle.Windsor;
using Castle.Windsor.Mvc;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static void BootstrapContainer()
        {
            logger.Info("initiating container");
            if (container == null)
                container = Bootstrapper.Initialize();
        }

        protected void Application_Start()
        {
            logger.Info("application start");
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();

            var factory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        protected void Application_End()
        {
            logger.Info("application end");
            container.Dispose();
        }
    }
}
