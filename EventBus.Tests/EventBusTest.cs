using Castle.Windsor;
using System;
// using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EventBus;
using System.Reflection;
using Castle.MicroKernel.Registration;

namespace EventBus.Tests
{
    public class GoodThingHappened    : IEvent {}
    public class BadThingHappened     : IEvent { }
    public class AnotherThingHappened : IEvent { }

    public class ServiceBase
    {
        public int nrEventsHandled;

        public ServiceBase()
        {
            nrEventsHandled = 0;
        }
    }

    interface IAService {}
    interface IBService {}
    interface ICService {}

    public class AService : ServiceBase, IAService, ISubscribe<GoodThingHappened>
    {
        public AService()
        {
        }

        public void Handle(GoodThingHappened @event)
        {
            nrEventsHandled++;
        }
    }

    public class BService : ServiceBase, IBService, ISubscribe<BadThingHappened>
    {
        public BService()
        {
        }

        public void Handle(BadThingHappened @event)
        {
            nrEventsHandled++;
        }
    }

    public class CService : ServiceBase, ICService, ISubscribe<IEvent>
    {
        public CService()
        {
        }

        public void Handle(IEvent @event)
        {
            nrEventsHandled++;
        }
    }

    
    [TestClass]
    public class EventBusTest
    {
        // private IUnityContainer container { get; set; }
        private IWindsorContainer container { get; set; } 
        
        private void printRegistrations()
        {
            // Unity only:
            //foreach (ContainerRegistration item in container.Registrations)
            //{
            //    Console.WriteLine(item.GetMappingAsString());
            //}

            // Windsor only:
            Console.WriteLine("Registrations:");
            foreach (var handler in container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                foreach (var service_name in handler.ComponentModel.Services)
                {
                    Console.WriteLine("Service: {0} Name: {1}, Implemented By {2}",
                        service_name,
                        handler.ComponentModel.Name,
                        handler.ComponentModel.Implementation);
                }
            }
        }

        [TestInitialize]
        public void InitializeContainer()
        {
            container = new WindsorContainer();
        }

        // HELP! do not know how to check "uninitialized".
        // [TestMethod]
        // public void EventBus_Uniinitialized()
        // {
        //     Assert.IsNull(EventBus.Current, "Current unset");
        // }

        [TestMethod]
        public void EventBus_Instantiation()
        {
            // arrange
            var bus = new EventBus(container);

            // assert
            Assert.AreEqual(EventBus.Current, bus, "Current reflects new bus");
        }

        [TestMethod]
        public void EventBus_Publish_Unsubscribed_Event()
        {
            // arrange
            var bus = new EventBus(container);

            // act
            bus.Publish(new GoodThingHappened());
            
            // assert
            Assert.IsTrue(true, "Publishing an unsubscribed Event");
        }

        [TestMethod]
        public void EventBus_Publish_Subscribed_Event()
        {
            // arrange
            var aService = new AService();
            // container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
            container.Register(
                Component.For<ISubscribe<GoodThingHappened>>().Instance(aService)
            );
            printRegistrations();
            var bus = new EventBus(container);

            // act
            bus.Publish(new GoodThingHappened());

            // assert
            Assert.AreEqual(1, aService.nrEventsHandled, "subscribed event handled");
        }

        [TestMethod]
        public void EventBus_Publish_Multiple_Subscribed_Events()
        {
            // arrange
            var aService = new AService();
            var bService = new BService();
            //container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
            //container.RegisterInstance<ISubscribe<BadThingHappened>>("bService", bService);
            container.Register(
                Component.For<ISubscribe<GoodThingHappened>>().Instance(aService),
                Component.For<ISubscribe<BadThingHappened>>().Instance(bService)
            );
            
            printRegistrations();
            var bus = new EventBus(container);

            // act
            bus.Publish(new GoodThingHappened());
            bus.Publish(new BadThingHappened());
            bus.Publish(new BadThingHappened());

            // assert
            Assert.AreEqual(1, aService.nrEventsHandled, "subscribed event A handled");
            Assert.AreEqual(2, bService.nrEventsHandled, "subscribed event B handled");
        }

