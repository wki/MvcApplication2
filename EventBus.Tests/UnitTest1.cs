using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventBus;

namespace EventBus.Tests
{
    class GoodThingHappened    : IEvent {}
    class BadThingHappened     : IEvent { }
    class AnotherThingHappened : IEvent { }

    class AService : ISubscribe<GoodThingHappened>
    {
        public int nrEventsHandled;

        public AService()
        {
            nrEventsHandled = 0;
        }

        public void Handle(GoodThingHappened @event)
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
                //Console.WriteLine("Registered Type: {0} Name: {1} Mapped To: {2}", item.RegisteredType, item.Name, item.MappedToType);
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
            container.RegisterInstance<ISubscribe<GoodThingHappened>>(aService);
            printRegistrations();
            var bus = new EventBus(container);

            var eventHandlers = container.ResolveAll<ISubscribe<GoodThingHappened>>();
            Console.WriteLine(
                String.Format(@"{0} handlers", eventHandlers)
            );

            // act
            bus.Publish(new GoodThingHappened());

            // assert
            Assert.AreEqual(1, aService.nrEventsHandled, "subscribed event handled");
        }
    }
}
