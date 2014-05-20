using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Mvc;
using Web.Configuration;
using Web.Infrastructure.Managers;
using MvcApplication2.Domain;
using Castle.MicroKernel.Registration;

namespace Web
{
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

            return container;
        }

        public static void SetupInfrastructure(IWindsorContainer container)
        {
            // TODO: find a way to use a config:

            // init Logging Framework
            // init Storage
            // init Mail Sender
            var messageQConfig = MessageQConfiguration.Instance;
            var messageQ = new MessageQ.MessageQ() {
                HostName = messageQConfig.Host,
                Port = messageQConfig.Port,
                VirtualHost = messageQConfig.VirtualHost,
                UserName = messageQConfig.User,
                Password = messageQConfig.Password
            };


            // special: Repository might need config.
        }

        public static void SetupDomain(IWindsorContainer container)
        {
            // TODO: brauchen wir überhaupt einen Domain Layer als Objekt???
            var domainLayer = new DomainLayer(container);
            
            // Cross Cutting Concerns: Logging, Transaction
            // Register all Services
            // Register all Factories
            // Register all Repositories (maybe from a different assembly)
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