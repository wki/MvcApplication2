using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Mvc;
using Web.Configuration;
using Web.Infrastructure.Managers;
using System.Web.Mvc;
using MvcApplication2.Domain;
using MvcApplication2.Domain.Measurement;
using System.Web.Http;
using Castle.MicroKernel;
using System.Web.Http.Dependencies;
using System;
using System.Collections.Generic;

namespace Web
{
    // see: http://cangencer.wordpress.com/2012/12/22/integrating-asp-net-web-api-with-castle-windsor/

    /*
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel container;
        private readonly IDisposable scope;

        public WindsorDependencyScope(IKernel container)
        {
            this.container = container;
            this.scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this.scope.Dispose();
        }
    }
    
    internal class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel container;

        public WindsorDependencyResolver(IKernel container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this.container);
        }

        public object GetService(Type serviceType)
        {
            return this.container.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose() { }

    }
    */

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

            return container;
        }

        public static void SetupInfrastructure(IWindsorContainer container)
        {
            // TODO: find a way to use a config:

            // init Logging Framework
            // init Storage
            // init Mail Sender
            var messageQConfig = MessageQConfiguration.Instance;
            var messageQ = new MessageQ.MessageQ(config: messageQConfig);


            // special: Repository might need config.
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

            /*
             DOES NOT WORK:
            
            container.Register(Classes
                .FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestyleScoped()
            );

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container.Kernel);
            */
        }
    }
}