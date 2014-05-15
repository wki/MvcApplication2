using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EventBus;
using System.Reflection;

namespace EventBus.Tests
{
    class GoodThingHappened    : IEvent {}
    class BadThingHappened     : IEvent { }
    class AnotherThingHappened : IEvent { }

    class ServiceBase
    {
        public int nrEventsHandled;

        public ServiceBase()
        {
            nrEventsHandled = 0;
        }
    }

    class AService : ServiceBase, ISubscribe<GoodThingHappened>
    {
        public AService()
        {
        }

        public void Handle(GoodThingHappened @event)
        {
            nrEventsHandled++;
        }
    }

    class BService : ServiceBase, ISubscribe<BadThingHappened>
    {
        public BService()
        {
        }

        public void Handle(BadThingHappened @event)
        {
            nrEventsHandled++;
        }
    }

    class CService : ServiceBase, ISubscribe<IEvent>
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
        private IUnityContainer container { get; set; }
        
        private void printRegistrations()
        {
            foreach (ContainerRegistration item in container.Registrations)
            {
                Console.WriteLine(item.GetMappingAsString());
            }
        }

        [TestInitialize]
        public void InitializeContainer()
        {
            container = new UnityContainer();
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
            container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
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
            container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
            container.RegisterInstance<ISubscribe<BadThingHappened>>("bService", bService);
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
            container.RegisterInstance<ISubscribe<GoodThingHappened>>("a1Service", a1Service);
            container.RegisterInstance<ISubscribe<GoodThingHappened>>("a2Service", a2Service);
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
            var aService = new AService();
            var cService = new CService();
            container.RegisterInstance<ISubscribe<GoodThingHappened>>("aService", aService);
            container.RegisterInstance<ISubscribe<IEvent>>("cService", cService);
            
            // var interfaces = new List<Type>(instanceType.GetInterfaces());

            var is_assignable = typeof(ISubscribe<IEvent>)
                .IsAssignableFrom(typeof(ISubscribe<BadThingHappened>));
            Console.WriteLine("ISubscribe<IEvent> isAssignableFrom ISubscribe<BadThingHappened>: " + is_assignable);
            
            is_assignable = typeof(IEvent).GetType()
                .IsAssignableFrom(typeof(BadThingHappened).GetType());
            Console.WriteLine("IEvent isAssignableFrom BadThingHappened: " + is_assignable);
            
            is_assignable = typeof(BadThingHappened).GetType()
                .IsAssignableFrom(typeof(IEvent).GetType());
            Console.WriteLine("BadThingHappened isAssignableFrom IEvent: " + is_assignable);

            Type instanceType = typeof(BadThingHappened); // typeof(ISubscribe<IEvent>);
            Console.WriteLine("Type: " + instanceType);
            instanceType
                // .FindInterfaces(new TypeFilter())
                .GetInterfaces()
                // .Where(x => x.IsGenericType)
                .ToList()
                .ForEach(x => Console.WriteLine("Interface Type: " + x));
                // .ForEach(x => container.RegisterInstance(x, cService, x.Name));

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
