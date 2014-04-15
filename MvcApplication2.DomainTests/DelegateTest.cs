using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcApplication2.DomainTests
{
    [TestClass]
    public class DelegateTest
    {
        public class MyClass
        {
            public delegate void DoSomething(string s);

            //public DoSomething MakeSomething()
            //{
            //    return new DoSomething();
            //}
        }

        public MyClass C { set; get; }

        [TestInitialize]
        public void InitializeTest()
        {
            C = new MyClass();
        }

        [TestMethod]
        public void InitializedDelegateWithoutMethod()
        {
            //var callback = C.MakeSomething();
            //callback("hello");
 
            Assert.IsTrue(true);
        }
    }
}
