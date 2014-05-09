﻿using Microsoft.Practices.Unity;
using MvcApplication2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcApplication2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //private static IWindsorContainer container;
        private static IUnityContainer _container;

        private static void BootstrapContainer()
        {
            // Windsor code:
            // container = new WindsorContainer().Install(FromAssembly.This());

            // var controllerFactory = new WindsorControllerFactory(container.Kernel);
            // ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // Unity code:
            if (_container == null)
                _container = Bootstrapper.Initialise();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}