using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Mvc;
using MvcApplication2.Domain;
using MvcApplication2.Domain.Measurement;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Web.Configuration;
using Web.Infrastructure.Managers;
using WebApiContrib.IoC.CastleWindsor;

namespace Web
{
    // see: http://cangencer.wordpress.com/2012/12/22/integrating-asp-net-web-api-with-castle-windsor/

    public static class Bootstrapper
    {
        public static IWindsorContainer Initialize()
        {
            var container = BuildWindsorContainer();

            return container;
        }

        private static IWindsorContainer BuildWindsorContainer()
        {
            var container = new WindsorContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            // container.RegisterType<IManager, ContactManager>();
            container.Register(
                Component.For<IManager>().ImplementedBy<ContactManager>()
            );
            SetupInfrastructure(container);
            SetupDomain(container);
            RegisterTypes(container);

            RegisterControllers(container);
            RegisterWebApi(container);

            return container;
        }

        public static void SetupInfrastructure(IWindsorContainer container)
        {
            // TODO: find a way to use a config:

            // init Logging Framework -- happens automatically based on config
            // init Storage
            // init Mail Sender
            var messageQConfig = MessageQConfiguration.Instance;
            var messageQ = new MessageQ.MessageQ(config: messageQConfig);


            // special: Repository might need config (db connect string)
        }

        public static void SetupDomain(IWindsorContainer container)
        {
            // TODO: brauchen wir überhaupt einen Domain Layer als Objekt???
            var domainLayer = new DomainLayer(container);
            
            // Cross Cutting Concerns: Logging, Transaction
            // Register all Services
            container.Register(Classes
                //.FromAssemblyInThisApplication()
                .FromAssemblyContaining<ICollectService>()
                .BasedOn<ICollectService>()  // TODO: IService would be nice.
                .WithServiceDefaultInterfaces()
            );

            // Register all Factories
            // Register all Repositories (maybe from a different assembly)
        }

        public static void RegisterTypes(IWindsorContainer container)
        {
            // hier nur die innerhalb der Mvc App registrierten Typen registrieren.
            // Der Domain kümmert sich um sich selbst
            container.Register(Classes
                .FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient()
            );
        }

        public static void RegisterControllers(IWindsorContainer container)
        {
            var factory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        public static void RegisterWebApi(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorResolver(container);

            container.Register(Classes
                .FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest() // we must ensure to call a service only once which is a usual case.
                // .LifestyleScoped()  // does not work. dunno why
            );
        }
    }
}