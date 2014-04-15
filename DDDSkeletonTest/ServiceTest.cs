using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDDSkeleton.Infrastructure.Common.Domain;
using DDDSkeleton.Infrastructure.Common.DomainEvents;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace DDDSkeletonTest
{
    public class SomethingHappened : DomainEvent {}
    public class ErrorOccured : DomainEvent {}

    public class SampleService 
        : IService, IHandle<SomethingHappened>
    {
        public int NrEvents = 0;
        
        public void Handle(SomethingHappened somethingHappened)
        {
            NrEvents++;
        }
    }

    public class SecondService
        : IService, IHandle<SomethingHappened>, IHandle<ErrorOccured>
    {
        public int NrEvents = 0;
        
        public void Handle(SomethingHappened somethingHappened)
        {
            NrEvents++;
        }

        public void Handle(ErrorOccured errorOccured) { }
    }

    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void TestServiceInstantiation()
        {
            var s = new SampleService();

            Assert.AreEqual(s.NrEvents, 0);
        }

        [TestMethod]
        public void TestHandling()
        {
            var s = new SampleService();

            s.Handle(new SomethingHappened());
            s.Handle(new SomethingHappened());

            Assert.AreEqual(s.NrEvents, 2);
        }

        [TestMethod]
        public void TestEventRegistration()
        {
            var s = new SampleService();

            DomainEvents.Register<SomethingHappened>(s.Handle);

            Assert.AreEqual(s.NrEvents, 0);
        }

        [TestMethod]
        public void TestEventHandling()
        {
            var s = new SampleService();

            DomainEvents.Register<SomethingHappened>(s.Handle);
            DomainEvents.Raise(new SomethingHappened());

            Assert.AreEqual(s.NrEvents, 1);
        }

        [TestMethod]
        public void TestContainerEventHandling()
        {
            var s = new SampleService();
            var c = new UnityContainer();
            // c.RegisterInstance(s);
            //c.RegisterTypes(
            //    // Registers open generics
            //    AllClasses.FromLoadedAssemblies(false, false),
            //    WithMappings.FromAllInterfaces,
            //    WithName.TypeName);

            c.RegisterTypes(
                AllClasses
                    .FromLoadedAssemblies(false, false)
                    .Where(t => typeof(IService).IsAssignableFrom(t)),
                //WithMappings.FromAllInterfaces, // .Where(t => t.Name.StartsWith("IHandle")),
                t => t.GetInterfaces().Where(x => x.Name.StartsWith("IHandle")),
                WithName.TypeName);
            

            Console.WriteLine("Container has {0} Registrations", c.Registrations.Count());
            foreach (ContainerRegistration item in c.Registrations)
            {
                //Console.WriteLine("Registered Type: {0} Name: {1} Mapped To: {2}", item.RegisteredType, item.Name, item.MappedToType);
                Console.WriteLine(item.GetMappingAsString());
            }

            var resolved = c.ResolveAll<IHandle<SomethingHappened>>();

            // Assert.AreEqual(2, resolved.Count());
            
            Assert.AreEqual("x", resolved.Select(t => t.ToString()).Aggregate<object>((current, next) => current + ", " + next));

            // DomainEvents.Raise(new SomethingHappened());

            // Assert.AreEqual(1, s.NrEvents);
        }
    }
}