        [TestMethod]
        public void EventBus_Publish_Multiply_Subscribed_Event()
        {
            // arrange
            var a1Service = new AService();
            var a2Service = new AService();
            //container.RegisterInstance<ISubscribe<GoodThingHappened>>("a1Service", a1Service);
            //container.RegisterInstance<ISubscribe<GoodThingHappened>>("a2Service", a2Service);
            container.Register(
                Component.For<ISubscribe<GoodThingHappened>>().Named("a1").Instance(a1Service),
                Component.For<ISubscribe<GoodThingHappened>>().Named("a2").Instance(a2Service)
            );
            printRegistrations();
            var bus = new EventBus(container);

            // act
            bus.Publish(new GoodThingHappened());
            bus.Publish(new GoodThingHappened());
            bus.Publish(new BadThingHappened());

            // assert
            Assert.AreEqual(2, a1Service.nrEventsHandled, "subscribed event handled by A1");
            Assert.AreEqual(2, a2Service.nrEventsHandled, "subscribed event handled by A2");
        }

        [TestMethod]
        public void EventBus_Publish_Generic_Subscribed_Event()
        {
            // arrange
            //var aService = new AService();
            //var cService = new CService();
            //container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
            //container.RegisterInstance<ISubscribe<IEvent>>("cService", cService);
            container.Register(
                //Component.For<ISubscribe<GoodThingHappened>>().Instance(aService),
                //Component.For<ISubscribe<IEvent>>().Instance(cService),
                Classes.FromThisAssembly()
                    // .FromAssemblyContaining<EventBusTest>()
                    .BasedOn<ServiceBase>()
                    .WithService.AllInterfaces()
                    // .WithService.Base()
                    // .WithService.Self()
                    .Configure(component => component.Named(component.Implementation.FullName + "XYZ"))
                    .LifestyleSingleton()
                    // .WithService.AllInterfaces()
                    //.LifeStyle.Singleton
            );

            AService aService = (AService) container.Resolve<IAService>();
            CService cService = (CService) container.Resolve<ICService>();
            
            // var interfaces = new List<Type>(instanceType.GetInterfaces());

            //var is_assignable = typeof(ISubscribe<IEvent>)
            //    .IsAssignableFrom(typeof(ISubscribe<BadThingHappened>));
            //Console.WriteLine("ISubscribe<IEvent> isAssignableFrom ISubscribe<BadThingHappened>: " + is_assignable);

            //is_assignable = typeof(ISubscribe<BadThingHappened>)
            //    .IsAssignableFrom(typeof(ISubscribe<IEvent>));
            //Console.WriteLine("ISubscribe<BadThingHappened> isAssignableFrom ISubscribe<IEvent>: " + is_assignable);
            
            //is_assignable = typeof(IEvent).GetType()
            //    .IsAssignableFrom(typeof(BadThingHappened).GetType());
            //Console.WriteLine("IEvent isAssignableFrom BadThingHappened: " + is_assignable);
            
            //is_assignable = typeof(BadThingHappened).GetType()
            //    .IsAssignableFrom(typeof(IEvent).GetType());
            //Console.WriteLine("BadThingHappened isAssignableFrom IEvent: " + is_assignable);

            //Type instanceType = typeof(BadThingHappened); // typeof(ISubscribe<IEvent>);
            //Console.WriteLine("Type: " + instanceType);
            //instanceType
            //    // .FindInterfaces(new TypeFilter())
            //    .GetInterfaces()
            //    // .Where(x => x.IsGenericType)
            //    .ToList()
            //    .ForEach(x => Console.WriteLine("Interface Type: " + x));
            //    // .ForEach(x => container.RegisterInstance(x, cService, x.Name));

            printRegistrations();
            var bus = new EventBus(container);

            // act
            bus.Publish(new GoodThingHappened()); // caucht by a+c
            bus.Publish(new BadThingHappened());  // caught by c
            bus.Publish(new BadThingHappened());  // caught by c

            // assert
            Assert.AreEqual(1, aService.nrEventsHandled, "subscribed event A handled");
            Assert.AreEqual(3, cService.nrEventsHandled, "subscribed event C handled");
        }
    }
}
