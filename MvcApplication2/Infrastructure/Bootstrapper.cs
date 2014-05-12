using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using MvcApplication2.Infrastructure.Managers;
using MvcApplication2.Domain;

namespace MvcApplication2
{
    internal class UnityRegistrar : IRegistrar
    {
        private readonly IUnityContainer _container;

        public UnityRegistrar(IUnityContainer container)
        {
            _container = container;
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }
    }

    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IManager, ContactManager>();
            SetupDomain(container);
            RegisterTypes(container);

            return container;
        }

        public static void SetupDomain(IUnityContainer container)
        {
            // maybe, we should make this configurable to setup.
            var domainLayer = new DomainLayer(new UnityRegistrar(container));
            container.RegisterInstance(domainLayer);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // hier nur die innerhalb der Mvc App registrierten Typen registrieren.
            // Der Domain kümmert sich um sich selbst
        }
    }
}