using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Mvc;
using Web.Infrastructure.Managers;
using MvcApplication2.Domain;
using Castle.MicroKernel.Registration;

namespace Web
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Initialise()
        {
            var container = BuildWindsorContainer();

            // DependencyResolver.SetResolver(new UnityDependencyResolver(container));

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
            SetupDomain(container);
            RegisterTypes(container);

            return container;
        }

        public static void SetupDomain(IWindsorContainer container)
        {
            // maybe, we should make this configurable to setup.
            var domainLayer = new DomainLayer(container);
            // container.RegisterInstance(domainLayer);
        }

        public static void RegisterTypes(IWindsorContainer container)
        {
            // hier nur die innerhalb der Mvc App registrierten Typen registrieren.
            // Der Domain kümmert sich um sich selbst
            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .LifestyleTransient()
            );
        }
    }
}