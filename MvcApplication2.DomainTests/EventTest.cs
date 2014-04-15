using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcApplication2.DomainTests
{
    [TestClass]
    public class EventTest
    {
        public class MyDemoSubdomain
        {
            public event Action<string> SomethingHappened;

            public void FireSomethingHappened(string arg)
            {
                if (SomethingHappened != null)
                    SomethingHappened(arg);
            }

            // public delegate void SomethingHappendHandler(string arg);
        }

        public class MyDemoService
        {
            public MyDemoSubdomain Subdomain;

            public int Count { get; set; }

            public string LastArg { get; set; }

            public MyDemoService()
            {
                Count = 0;
                LastArg = "";
            }

            public void BindEvent()
            {
                Subdomain.SomethingHappened += HandleSomethingHappened;
            }

            public void FireEvent(string arg)
            {
                Subdomain.FireSomethingHappened(arg);
            }

            public void HandleSomethingHappened(string arg)
            {
                Count++;
                LastArg = arg;
            }
        }

        public MyDemoService Service { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            var d = new MyDemoSubdomain();

            var s = new MyDemoService();
            s.Subdomain = d;

            Service = s;
        }

        [TestMethod]
        public void NotBoundAndFired()
        {
            Service.FireEvent("hello");
            Assert.AreEqual(Service.Count, 0);
            Assert.AreEqual(Service.LastArg, "");
        }

        [TestMethod]
        public void BoundAndFired()
        {
            Service.BindEvent();
            Service.FireEvent("huhu");
            Assert.AreEqual(Service.Count, 1);
            Assert.AreEqual(Service.LastArg, "huhu");
        }

        [TestMethod]
        public void BoundAndFiredTwice()
        {
            Service.BindEvent();
            Service.FireEvent("hi");
            Service.FireEvent("world");
            Assert.AreEqual(Service.Count, 2);
            Assert.AreEqual(Service.LastArg, "world");
        }

        [TestMethod]
        public void BoundTwiceAndFiredTwice()
        {
            Service.BindEvent();
            Service.BindEvent();
            Service.FireEvent("hi");
            Service.FireEvent("world");
            Assert.AreEqual(Service.Count, 4);
            Assert.AreEqual(Service.LastArg, "world");
        }

    }
}
